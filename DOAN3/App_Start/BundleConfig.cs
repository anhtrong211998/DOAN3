using System.Web;
using System.Web.Optimization;

namespace DOAN3
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

            bundles.Add(new StyleBundle("~/Content/css1").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/css").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/style.css",
                "~/Content/css/revslider.css",
                "~/Content/css/owl.carousel.css",
                "~/Content/css/owl.theme.css"));
            bundles.Add(new ScriptBundle("~/Content/js").Include(
                "~/Content/js/jquery.min.js",
                "~/Content/js/bootstrap.min.js",
                "~/Content/js/common.js",
                "~/Content/js/revslider.js",
                "~/Content/js/owl.carousel.min.js",
                "~/Content/js/jquery.flexslider.js"));
            bundles.Add(new StyleBundle("~/Content/blogmate").Include(
                "~/Content/css/blogmate.css"));
            bundles.Add(new StyleBundle("~/Content/login").Include(
                "~/Content/css/bootstrap.min.css",
                "~/Content/css/font-awesome.css",
                "~/Content/css/Stylelogin.css"));
            bundles.Add(new StyleBundle("~/Content/sanpham").Include(
               "~/Content/css/rate.css",
               "~/Content/css/flexslider.css"));
        }
    }
}
