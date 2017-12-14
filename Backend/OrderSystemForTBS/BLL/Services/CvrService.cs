using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
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
                Console.WriteLine(response);
                //response = response.Replace("\"", string.Empty);
                var result = response.Split(",\"");

                var company = new Company()
                {
                    Vat = result[0].Split(":")[1].Replace("\"", String.Empty),
                    Name = result[1].Split(":")[1].Replace("\"", String.Empty),
                    Address = result[2].Split(":")[1].Replace("\"", String.Empty),
                    ZipCode = int.Parse(result[3].Split(":")[1].Replace("\"", String.Empty)),
                };

                string[] CompanyArray = ((IEnumerable)company).Cast<Company>()
                    .Select(x => x.ToString())
                    .ToArray();

                return CompanyArray;
            }

            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return null;
            }
        }
    }

    public class Company
    {
        public string Vat { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public int ZipCode { get; set; }
    }
}




