namespace WechatPublicAccount;

public class EntityClass
{
    public class WxGetInput
    {
        /// <summary>
        /// 签名
        /// </summary>
        public string signature { get; set; }

        /// <summary>
        /// 时间戳
        /// </summary>
        public string timestamp { get; set; }

        /// <summary>
        /// 随机数
        /// </summary>
        public string nonce { get; set; }

        /// <summary>
        /// 随机字符串
        /// </summary>
        public string echostr { get; set; }
    }

    public class xml
    {
        /// <summary>
        /// 接收方微信号
        /// </summary>
        public string? ToUserName { get; set; }

        /// <summary>
        /// 发送方微信号
        /// </summary>
        public string? FromUserName { get; set; }

        /// <summary>
        /// 消息创建时间
        /// </summary>
        public string? CreateTime { get; set; }

        /// <summary>
        /// 消息类型
        /// </summary>
        public string? MsgType { get; set; }

        /// <summary>
        /// 文本消息内容
        /// </summary>
        public string? Content { get; set; }

        /// <summary>
        /// 消息id 64位整形
        /// </summary>
        public string? MsgId { get; set; }

        /// <summary>
        /// 消息的数据ID（消息如果来自文章时才有）
        /// </summary>
        public string? MsgDataId { get; set; }

        /// <summary>
        /// 多图文时第几篇文章，从1开始（消息如果来自文章时才有）
        /// </summary>
        public string? Idx { get; set; }
    }

    public class NowWeather
    {
        /// <summary>
        /// 天气
        /// </summary>
        public string Weater { get; set; }

        /// <summary>
        /// 气温
        /// </summary>
        public string Temperature { get; set; }

        /// <summary>
        /// 风向
        /// </summary>
        public string Winddirection { get; set; }

        /// <summary>
        /// 风速
        /// </summary>
        public string Windpower { get; set; }

        /// <summary>
        /// 湿度
        /// </summary>
        public string Humidity { get; set; }
    }
    
    public class FutureWeather
    {
        /// <summary>
        /// 白天天气
        /// </summary>
        public string dayweather { get; set; }
        /// <summary>
        /// 晚上天气
        /// </summary>
        public string nightweather { get; set; }
        /// <summary>
        /// 白天温度
        /// </summary>
        public string daytemp { get; set; }
        /// <summary>
        /// 晚上温度
        /// </summary>
        public string nighttemp { get; set; }
    }
}