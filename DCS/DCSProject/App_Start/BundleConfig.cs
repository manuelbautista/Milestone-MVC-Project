using System.Web;
using System.Web.Optimization;

namespace DCSProject
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.unobtrusive*",
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            //bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/site.css"));

            bundles.Add(new StyleBundle("~/Content/themes/base/css").Include(
                        "~/Content/themes/base/jquery.ui.core.css",
                        "~/Content/themes/base/jquery.ui.resizable.css",
                        "~/Content/themes/base/jquery.ui.selectable.css",
                        "~/Content/themes/base/jquery.ui.accordion.css",
                        "~/Content/themes/base/jquery.ui.autocomplete.css",
                        "~/Content/themes/base/jquery.ui.button.css",
                        "~/Content/themes/base/jquery.ui.dialog.css",
                        "~/Content/themes/base/jquery.ui.slider.css",
                        "~/Content/themes/base/jquery.ui.tabs.css",
                        "~/Content/themes/base/jquery.ui.datepicker.css",
                        "~/Content/themes/base/jquery.ui.progressbar.css",
                        "~/Content/themes/base/jquery.ui.theme.css"));


            bundles.Add(new StyleBundle("~/content/sitecss").Include(
                        "~/Content/bootstrap.min.css",
                        "~/Content/metisMenu.min.css",
                        "~/Content/sb-admin-2.css",
                        "~/Content/font-awesome.min.css",
                        "~/Content/main.css"
                        ));

            bundles.Add(new StyleBundle("~/content/datatablecss").Include(
                       "~/Content/dataTables.bootstrap.css",
                       "~/Content/dataTables.responsive.css"
                       ));

            bundles.Add(new StyleBundle("~/plugins/dataTablesStyles").Include(
                      "~/Content/plugins/dataTables/dataTables.bootstrap.css",
                      "~/Content/plugins/dataTables/dataTables.responsive.css",
                      "~/Content/plugins/dataTables/dataTables.tableTools.min.css"));

            bundles.Add(new ScriptBundle("~/plugins/dataTables").Include(
                      "~/Scripts/plugins/dataTables/jquery.dataTables.js",
                      "~/Scripts/plugins/dataTables/dataTables.bootstrap.js",
                      "~/Scripts/plugins/dataTables/dataTables.responsive.js",
                      "~/Scripts/plugins/dataTables/dataTables.tableTools.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/templatejs").Include(
                        "~/Scripts/jquery.min.js",
                        "~/Scripts/bootstrap.min.js",
                        "~/Scripts/metisMenu.min.js",
                        "~/Scripts/sb-admin-2.js"));

            //bundles.Add(new StyleBundle("~/content/themes/base/datepickercss").Include(
            //           "~/Content/themes/base/core.css",
            //           "~/Content/themes/base/datepicker.css",
            //           "~/Content/themes/base/theme.css"
            //           ));

            bundles.Add(new ScriptBundle("~/bundles/globalization").Include(
                "~/Scripts/cldr.js",
                "~/Scripts/cldr/event.js",
                "~/Scripts/cldr/supplemental.js",
                "~/Scripts/globalize.js",
                "~/Scripts/globalize/message.js",
                "~/Scripts/globalize/number.js",
                "~/Scripts/globalize/plural.js",
                "~/Scripts/globalize/date.js",
                "~/Scripts/globalize/currency.js",
                "~/Scripts/globalize/relative-time.js"));

            bundles.Add(new ScriptBundle("~/bundles/datepickerjs").Include(
                       "~/Scripts/ui/core.js",
                       "~/Scripts/ui/widget.js",
                       "~/Scripts/ui/datepicker.js"
               ));

            bundles.Add(new StyleBundle("~/content/themes/base/datepickercss").Include(
                        "~/Content/themes/base/core.css",
                        "~/Content/themes/base/datepicker.css",
                        "~/Content/themes/base/theme.css"
                        ));

         
        }
    }
}