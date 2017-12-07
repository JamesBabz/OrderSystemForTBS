using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BLL.Services
{
    public  class DawaService
    {

       
        public string GetCityFromZipCode(int zipCode)
        {
            HttpClient client = new HttpClient();
            try
            {
                string response = client.GetStringAsync("http://dawa.aws.dk/postnumre/" + zipCode).Result;
                var test = response.Split(":");
                string city = test[4];
                var lastIndex = city.IndexOf(",", StringComparison.Ordinal) - 3;
                city = city.Substring(2, lastIndex);
                return city;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                return "";
            }
        }
    }
}
