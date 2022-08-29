using Newtonsoft.Json.Linq;

namespace WechatPublicAccount.Static;

public static class Config
{
    public static void ReadConfiguration()
    {
        string path = System.AppDomain.CurrentDomain.BaseDirectory + "Configuration/config.json";
        string json = System.IO.File.ReadAllText(path);
        JObject jObject = JObject.Parse(json);

        var personalInformation = jObject["personalInformation"];
        city = personalInformation["city"].ToString();
        name = personalInformation["name"].ToString();
        birthday = personalInformation["birthday"].ToString();
        name2 = personalInformation["name2"].ToString();
        birthday2 = personalInformation["birthday2"].ToString();
        loveTime = personalInformation["loveTime"].ToString();
        meetDate = personalInformation["meetDate"].ToString();

        var timeJson = jObject["timer"];
        hour = int.Parse(timeJson["hour"].ToString());
        minute = int.Parse(timeJson["minute"].ToString());

        var wxJson = jObject["wx"];
        token = wxJson["token"].ToString();
        appid = wxJson["appid"].ToString();
        secret = wxJson["secret"].ToString();

        foreach (var item in wxJson["templateId"] ?? new JArray())
        {
            templateId.Add(item.ToString());
        }

        foreach (var item in wxJson["receiverId"] ?? new JArray())
        {
            receiverId.Add(item.ToString());
        }

        jumpUrl = wxJson["jumpUrl"].ToString();

        var thirdPartyJson = jObject["thirdParty"];
        aWordUrl = thirdPartyJson["aWordUrl"].ToString();
        aMapWeatherKey = thirdPartyJson["aMapWeatherKey"].ToString();
    }

    public static string city { get; set; }
    public static string name { get; set; }
    public static string birthday { get; set; }
    public static string name2 { get; set; }
    public static string birthday2 { get; set; }
    public static string loveTime { get; set; }
    public static string meetDate { get; set; }
    public static int hour { get; set; }
    public static int minute { get; set; }
    public static string token { get; set; }
    public static string appid { get; set; }
    public static string secret { get; set; }
    public static List<string> templateId { get; set; } = new List<string>();
    public static List<string> receiverId { get; set; } = new List<string>();
    public static string jumpUrl { get; set; }
    public static string aWordUrl { get; set; }
    public static string aMapWeatherKey { get; set; }
}