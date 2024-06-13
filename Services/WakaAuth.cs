
using Newtonsoft.Json;
using System.Net.Http;
using System.Text;
using System;
using SmartTime.Models;
using System.Threading.Tasks;


namespace SmartTime
{
    public class WakaAuth
    {
        HttpClient client = new HttpClient();

        public string Token { get; private set; } = string.Empty;
        public WakaAuth(string token)
        {
            Token = token;
        }


        public async Task<float> GetTodayTimeAsync()
        {
            DailyStat stat = await GetTodayStat();
            return stat.GetAllValueTimeInProjects();
        }


        public async Task<DailyStat> GetTodayStat()
        {
            try
            {
                var base64ApiKey = Convert.ToBase64String(Encoding.ASCII.GetBytes(Token));
                var client = new HttpClient();

                client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", base64ApiKey);

                var response = await client.GetAsync($"https://api.wakatime.com/api/v1/users/current/durations?date={DateTime.Now.ToString("yyyy-MM-dd")}");


                if (response.IsSuccessStatusCode)
                {
                    var content = await response.Content.ReadAsStringAsync();
                    return JsonConvert.DeserializeObject<DailyStat>(content);
                }
                else
                {
                    return new DailyStat();
                }
            }
            catch
            {
                return new DailyStat();
            }
        }


    }
    
}

