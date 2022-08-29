using System.Timers;
using WechatPublicAccount;
using WechatPublicAccount.Controllers;
using WechatPublicAccount.Static;

Config.ReadConfiguration(); //读取配置文件
Countdown.StartCountdown(); //开始倒计时

Countdown.countdownEvent += (() =>
{
    if (DateTime.Now.Hour == Config.hour && DateTime.Now.Minute == Config.minute)
    {
        TemplateMessageSend.SendTemplateMessage(TemplateRequest.DailyPush);
    }
});

// TemplateMessageSend.SendTemplateMessage(TemplateRequest.DailyPush);

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers().AddXmlSerializerFormatters();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();


app.Run();