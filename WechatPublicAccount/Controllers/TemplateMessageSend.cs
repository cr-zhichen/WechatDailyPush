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
    private static async Task ObtainToken(Func<string, dynamic> request, string receiverId)
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
                SendTemplateMessage(request, receiverId);
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
    public static async Task SendTemplateMessage(Func<string, dynamic> request, string receiverId = "")
    {
        try
        {
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

                var jo = await request(_receiverId);
                string serializeObject = JsonConvert.SerializeObject(jo);

                response.Content = new StringContent(serializeObject, Encoding.UTF8, "application/json");
                HttpClient client = new HttpClient();
                HttpResponseMessage result = await client.SendAsync(response);
                string json = await result.Content.ReadAsStringAsync();
                JObject jObject = JObject.Parse(json);

                Console.WriteLine($"{json}");
                if ((int)jObject["errcode"] == 40001 ||
                    (int)jObject["errcode"] == 42001 ||
                    (int)jObject["errcode"] == 41001)
                {
                    await ObtainToken(request, _receiverId);
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