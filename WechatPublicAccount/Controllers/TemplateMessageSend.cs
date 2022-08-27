using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using WechatPublicAccount.Static;

namespace WechatPublicAccount.Controllers;

public class TemplateMessageSend
{
    private static string access_token;

    /// <summary>
    /// 获取微信Token
    /// </summary>
    public static async Task ObtainToken(string receiverId)
    {
        try
        {
            HttpRequestMessage response = new HttpRequestMessage(
                HttpMethod.Get,
                $"https://api.weixin.qq.com/cgi-bin/token?grant_type=client_credential&appid={Config.appid}&secret={Config.secret}");
            response.Headers.Add("Accept", "application/json");
            HttpClient client = new HttpClient();
            HttpResponseMessage result = await client.SendAsync(response);
            string json = await result.Content.ReadAsStringAsync();
            JObject jo = JObject.Parse(json);

            if (jo["errcode"] != null)
            {
                throw new Exception(jo["errmsg"].ToString());
            }

            access_token = jo["access_token"]?.ToString() ?? "";
            Console.WriteLine($"accessToken:{access_token}");

            if (!string.IsNullOrEmpty(receiverId))
            {
                SendTemplateMessage(receiverId);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    /// <summary>
    /// 发送模板消息
    /// </summary>
    /// <param name="receiverId">接收者openid 为空则为群发</param>
    public static async Task SendTemplateMessage(string receiverId = "")
    {
        try
        {
            dynamic jo = new { };
            //群发消息
            if (String.IsNullOrEmpty(receiverId))
            {
                foreach (var item in Config.receiverId)
                {
                    await Send(item);
                }
            }
            else
            {
                await Send(receiverId);
            }

            async Task Send(string _receiverId)
            {
                HttpRequestMessage response = new HttpRequestMessage(
                    HttpMethod.Post,
                    $"https://api.weixin.qq.com/cgi-bin/message/template/send?access_token={access_token}");

                response.Headers.Add("Accept", "application/json");

                EntityClass.NowWeather nowWeather = await ThirdPartyInterface.GetNowWeather();
                List<EntityClass.FutureWeather> futureWeathers = await ThirdPartyInterface.GetRecentWeather();

                jo = new
                {
                    touser = _receiverId,
                    template_id = Config.templateId,
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

                string serializeObject = JsonConvert.SerializeObject(jo);

                response.Content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage result = await client.SendAsync(response);
                string json = await result.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(json);

                Console.WriteLine($"{json}");
                if ((int)jObject["errcode"] == 40001 || (int)jObject["errcode"] == 42001)
                {
                    await ObtainToken(_receiverId);
                }
                else if ((int)jObject["errcode"] != 0)
                {
                    throw new Exception(jObject["errmsg"].ToString());
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}