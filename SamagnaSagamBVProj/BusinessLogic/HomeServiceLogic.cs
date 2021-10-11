using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using SamagnaSagamBVProj.Models;

namespace SamagnaSagamBVProj.BusinessLogic
{
    public class HomeServiceLogic
    {
        public string data = string.Empty;
        public string result = string.Empty;
        public readonly ILogger _logger;
        private readonly HomeConfig _homeConfig;

        public HomeServiceLogic(ILogger<HomeServiceLogic> logger, HomeConfig homeConfig)
        {
            this._logger = logger;
            _homeConfig = homeConfig;
        }

        public async Task<string> GetDataAsync()
        {
            try
            {
                HttpResponseMessage apiresponse = await GetConnection();
                if (apiresponse.IsSuccessStatusCode)
                {
                    _logger.LogInformation("Successfully connected to the endpoint");
                    result = GetDataByAge(apiresponse);

                    return "age:count" + "\n" + result;

                }
                else
                {
                    _logger.LogError("Couldn't connect to the endpoint properly");
                    return "";
                }


            }
            catch (Exception ex)

            {
                _logger.LogError("Unexpected error occurred" + ex.ToString() + ex.InnerException.ToString());
            }

            return string.Empty;
        }

        public string GetDataByAge(HttpResponseMessage apiresponse)
        {
            try
            {
                _logger.LogInformation("Connected to the endpoint successfully");
                apiresponse.EnsureSuccessStatusCode();
                data = apiresponse.Content.ReadAsStringAsync().Result;

                var users = data != null
                            ? JsonConvert.DeserializeObject<Userss>(data)
                            : null;

                var groupedUsersBasedOnAge = users != null
                                             ? users.Users.GroupBy(a => a.age).Select(x => new { Key = x.Key, value = x.Count() }).OrderByDescending(a => a.value).ToList()
                                             : null;
                if (groupedUsersBasedOnAge != null)
                {
                    foreach (var eachAge in groupedUsersBasedOnAge)
                    {
                        result = result + eachAge.Key + ":" + eachAge.value + "," + "\n";
                    }
                    return result;
                }
            }
            catch (Exception ex)
            {
                _logger.LogError("Exception during processing the request" + ex.ToString());
            }
            
            return string.Empty;
        }

        public async Task<HttpResponseMessage> GetConnection()
        {
            try
            {
                HttpClient client = new HttpClient();
                var httpRequestMessage = new HttpRequestMessage()
                {
                    RequestUri = new Uri(_homeConfig.url + _homeConfig.path)

                };

                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

                var apiresponse = await client.SendAsync(httpRequestMessage);
                _logger.LogInformation("Successfully connection");
                return apiresponse;
            }catch(Exception ex)
            {
                _logger.LogError("Couldn't connect to the url properly", ex.ToString());
            }
            return null;
        }
    }
}
