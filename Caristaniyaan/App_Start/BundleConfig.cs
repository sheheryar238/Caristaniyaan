using System.Web;
using System.Web.Optimization;

namespace Caristaniyaan
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new ScriptBundle("~/bundles/scripts").Include(
                      "~/Scripts/jquery-2.2.4.min.js",
                      "~/Scripts/jquery-ui.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/owl.carousel.js",
                      "~/Scripts/jquery.magnific-popup.min.js",
                      "~/Scripts/ProductImageSlider.js",
                      "~/Scripts/addtocart.js",
                      "~/Scripts/script.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/jquery-ui.css",
                      "~/Content/bootstrap.min.css",
                      "~/Content/owl.carousel.css",
                      "~/Content/owl.theme.css",
                      "~/Content/owl.transitions.css",
                      "~/Content/magnific-popup.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/animate.css",
                      "~/Content/fw.css",
                      "~/Content/car_service.css",
                      "~/Content/ProductImageSlider.css"));

            bundles.Add(new StyleBundle("~/admin/css").Include(
                      "~/Content/bootstrap.min.css",
                      "~/Content/font-awesome.min.css",
                      "~/Content/ionicons.min.css",
                      "~/Content/admin.min.css",
                      "~/Content/skin-red.min.css",
                      "~/Content/DataTables/css/dataTables.bootstrap.css"
                      ));

            bundles.Add(new ScriptBundle("~/admin/scripts").Include(
                      "~/Scripts/jquery.min.js",
                      "~/Scripts/bootstrap.min.js",
                      "~/Scripts/bootbox.min.js",
                      "~/Scripts/admin.min.js", 
                      "~/Scripts/DataTables/jquery.dataTables.js",
                      "~/Scripts/DataTables/dataTables.bootstrap.js"));
        }
    }
}                                                   