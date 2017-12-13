using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using Newtonsoft.Json;


namespace BLL.Services
{
    public class CvrService
    {
        public string[] GetCompanyInfo(string query)
        {
            HttpClient client = new HttpClient();
            try
            {
                client.DefaultRequestHeaders.Add("User-Agent", "mit-navn");

                string response = client.GetStringAsync("http://cvrapi.dk/api?search=" + query + "&country=DK").Result;

                response = response.Replace('"', ' ');

                var result = response.Split(",");

                for (int i = 1; i < result.Length; i++)
                {
                    var currentResult = result[i];

                    var indexOf = currentResult.IndexOf(":");

                      if (currentResult.Contains("null"))
                    {
                        currentResult = currentResult.Replace("null", "Ingen data oplyst");
                    }

                    currentResult = currentResult.Substring(indexOf + 2);
                    currentResult = currentResult.Trim();

                    if (currentResult.Contains("\\u00f8"))
                    {
                        currentResult = currentResult.Replace("\\u00f8", "ø");
                    }

                    result[i] = currentResult;
                }

                return result;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }
}




