using System;
using System.Data.Entity;
using System.Data.Services;
using System.Data.Services.Common;
using System.Data.Services.Providers;
using System.IO;
using System.Linq;
using System.ServiceModel;
using System.Web;
using System.Web.Mvc;
using QueryInterceptor;

namespace NuGetGallery
{
    [ServiceBehavior(IncludeExceptionDetailInFaults = true, ConcurrencyMode = ConcurrencyMode.Multiple)]
    public abstract class FeedServiceBase<TPackage> : DataService<FeedContext<TPackage>>, IDataServiceStreamProvider, IServiceProvider
    {
        private readonly IEntitiesContext entities;
        private readonly IEntityRepository<Package> packageRepo;
        private readonly IConfiguration configuration;
        private readonly ISearchService searchService;

        public FeedServiceBase()
            : this(DependencyResolver.Current.GetService<IEntitiesContext>(),
                   DependencyResolver.Current.GetService<IEntityRepository<Package>>(),
                   DependencyResolver.Current.GetService<IConfiguration>(),
                   DependencyResolver.Current.GetService<ISearchService>())
        {

        }

        protected FeedServiceBase(
            IEntitiesContext entities,
            IEntityRepository<Package> packageRepo,
            IConfiguration configuration,
            ISearchService searchService)
        {
            this.entities = entities;
            this.packageRepo = packageRepo;
            this.configuration = configuration;
            this.searchService = searchService;
        }

        protected IEntitiesContext Entities
        {
            get { return entities; }
        }

        protected IEntityRepository<Package> PackageRepo
        {
            get { return packageRepo; }
        }

        protected IConfiguration Configuration
        {
            get { return configuration; }
        }

        protected ISearchService SearchService
        {
            get { return searchService; }
        }

        // This method is called only once to initialize service-wide policies.
        protected static void InitializeServiceBase(DataServiceConfiguration config)
        {
            config.SetServiceOperationAccessRule("Search", ServiceOperationRights.AllRead);
            config.SetServiceOperationAccessRule("FindPackagesById", ServiceOperationRights.AllRead);
            config.SetEntitySetAccessRule("Packages", EntitySetRights.AllRead);
            config.SetEntitySetPageSize("Packages", 100);
            config.DataServiceBehavior.MaxProtocolVersion = DataServiceProtocolVersion.V2;
            config.UseVerboseErrors = true;
        }

        protected abstract override FeedContext<TPackage> CreateDataSource();

        public void DeleteStream(
            object entity,
            DataServiceOperationContext operationContext)
        {
            throw new NotSupportedException();
        }

        public Stream GetReadStream(
            object entity,
            string etag,
            bool? checkETagForEquality,
            DataServiceOperationContext operationContext)
        {
            throw new NotSupportedException();
        }

        public abstract Uri GetReadStreamUri(
            object entity,
            DataServiceOperationContext operationContext);


        public string GetStreamContentType(
            object entity,
            DataServiceOperationContext operationContext)
        {
            return "application/zip";
        }

        public string GetStreamETag(
            object entity,
            DataServiceOperationContext operationContext)
        {
            return null;
        }

        public Stream GetWriteStream(
            object entity,
            string etag,
            bool? checkETagForEquality,
            DataServiceOperationContext operationContext)
        {
            throw new NotSupportedException();
        }

        public string ResolveType(
            string entitySetName,
            DataServiceOperationContext operationContext)
        {
            throw new NotSupportedException();
        }

        public int StreamBufferSize
        {
            get { return 64000; }
        }

        public object GetService(Type serviceType)
        {
            if (serviceType == typeof(IDataServiceStreamProvider))
            {
                return this;
            }

            return null;
        }

        protected virtual IQueryable<Package> SearchCore(string searchTerm, string targetFramework)
        {
            var packages = PackageRepo.GetAll()
                                      .Include(p => p.PackageRegistration)
                                      .Include(x => x.Authors)
                                      .Include(x => x.PackageRegistration.Owners)
                                      .Where(p => p.Listed);

            if (String.IsNullOrEmpty(searchTerm))
            {
                return packages;
            }
            var parameters = ReadODataParameters();
            if (parameters.FilterByLatestVersion)
            {
                // Determines if the query requires filtering by latest version. A user could ask for all versions of a package by specifying the -All parameter in the cmdlet.
                return GetResultsFromSearchService(packages, searchTerm, parameters);
            }
            return packages.Search(searchTerm);
        }

        private IQueryable<Package> GetResultsFromSearchService(IQueryable<Package> packages, string searchTerm, ODataParameters parameters)
        {
            // For count queries, we can ask Lucene to essentially no-op. A cleaner way to do this would be to have a method on the search service that simply gives counts, 
            // but it seems like overkill.
            int take = parameters.IsCountQuery ? 0 : (parameters.Skip + 1) * parameters.Top;
            int totalHits = 0;
            var result = SearchService.SearchWithRelevance(packages, searchTerm, take, out totalHits);

            if (parameters.IsCountQuery)
            {
                // At this point, we already know what the total count is. We can have it return this value very quickly without doing any SQL by using our custom query provider.
                return new CountQuery<Package>(totalHits);
            }
            else if (!parameters.IsOrderedQuery)
            {
                // If no order by parameters are provided, OData decides to sort it by the primary keys in this case Id and Version. We don't want it affecting our relevance 
                // results, so we have it removed.
                result = result.InterceptWith(new RemoveOrderByVisitor());
            }
            return result;
        }

        private ODataParameters ReadODataParameters()
        {
            var request = HttpContext.Current.Request;

            return new ODataParameters
            {
                IsCountQuery = request.Path.TrimEnd('/').EndsWith("$count"),

                FilterByLatestVersion = request["$filter"].IndexOf("IsLatestVersion", StringComparison.Ordinal) != -1,

                Top = ReadInt(request["$top"], 30),

                Skip = ReadInt(request["$skip"], 0),
                
                IsOrderedQuery = !String.IsNullOrEmpty(request["$orderby"])
            };
        }

        private int ReadInt(string requestValue, int defaultValue)
        {
            int result;
            return Int32.TryParse(requestValue, out result) ? result : defaultValue;
        }

        protected virtual bool UseHttps()
        {
            return HttpContext.Current.Request.IsSecureConnection;
        }

        private sealed class ODataParameters
        {
            public bool IsCountQuery { get; set; }

            public bool IsOrderedQuery { get; set; }

            public bool FilterByLatestVersion { get; set; }

            public int Top { get; set; }

            public int Skip { get; set; }
        }
    }
}
