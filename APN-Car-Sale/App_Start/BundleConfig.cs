using System.Web;
using System.Web.Optimization;

namespace APN_Car_Sale
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
            //            "~/Scripts/jquery-{version}.js"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js",
                      "~/Template/js/owl.carousel.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css",
                      "~/Template/css/owl.carousel.css"));

            bundles.Add(new ScriptBundle("~/Template/js").Include(
                        "~/Template/js/jquery.min.js",
                        //"~/Template/js/jquery-migrate-3.0.1.min.js",
                        "~/Template/js/popper.min.js",
                        "~/Template/js/bootstrap.min.js",
                        "~/Template/js/jquery.easing.1.3.js",
                        "~/Template/js/jquery.waypoints.min.js",
                        //"~/Template/js/jquery.stellar.min.js",
                        "~/Template/js/owl.carousel.min.js",
                        "~/Template/js/jquery.magnific-popup.min.js",
                        "~/Template/js/aos.js",
                        "~/Template/js/jquery.animateNumber.min.js",
                        "~/Template/js/bootstrap-datepicker.js",
                        "~/Template/js/jquery.timepicker.min.js",
                        "~/Template/js/scrollax.min.js",
                        //"~/Template/js/google-map.js",
                        "~/Content/slider/js/jquery.flexslider.js",
                        "~/Template/js/main.js"
                        ));

            bundles.Add(new StyleBundle("~/Template/css").Include(
                        "~/Template/css/open-iconic-bootstrap.min.css",
                        "~/Template/css/animate.css",
                        "~/Template/css/owl.carousel.min.css",
                        "~/Template/css/owl.theme.default.min.css",
                        "~/Template/css/magnific-popup.css",
                        "~/Template/css/aos.css",
                        "~/Template/css/ionicons.min.css",
                        "~/Template/css/bootstrap-datepicker.css",
                        "~/Template/css/jquery.timepicker.css",
                        "~/Template/css/flaticon.css",
                        "~/Template/css/icomoon.css",
                        "~/Content/slider/css/flexslider.css",
                        "~/Content/slider/css/demo.css",
                        "~/Template/css/style.css"
                ));
        }
    }
}
