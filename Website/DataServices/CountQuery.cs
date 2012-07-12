using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace NuGetGallery
{
    public class CountQuery<TVal> : IQueryable, IQueryable<TVal>, IQueryProvider
    {
        private readonly IQueryable<TVal> innerQuery;
        private readonly long count;

        public CountQuery(long count)
        {
            this.count = count;
            this.innerQuery = Enumerable.Empty<TVal>().AsQueryable();
        }

        public IEnumerator<TVal> GetEnumerator()
        {
            return innerQuery.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public Type ElementType
        {
            get { return innerQuery.ElementType; }
        }

        public System.Linq.Expressions.Expression Expression
        {
            get { return innerQuery.Expression; }
        }

        public IQueryProvider Provider
        {
            get { return this; }
        }

        public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
        {
            return innerQuery.Provider.CreateQuery<TElement>(expression);
        }

        public IQueryable CreateQuery(Expression expression)
        {
            return innerQuery.Provider.CreateQuery(expression);
        }

        public TResult Execute<TResult>(System.Linq.Expressions.Expression expression)
        {
            return innerQuery.Provider.Execute<TResult>(expression);
        }

        public object Execute(System.Linq.Expressions.Expression expression)
        {
            if (expression.NodeType == ExpressionType.Call)
            {
                var methodCall = (MethodCallExpression)expression;
                if (methodCall.Method.Name.Equals("LongCount", StringComparison.Ordinal))
                {
                    return (long)count;
                }
            }
            return innerQuery.Provider.Execute(expression);
        }
    }
}