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

        private AngularSpaResponseViewModel GetDevelopmentReponseViewModel()
        {
            var result = new AngularSpaResponseViewModel();
            StylesheetData defaultThemeData = null;
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
            using (StreamReader r = new StreamReader(Server.MapPath("~/Themes.json")))
            {
                string json = r.ReadToEnd();
                var themes = JsonConvert.DeserializeObject<DeserializedTheme[]>(json);
                var themeDatas = new List<ThemeData>();
                foreach (var theme in themes)
                {
                    var themeData = new ThemeData
                    {
                        ThemeId = theme.id,
                        IsDefault = theme.is_default,
                        StylesheetUrl = this.GetDevelopmentAssetUrl(theme.filename + ".css"),
                        UIMainColor = theme.ui_main_color,
                        UIDescription = theme.ui_description
                    };
                    themeDatas.Add(themeData);
                    if (themeData.IsDefault)
                    {
                        defaultThemeData = new StylesheetData
                        {
                            IsTheme = true,
                            StylesheetUrl = themeData.StylesheetUrl
                        };
                    }
                }
                result.Themes = themeDatas;
            }
            result.Stylesheets = new StylesheetData[3] { defaultThemeData, vendorCss, mainCss };
            result.Scripts = new string[4] { manifestScript, polyfillsScript, vendorScript, mainScript };
            return result;
        }

        public ActionResult Index()
        {
            var isInDevelopment = HttpContext.IsDebuggingEnabled;
            AngularSpaResponseViewModel result = null;
            if (isInDevelopment)
            {
                result = this.GetDevelopmentReponseViewModel();
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
                    result.Scripts = scriptUrls;
                }

            }
            return View(result);
        }
    }
}