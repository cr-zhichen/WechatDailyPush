using Newtonsoft.Json.Linq;
using WechatPublicAccount.Static;

namespace WechatPublicAccount;

public class ThirdPartyInterface
{
    /// <summary>
    /// 获取一言
    /// </summary>
    /// <returns></returns>
    public static async Task<string> GetAWord()
    {
        try
        {
            HttpRequestMessage response = new HttpRequestMessage(
                HttpMethod.Get,
                Config.aWordUrl);
            response.Headers.Add("Accept", "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.SendAsync(response);
            string json = await result.Content.ReadAsStringAsync();
            return json;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// 获取实时天气信息
    /// </summary>
    /// <returns></returns>
    public static async Task<EntityClass.NowWeather> GetNowWeather()
    {
        try
        {
            string key = Config.aMapWeatherKey;
            int postalCode = await GetPostalCode();
            string extensions = "base";

            HttpRequestMessage response = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://restapi.amap.com/v3/weather/weatherInfo?key={key}&city={postalCode}&extensions={extensions}");
            response.Headers.Add("Accept", "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.SendAsync(response);
            string json = await result.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);
            var lives = jObject["lives"][0];

            EntityClass.NowWeather nowWeather = new();
            nowWeather.Weater = lives["weather"].ToString();
            nowWeather.Temperature = lives["temperature"].ToString();
            nowWeather.Winddirection = lives["winddirection"].ToString();
            nowWeather.Windpower = lives["windpower"].ToString();
            nowWeather.Humidity = lives["humidity"].ToString();

            return nowWeather;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// 获取今天到后天的天气
    /// </summary>
    /// <returns></returns>
    public static async Task<List<EntityClass.FutureWeather>> GetRecentWeather()
    {
        try
        {
            string key = Config.aMapWeatherKey;
            int postalCode = await GetPostalCode();
            string extensions = "all";

            HttpRequestMessage response = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://restapi.amap.com/v3/weather/weatherInfo?key={key}&city={postalCode}&extensions={extensions}");
            response.Headers.Add("Accept", "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.SendAsync(response);
            string json = await result.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);
            var casts = jObject["forecasts"][0]["casts"];

            List<EntityClass.FutureWeather> futureWeathers = new();
            foreach (var item in casts)
            {
                futureWeathers.Add(new()
                {
                    dayweather = item["dayweather"].ToString(),
                    nightweather = item["nightweather"].ToString(),
                    daytemp = item["daytemp"].ToString(),
                    nighttemp = item["nighttemp"].ToString()
                });
            }


            return futureWeathers;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private static int PostalCode = 0;

    /// <summary>
    /// 获取邮政编码 用以查询天气
    /// </summary>
    /// <returns></returns>
    private static async Task<int> GetPostalCode()
    {
        try
        {
            if (PostalCode != 0)
            {
                return PostalCode;
            }

            string key = Config.aMapWeatherKey;
            string city = Config.city;
            string extensions = "base";

            HttpRequestMessage response = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://restapi.amap.com/v3/config/district?keywords={city}&subdistrict=0&key={key}");
            response.Headers.Add("Accept", "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.SendAsync(response);
            string json = await result.Content.ReadAsStringAsync();

            JObject jObject = JObject.Parse(json);

            PostalCode = (int)jObject["districts"][0]["adcode"];

            return PostalCode;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}