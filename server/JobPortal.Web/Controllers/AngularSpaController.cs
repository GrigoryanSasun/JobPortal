using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Common.Helpers;
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

        private IEnumerable<DeserializedTheme> GetDeserializedThemes()
        {
            var themesJson = JsonHelper.ReadJsonFile(Server.MapPath("~/Themes.json"));
            var themes = JsonConvert.DeserializeObject<DeserializedTheme[]>(themesJson);
            return themes;
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
            var themes = this.GetDeserializedThemes();
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
            result.Stylesheets = new StylesheetData[3] { defaultThemeData, vendorCss, mainCss };
            result.Scripts = new string[4] { manifestScript, polyfillsScript, vendorScript, mainScript };
            return result;
        }

        private AngularSpaResponseViewModel GetProductionResponseViewModel()
        {
            var result = new AngularSpaResponseViewModel();
            var webpackAssetsPath = Server.MapPath("~/Content/dist/webpack-assets.json");
            string webpackAssetsJson = JsonHelper.ReadJsonFile(webpackAssetsPath);
            var assets = JsonConvert.DeserializeObject<Dictionary<string, dynamic>>(webpackAssetsJson);
            var stylesheets = new List<StylesheetData>();
            var scriptUrls = new List<string>();
            dynamic manifestData;
            if (assets.TryGetValue("manifest", out manifestData))
            {
                scriptUrls.Add(manifestData.js.ToString());
            }
            dynamic polyfillData;
            if (assets.TryGetValue("polyfills", out polyfillData))
            {
                scriptUrls.Add(polyfillData.js.ToString());
            }
            dynamic vendorData;
            if (assets.TryGetValue("vendor", out vendorData))
            {
                stylesheets.Add(new StylesheetData
                {
                    IsTheme = false,
                    StylesheetUrl = vendorData.css.ToString()
                });
                scriptUrls.Add(vendorData.js.ToString());
            }
            dynamic mainData;
            if (assets.TryGetValue("main", out mainData))
            {
                stylesheets.Add(new StylesheetData
                {
                    IsTheme = false,
                    StylesheetUrl = mainData.css.ToString()
                });
                scriptUrls.Add(mainData.js.ToString());
            }
            var themes = this.GetDeserializedThemes();
            var themeDatas = new List<ThemeData>();
            foreach (var theme in themes)
            {
                dynamic themeAsset;
                if (assets.TryGetValue(theme.filename, out themeAsset))
                {
                    var themeData = new ThemeData
                    {
                        ThemeId = theme.id,
                        IsDefault = theme.is_default,
                        StylesheetUrl = themeAsset.css.ToString(),
                        UIMainColor = theme.ui_main_color,
                        UIDescription = theme.ui_description
                    };
                    if (themeData.IsDefault)
                    {
                        stylesheets.Insert(0, new StylesheetData
                        {
                            IsTheme = true,
                            StylesheetUrl = themeData.StylesheetUrl
                        });
                    }
                    themeDatas.Add(themeData);
                }
            }
            result.Stylesheets = stylesheets;
            result.Scripts = scriptUrls;
            result.Themes = themeDatas;
            return result;
        }

        public ActionResult Index()
        {
            // If debugging is enabled, consider the app in development mode
            var isInDevelopment = HttpContext.IsDebuggingEnabled;
            AngularSpaResponseViewModel result = null;
            if (isInDevelopment)
            {
                result = this.GetDevelopmentReponseViewModel();
            }
            else
            {
                result = this.GetProductionResponseViewModel();
            }
            return View(result);
        }
    }
}