using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Routing;

namespace JobPortal.Web.App_Start
{
    public class CommonPrefixProvider : DefaultDirectRouteProvider
    {
        private readonly string _commonPrefix;

        public CommonPrefixProvider(string commonPrefix)
        {
            this._commonPrefix = commonPrefix;
        }

        protected override string GetRoutePrefix(HttpControllerDescriptor controllerDescriptor)
        {
            var existingPrefix = base.GetRoutePrefix(controllerDescriptor);
            if (existingPrefix == null)
            {
                return this._commonPrefix;
            }

            return string.Format("{0}/{1}", this._commonPrefix, existingPrefix);
        }
    }
}