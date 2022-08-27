namespace WechatPublicAccount;

public static class XmlSplice
{
    /// <summary>
    /// 普通文本消息
    /// </summary>
    public static string MessageText =>
        "<xml>\n" +
        "<ToUserName><![CDATA[{0}]]></ToUserName>\n" +
        "<FromUserName><![CDATA[{1}]]></FromUserName>\n" +
        "<CreateTime>{2}</CreateTime>\n" +
        "<MsgType><![CDATA[text]]></MsgType>\n" +
        "<Content><![CDATA[{3}]]></Content>\n" +
        "</xml>";
}