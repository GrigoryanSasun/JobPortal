using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using JobPortal.Web.ViewModels;
using Newtonsoft.Json;

namespace JobPortal.Web.Controllers
{
    public class AngularSpaController : Controller
    {
        private string GetDevelopmentAssetUrl(string assetName)
        {
            return "/dist/" + assetName;
        }

        private string GetProductionAssetUrl(string assetUrl)
        {
            return "/Content" + assetUrl;
        }

        public ActionResult Index()
        {
            var isInDevelopment = HttpContext.IsDebuggingEnabled;
            var responseViewModel = new AngularSpaResponseViewModel();
            if (isInDevelopment)
            {
                var manifestScript = this.GetDevelopmentAssetUrl("manifest.js");
                var polyfillsScript = this.GetDevelopmentAssetUrl("polyfills.js");
                var vendorScript = this.GetDevelopmentAssetUrl("vendor.js");
                var mainScript = this.GetDevelopmentAssetUrl("main.js");
                responseViewModel.StylesheetUrls = new string[0];
                responseViewModel.JavascriptUrls = new string[4] { manifestScript, polyfillsScript, vendorScript, mainScript };
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
                        stylesheetUrls.Add(this.GetProductionAssetUrl(vendorCssUrl.ToString()));
                    }
                    var mainCssUrl = assets.main.css;
                    if (mainCssUrl != null)
                    {
                        stylesheetUrls.Add(this.GetProductionAssetUrl(mainCssUrl.ToString()));
                    }
                    scriptUrls.Add(this.GetProductionAssetUrl(assets.manifest.js.ToString()));
                    scriptUrls.Add(this.GetProductionAssetUrl(assets.polyfills.js.ToString()));
                    scriptUrls.Add(this.GetProductionAssetUrl(assets.vendor.js.ToString()));
                    scriptUrls.Add(this.GetProductionAssetUrl(assets.main.js.ToString()));
                    responseViewModel.StylesheetUrls = stylesheetUrls;
                    responseViewModel.JavascriptUrls = scriptUrls;
                }

            }
            return View(responseViewModel);
        }
    }
}