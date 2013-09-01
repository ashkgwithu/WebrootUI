using System.Web.Optimization;

namespace WebrootUI2.Web.Mvc
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.IgnoreList.Clear();
            AddDefaultIgnorePatterns(bundles.IgnoreList);

            bundles.Add(new ScriptBundle("~/bundles/common").Include(
                "~/Scripts/Common.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryadd").Include(
                        "~/Scripts/jquery.actual*",
                        "~/Scripts/jquery-migrate*"));

            bundles.Add(new ScriptBundle("~/bundles/datatable").Include(
                        "~/Scripts/jquery.dataTables.min.js",
                        "~/Scripts/dataTables.customize.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/paging").Include("~/Scripts/paging.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/sidebar.css",
                "~/Content/Site.css",
                "~/Content/style.css",
                "~/Content/tamarillo.css",
                "~/Content/jquery.qtip*"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css",
                        "~/Content/bootstrap-responsive.css"));

            bundles.Add(new StyleBundle("~/Content/jqueryui").Include(
                        "~/Content/themes/base/jquery-ui.css"));

            bundles.Add(new StyleBundle("~/Content/stylecss").Include("~/Content/style.css"));

            bundles.Add(new StyleBundle("~/Content/Gebo-Template")
                .Include("~/Content/splashy.css")
                .Include("~/Content/flags.css"));
        }

        public static void AddDefaultIgnorePatterns(IgnoreList ignoreList)
        {
            ignoreList.Ignore("*.debug.js", OptimizationMode.WhenEnabled);
            ignoreList.Ignore("*.debug.css", OptimizationMode.WhenDisabled);
        }
    }
}