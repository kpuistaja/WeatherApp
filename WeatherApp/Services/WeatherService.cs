using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using WeatherApp.Models;

namespace WeatherApp.Services
{
    public class WeatherService
    {
        const string ApiKey = "972d2ee9c57026a9afc7d7b7c0ffd069";

        public async Task<WeatherInfo> GetCityWeather(string city)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync
                ($"https://api.openweathermap.org/data/2.5/weather?q={city}&units=metric&appid={ApiKey}");
                var data = JsonConvert.DeserializeObject<WeatherInfo>(response);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public async Task<Byte[]> GetImageFromUrl(string url)
        {
            using (var client = new HttpClient())
            {
                var msg = await client.GetAsync(url);
                if(msg.IsSuccessStatusCode)
                {
                    var byteArray = await msg.Content.ReadAsByteArrayAsync();
                    return byteArray;
                }
                return null;
            }
        }

        public async Task<WeatherForecast> GetCityWeatherForecast(string city)
        {
            var client = new HttpClient();

            try
            {
                var response = await client.GetStringAsync($"https://api.openweathermap.org/data/2.5/forecast?q={city}&&units=metric&appid={ApiKey}");
                var data = JsonConvert.DeserializeObject<WeatherForecast>(response);
                return data;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}