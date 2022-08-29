using System.Timers;
using WechatPublicAccount.Controllers;
using WechatPublicAccount.Static;

namespace WechatPublicAccount;

public static class Countdown
{
    private static System.Timers.Timer timer = new System.Timers.Timer();

    /// <summary>
    /// 每分钟调用一次 可在其中判断是否到达时间
    /// </summary>
    public static Action countdownEvent;

    public static void StartCountdown()
    {
        timer.Enabled = true;
        timer.Interval = 60000; //执行间隔时间,单位为毫秒;此时时间间隔为1分钟  
        timer.Start();
        timer.Elapsed += new(TimedPush);
    }

    static void TimedPush(object source, ElapsedEventArgs e)
    {
        countdownEvent?.Invoke();
    }
}