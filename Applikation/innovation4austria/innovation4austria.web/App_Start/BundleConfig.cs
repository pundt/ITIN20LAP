using System.Web;
using System.Web.Optimization;

namespace innovation4austria.web
{
    public class BundleConfig
    {
        // Weitere Informationen zu Bundling finden Sie unter "http://go.microsoft.com/fwlink/?LinkId=301862"
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery-ui-{version}.js",
                        "~/Scripts/datepicker-de.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/ajax").Include(
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/customjs").Include(
                        "~/Scripts/bootstrap.js",
                        "~/Scripts/jquery.easing.min.js",
                        "~/Scripts/custom.js",
                        "~/Scripts/toastr.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.css",
                "~/Content/css/font-awesome.min.css",
                "~/Content/css/style.css",
                "~/Content/css/toastr.min.css",
                "~/Content/css/jqueryui/base/all.css"));
        }
    }
}
