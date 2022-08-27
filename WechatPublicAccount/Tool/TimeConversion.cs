namespace WechatPublicAccount;

public static class TimeConversion
{
    
    //传入一个时间 用当前时间减去这个时间 返回一个时间差
    public static string GetTimeDifference(string time)
    {
        DateTime now = DateTime.Now;
        DateTime time1 = Convert.ToDateTime(time);
        TimeSpan ts = now - time1;
        return (ts.Days + (ts.Hours > 0 ? 1 : 0)).ToString() + "天";
    }
    
    /// <summary>
    /// 传入生日 计算距离下次生日的天数
    /// </summary>
    /// <param name="birthday"></param>
    /// <returns></returns>
    public static string GetNextBirthday(string birthday)
    {
        System.DateTime now = System.DateTime.Now;
        System.DateTime nextBirthday = System.DateTime.Parse(birthday);
        nextBirthday = nextBirthday.AddYears(GetAge(birthday) + 1);

        System.TimeSpan ts = nextBirthday - now;
        return (ts.Days + (ts.Hours > 0 ? 1 : 0)).ToString() + "天";
    }

    /// <summary>
    /// 计算年龄
    /// </summary>
    /// <param name="birthday">生日</param>
    /// <returns></returns>
    public static int GetAge(string birthday)
    {
        System.DateTime now = System.DateTime.Now;
        System.DateTime birth = System.DateTime.Parse(birthday);
        int age = now.Year - birth.Year;
        if (now.Month < birth.Month || (now.Month == birth.Month && now.Day < birth.Day))
        {
            age--;
        }

        return age;
    }
}