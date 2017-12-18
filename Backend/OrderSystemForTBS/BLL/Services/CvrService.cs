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
        public List<string> GetCompanyInfo(string query)
        {

            HttpClient client = new HttpClient();
            try
            {
                List<string> companyList = new List<string>();
                client.DefaultRequestHeaders.Add("User-Agent", "mit-navn");
                string response = client.GetStringAsync("http://cvrapi.dk/api?search=" + query + "&country=DK").Result;
                Console.WriteLine(response);
                var result = response.Split(",\"");

                response.Replace("null", "Ugyldigt CVR");

                var company = new Company()
                {
                    Vat = result[0].Split(":")[1].Replace("\"", String.Empty),
                    Name = result[1].Split(":")[1].Replace("\"", String.Empty),
                    Address = result[2].Split(":")[1].Replace("\"", String.Empty),
                    ZipCode = result[3].Split(":")[1].Replace("\"", String.Empty),
                    City = result[4].Split(":")[1].Replace("\"", String.Empty),
                    Phone = result[7].Split(":")[1].Replace("\"", String.Empty),
                    Email = result[8].Split(":")[1].Replace("\"", String.Empty),
                };

              
                companyList.Add(company.Vat);
                companyList.Add(company.Name);
                companyList.Add(company.Address);
                companyList.Add(company.City);
                companyList.Add(company.ZipCode);
                companyList.Add(company.Phone);
                companyList.Add(company.Email);

                for (int i = 0; i < companyList.Count; i++)
                {
                    if (companyList[i] == "null")
                    {
                        companyList[i] = "Ingen data oplyst";
                    }

                    byte[] bytes = Encoding.Default.GetBytes(companyList[i]);
                    companyList[i] = Encoding.UTF8.GetString(bytes);
                }

                return companyList;
            }

            catch (Exception e)
            {           
                List<string> emptyList = new List<string>();

                emptyList.Add("");
                emptyList.Add("Ugyldig CVR");
                emptyList.Add("");
                emptyList.Add("");
                emptyList.Add("");
                emptyList.Add("");
                emptyList.Add("");
                
                Console.WriteLine(e.ToString());
                return emptyList;
            }
        }
    }

    public class Company
    {
        public string Vat { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public string City { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}




