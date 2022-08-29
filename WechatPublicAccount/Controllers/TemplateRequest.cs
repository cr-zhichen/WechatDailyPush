using WechatPublicAccount.Static;

namespace WechatPublicAccount.Controllers;

public static class TemplateRequest
{
    /// <summary>
    /// 每日推送的请求拼接
    /// </summary>
    /// <param name="_receiverId"></param>
    /// <returns></returns>
    public static async Task<dynamic> DailyPush(string _receiverId)
    {
        dynamic jo;
        EntityClass.NowWeather nowWeather = await ThirdPartyInterface.GetNowWeather();
        List<EntityClass.FutureWeather> futureWeathers = await ThirdPartyInterface.GetRecentWeather();

        jo = new
        {
            touser = _receiverId,
            template_id = Config.templateId[0],
            url = Config.jumpUrl,
            data = new
            {
                //这里写模板消息内容
                first = new
                {
                    value = await ThirdPartyInterface.GetAWord(),
                    color = "#173177"
                },
                city = new
                {
                    value = Config.city,
                    color = "#173177"
                },
                weather = new
                {
                    value = nowWeather.Weater,
                    color = "#173177"
                },
                nowTemperature = new
                {
                    value = $"{nowWeather.Temperature}℃",
                    color = "#173177"
                },
                wind = new
                {
                    value = $"{nowWeather.Winddirection} {nowWeather.Windpower}级",
                    color = "#173177"
                },
                wet = new
                {
                    value = $"{nowWeather.Humidity}％",
                    color = "#173177"
                },
                day1_wea = new
                {
                    value = (futureWeathers[0].dayweather == futureWeathers[0].nightweather
                                ? futureWeathers[0].dayweather.ToString()
                                : futureWeathers[0].dayweather + "转" + futureWeathers[0].nightweather)
                            + $" {futureWeathers[0].daytemp}℃~{futureWeathers[0].nighttemp}℃",
                    color = "#173177"
                },
                day2_wea = new
                {
                    value = (futureWeathers[1].dayweather == futureWeathers[1].nightweather
                                ? futureWeathers[1].dayweather.ToString()
                                : futureWeathers[1].dayweather + "转" + futureWeathers[1].nightweather)
                            + $" {futureWeathers[1].daytemp}℃~{futureWeathers[1].nighttemp}℃",
                    color = "#173177"
                },
                day3_wea = new
                {
                    value = (futureWeathers[1].dayweather == futureWeathers[1].nightweather
                                ? futureWeathers[1].dayweather.ToString()
                                : futureWeathers[1].dayweather + "转" + futureWeathers[1].nightweather)
                            + $" {futureWeathers[1].daytemp}℃~{futureWeathers[1].nighttemp}℃",
                    color = "#173177"
                },
                meetDate = new
                {
                    value = TimeConversion.GetTimeDifference(Config.meetDate),
                    color = "#173177"
                },
                togetherDate = new
                {
                    value = TimeConversion.GetTimeDifference(Config.loveTime),
                    color = "#173177"
                },
                name1 = new
                {
                    value = Config.name,
                    color = "#173177"
                },
                name2 = new
                {
                    value = Config.name2,
                    color = "#173177"
                },
                birthDate1 = new
                {
                    value = TimeConversion.GetNextBirthday(Config.birthday),
                    color = "#173177"
                },
                birthDate2 = new
                {
                    value = TimeConversion.GetNextBirthday(Config.birthday2),
                    color = "#173177"
                }
            }
        };

        return jo;
    }

}