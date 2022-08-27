using System.Text;
using System.Xml;
using Microsoft.AspNetCore.Mvc;
using WechatPublicAccount.Static;

namespace WechatPublicAccount.Controllers;

[ApiController]
[Route("[controller]")]
[Consumes("text/xml")]
public class Wx : ControllerBase
{
    /// <summary>
    /// 验证消息来自微信服务器
    /// </summary>
    [HttpGet]
    [Produces("text/plain")]
    public string WX([FromQuery] EntityClass.WxGetInput getInput)
    {
        string[] arr = { Config.token, getInput.timestamp, getInput.nonce };
        Array.Sort(arr);
        string str = string.Join("", arr);
        var sha1 = System.Security.Cryptography.SHA1.Create();
        var sha1Arr = sha1.ComputeHash(Encoding.UTF8.GetBytes(str));
        StringBuilder enStr = new StringBuilder();
        foreach (var b in sha1Arr)
        {
            enStr.AppendFormat("{0:x2}", b);
        }

        if (getInput.signature == enStr.ToString())
        {
            return getInput.echostr;
        }
        else
        {
            return "false";
        }
    }

    /// <summary>
    /// 接收微信服务器消息
    /// </summary>
    /// <returns></returns>
    [HttpPost]
    public async Task<string> WX([FromBody] EntityClass.xml xmldoc)
    {
        string responseContent = string.Format(
            XmlSplice.MessageText,
            xmldoc.FromUserName,
            xmldoc.ToUserName,
            DateTime.Now.Ticks,
            xmldoc.Content);

        if (xmldoc.MsgType != "event")
        {
            TemplateMessageSend.SendTemplateMessage(xmldoc.FromUserName);
        }

        return responseContent;
    }
}