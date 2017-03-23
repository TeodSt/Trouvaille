using System.Web;
using System.Web.Optimization;

namespace Trouvaille.MVC
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*",
                        "~/Scripts/jquery.unobtrusive-ajax.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/redirect").Include(
                        "~/Scripts/Custom/redirect.js"));

            bundles.Add(new ScriptBundle("~/bundles/map").Include(
                        "~/Scripts/Custom/map.js"));

            bundles.Add(new ScriptBundle("~/bundles/tinymce-config").Include(
                        "~/Scripts/Custom/tinymce-config.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/mdb.js",
                      "~/Scripts/tether.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/mdb.css",
                      "~/Content/style.css",
                      "~/Content/article.css"));
        }
    }
}
