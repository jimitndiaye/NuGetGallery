// <auto-generated />
// This file was generated by a T4 template.
// Don't change it directly as your change would get overwritten.  Instead, make changes
// to the .tt file (i.e. the T4 template) and save it to regenerate this file.

// Make sure the compiler doesn't complain about missing Xml comments
#pragma warning disable 1591
#region T4MVC

using System;
using System.Diagnostics;
using System.CodeDom.Compiler;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Hosting;
using System.Web.Mvc;
using System.Web.Mvc.Ajax;
using System.Web.Mvc.Html;
using System.Web.Routing;
using T4MVC;
namespace NuGetGallery {
    public partial class ApiController {
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected ApiController(Dummy d) { }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        protected RedirectToRouteResult RedirectToAction(ActionResult result) {
            var callInfo = result.GetT4MVCResult();
            return RedirectToRoute(callInfo.RouteValueDictionary);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetPackage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetPackage);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetPackageCdn()
        {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetPackageCdn);
        }

        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult VerifyPackageKey() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.VerifyPackageKey);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CreatePackagePut() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.CreatePackagePut);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult CreatePackagePost() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.CreatePackagePost);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult DeletePackage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.DeletePackage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult PublishPackage() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.PublishPackage);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetPackageIds() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetPackageIds);
        }
        [NonAction]
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public System.Web.Mvc.ActionResult GetPackageVersions() {
            return new T4MVC_ActionResult(Area, Name, ActionNames.GetPackageVersions);
        }

        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ApiController Actions { get { return MVC.Api; } }
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Area = "";
        [GeneratedCode("T4MVC", "2.0")]
        public readonly string Name = "Api";

        static readonly ActionNamesClass s_actions = new ActionNamesClass();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ActionNamesClass ActionNames { get { return s_actions; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ActionNamesClass {
            public readonly string GetPackage = "GetPackageApi";
            public readonly string GetPackageCdn = "GetPackageCdnApi";
            public readonly string GetNuGetExe = "GetNuGetExeApi";
            public readonly string VerifyPackageKey = "VerifyPackageKeyApi";
            public readonly string CreatePackagePut = "PushPackageApi";
            public readonly string CreatePackagePost = "PushPackageApi";
            public readonly string DeletePackage = "DeletePackageApi";
            public readonly string PublishPackage = "PublishPackageApi";
            public readonly string GetPackageIds = "PackageIDs";
            public readonly string GetPackageVersions = "PackageVersions";
        }


        static readonly ViewNames s_views = new ViewNames();
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public ViewNames Views { get { return s_views; } }
        [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
        public class ViewNames {
        }
    }

    [GeneratedCode("T4MVC", "2.0"), DebuggerNonUserCode]
    public class T4MVC_ApiController: NuGetGallery.ApiController {
        public T4MVC_ApiController() : base(Dummy.Instance) { }

        public override System.Web.Mvc.ActionResult GetPackage(string id, string version) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetPackage);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("version", version);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetNuGetExe() {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetNuGetExe);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult VerifyPackageKey(string apiKey, string id, string version) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.VerifyPackageKey);
            callInfo.RouteValueDictionary.Add("apiKey", apiKey);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("version", version);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreatePackagePut(string apiKey) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreatePackagePut);
            callInfo.RouteValueDictionary.Add("apiKey", apiKey);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult CreatePackagePost(string apiKey) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.CreatePackagePost);
            callInfo.RouteValueDictionary.Add("apiKey", apiKey);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult DeletePackage(string apiKey, string id, string version) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.DeletePackage);
            callInfo.RouteValueDictionary.Add("apiKey", apiKey);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("version", version);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult PublishPackage(string apiKey, string id, string version) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.PublishPackage);
            callInfo.RouteValueDictionary.Add("apiKey", apiKey);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("version", version);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetPackageIds(string partialId, bool? includePrerelease) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetPackageIds);
            callInfo.RouteValueDictionary.Add("partialId", partialId);
            callInfo.RouteValueDictionary.Add("includePrerelease", includePrerelease);
            return callInfo;
        }

        public override System.Web.Mvc.ActionResult GetPackageVersions(string id, bool? includePrerelease) {
            var callInfo = new T4MVC_ActionResult(Area, Name, ActionNames.GetPackageVersions);
            callInfo.RouteValueDictionary.Add("id", id);
            callInfo.RouteValueDictionary.Add("includePrerelease", includePrerelease);
            return callInfo;
        }

    }
}

#endregion T4MVC
#pragma warning restore 1591
