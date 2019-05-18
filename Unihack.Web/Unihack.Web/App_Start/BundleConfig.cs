using System.Web;
using System.Web.Optimization;

namespace Unihack.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            #region scripts
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/login").Include(
                      "~/Scripts/login.js"));

            bundles.Add(new ScriptBundle("~/bundles/register").Include(
                      "~/Scripts/register.js"));

            bundles.Add(new ScriptBundle("~/bundles/ajaxHelper").Include(
                      "~/Scripts/AjaxHelper.js"));

            bundles.Add(new ScriptBundle("~/bundles/easy-loading").Include(
                      "~/Scripts/easy-loading.js",
                       "~/Scripts/easy-loading.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/toastr").Include(
                        "~/Scripts/toastr.js"));

            bundles.Add(new ScriptBundle("~/bundles/home").Include(
                        "~/Scripts/home.js"));

            bundles.Add(new ScriptBundle("~/bundles/add-manager").Include(
                        "~/Scripts/add-manager.js"));

            bundles.Add(new ScriptBundle("~/bundles/perfect-scrollbar.min.js").Include(
                       "~/Scripts/perfect-scrollbar.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/logout").Include(
                    "~/Scripts/logout.js"));
            #endregion

            #region styles
            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/login").Include(
                     "~/Content/login.css"));

            bundles.Add(new StyleBundle("~/Content/util").Include(
                     "~/Content/util.css"));

            bundles.Add(new StyleBundle("~/Content/font-awesome").Include(
                     "~/Content/font-awesome.min.css"));

            bundles.Add(new StyleBundle("~/Content/easy-loading").Include(
                     "~/Content/easy-loading.min.css"));

            bundles.Add(new StyleBundle("~/Content/toastr").Include(
                       "~/Content/toastr.css"));

            bundles.Add(new StyleBundle("~/Content/topnav").Include(
                       "~/Content/topnav.css"));

            bundles.Add(new StyleBundle("~/Content/home").Include(
                       "~/Content/home.css"));

            bundles.Add(new StyleBundle("~/Content/table-main").Include(
                       "~/Content/table-main.css"));

            bundles.Add(new StyleBundle("~/Content/perfect-scrollbar").Include(
                       "~/Content/perfect-scrollbar.css"));

            #endregion
        }
    }
}
