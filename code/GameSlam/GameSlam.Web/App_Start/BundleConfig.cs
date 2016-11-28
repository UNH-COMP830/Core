using System.Web;
using System.Web.Optimization;

namespace GameSlam.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.UseCdn = true;
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Assets/js/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Assets/js/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Assets/js/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Assets/js/bootstrap.js",
                      "~/Assets/js/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Assets/css/bootstrap.css",
                      "~/Assets/css/site.css"));
                                
            bundles.Add(new StyleBundle("~/Content/gameStyle").Include(
                      "~/Assets/css/reset.css",
                      "~/Assets/css/css/animate.css",
                      "~/Assets/css/style.css"));

            bundles.Add(new StyleBundle("~/Content/Images").Include(
                      "~/Assets/images/logo.png"));

            bundles.Add(new ScriptBundle("~/bundles/gameSlamjs").Include(
                        "~/Assets/js/gameslam.js"));
        }
    }
}
