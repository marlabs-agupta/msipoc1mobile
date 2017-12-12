using System;
using System.Collections.Generic;
using System.Text;

namespace MSIPoC1Mobile
{
    public static class ConfigSettings
    {
        private const string BaseUrl = "http://msipoc1api.azurewebsites.net/api/";

        public static string LoginAPI = BaseUrl + "Account/Login";
        public static string RegisterAPI = BaseUrl + "Account/Register";
     }
}
