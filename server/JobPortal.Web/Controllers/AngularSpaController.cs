using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Web.ViewModel;
using Newtonsoft.Json;

namespace JobPortal.Web.Controllers
{
    public class AngularSpaController : Controller
    {
        private string GetDevelopmentAssetUrl(string assetName)
        {
            return "/Content/dist/" + assetName;
        }

        public ActionResult Index()
        {
            var isInDevelopment = HttpContext.IsDebuggingEnabled;
            var responseViewModel = new AngularSpaResponseViewModel();
            if (isInDevelopment)
            {
                var defaultThemeCss = new StylesheetData
                {
                    IsTheme = true,
                    StylesheetUrl = this.GetDevelopmentAssetUrl("defaultTheme.css")
                }; 
                var vendorCss = new StylesheetData
                {
                    StylesheetUrl = this.GetDevelopmentAssetUrl("vendor.css")
                };
                var mainCss = new StylesheetData
                {
                    StylesheetUrl = this.GetDevelopmentAssetUrl("main.css")
                };
                var manifestScript = this.GetDevelopmentAssetUrl("manifest.js");
                var polyfillsScript = this.GetDevelopmentAssetUrl("polyfills.js");
                var vendorScript = this.GetDevelopmentAssetUrl("vendor.js");
                var mainScript = this.GetDevelopmentAssetUrl("main.js");
                responseViewModel.Stylesheets = new StylesheetData[3] { defaultThemeCss, vendorCss, mainCss };
                responseViewModel.Scripts = new string[4] { manifestScript, polyfillsScript, vendorScript, mainScript };
                responseViewModel.Themes = new ThemeData[3]
                {
                    new ThemeData
                    {
                        ThemeId = "default_theme",
                        IsDefault = true,
                        StylesheetUrl = this.GetDevelopmentAssetUrl("defaultTheme.css"),
                        UIMainColor = "#337ab7"
                    },
                    new ThemeData
                    {
                        ThemeId = "green_theme",
                        StylesheetUrl = this.GetDevelopmentAssetUrl("greenTheme.css"),
                        UIMainColor = "green"
                    },
                    new ThemeData
                    {
                        ThemeId = "red_theme",
                        StylesheetUrl = this.GetDevelopmentAssetUrl("redTheme.css"),
                        UIMainColor = "red"
                    }
                };

            }
            else
            {
                var webpackAssetsPath = HttpContext.Server.MapPath("~/Content/dist/webpack-assets.json");
                using (var streamReader = new StreamReader(webpackAssetsPath))
                {
                    string json = streamReader.ReadToEnd();
                    dynamic assets = JsonConvert.DeserializeObject(json);
                    var stylesheetUrls = new List<string>();
                    var scriptUrls = new List<string>();
                    var vendorCssUrl = assets.vendor.css;
                    if (vendorCssUrl != null)
                    {
                        stylesheetUrls.Add(vendorCssUrl.ToString());
                    }
                    var mainCssUrl = assets.main.css;
                    if (mainCssUrl != null)
                    {
                        stylesheetUrls.Add(mainCssUrl.ToString());
                    }
                    scriptUrls.Add(assets.manifest.js.ToString());
                    scriptUrls.Add(assets.polyfills.js.ToString());
                    scriptUrls.Add(assets.vendor.js.ToString());
                    scriptUrls.Add(assets.main.js.ToString());
                    //responseViewModel.Stylesheets = stylesheetUrls;
                    responseViewModel.Scripts = scriptUrls;
                }

            }
            return View(responseViewModel);
        }
    }
}