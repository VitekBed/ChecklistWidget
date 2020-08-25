using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using System.Net.NetworkInformation;
using System.Net.Http;

namespace VitekWidget
{
    public static class Refresher
    {
        private const string address = "https://erp.cmiy.cz";
        public static string Response = string.Empty;
        public static void DoRefresh()
        {
            HttpClient client = new HttpClient();
            client.BaseAddress = new Uri(address);
            client.Timeout = new TimeSpan(0,0,1);
            try
            {
                HttpResponseMessage response = client.GetAsync(address).Result;
                Response = response.StatusCode.ToString();
            }
            catch(Exception e)
            {
                Response = e.ToString();
            }
        }
    }
}
