﻿using System.Web;
using System.Web.Optimization;

namespace SuperStore
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap/js").Include(
                    // "~/Content/js/responsive.bootstrap.min.js",
                    "~/Scripts/bootstrap.min.js",
                     //"~/Scripts/bootstrap-datetimepicker.min.js",
                     "~/Scripts/jquery.validate.min.js",
                      "~/Scripts/jquery.validate.unobtrusive.min.js",
                     "~/Scripts/jquery.unobtrusive-ajax.js"
                   // "~/Content/toastr/toastr.min.js",
                   //"~/Content/Scripts/progress.js",
                   //"~/Content/js/moment.min.js",
                   // "~/Content/inpumask/jquery.inputmask.bundle.js",
                   // "~/Content/Scripts/jquery.countdown.min.js",
                   // "~/Content/Scripts/jquery.marquee.js"
                    ));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapdatatable/js").Include(
                   "~/Scripts/jquery.dataTables.min.js",
                   "~/Scripts/dataTables.bootstrap.min.js",
                    "~/Scripts/dataTables.buttons.min.js",
                    "~/Scripts/dataTables.responsive.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            // bootstrap datatable css
            bundles.Add(new StyleBundle("~/bootstrap/datatable/css").Include(
                       "~/Content/css/responsive.bootstrap.min.css",
                       "~/Content/css/dataTables.bootstrap.min.css",
                       "~/Content/css/buttons.dataTables.min.css"));
        }
    }
}
