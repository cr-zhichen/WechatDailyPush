# WechatDailyPush

本项目为微信公众号每日卡片推送服务  
[项目源码](https://github.com/cr-zhichen/WechatDailyPush) [可执行文件下载](https://github.com/cr-zhichen/WechatDailyPush/releases)  

## 完成效果

每日10点30分进行自动推送  
![定时推送](https://img-cdn.ccrui.cn/2022/08/27/WechatIMG14.jpg)  

每次向测试号发送消息时，进行一次推送  
![被动推送](https://img-cdn.ccrui.cn/2022/08/27/WechatIMG15.jpg)  

## 准备工作

1. 一个具有公网ip的服务器
2. 申请[微信公众平台测试号](https://mp.weixin.qq.com/debug/cgi-bin/sandbox?t=sandbox/login)  
3. 申请[高德天气接口key](https://lbs.amap.com/dev/key/app)  

## 部署方式

下载构建完成的[可执行文件](https://github.com/cr-zhichen/WechatDailyPush/releases)  
修改`./Configuration/config.json`配置文件  

### 配置文件

定时执行  
示例为每日10点30分执行推送

![定时执行](https://img-cdn.ccrui.cn/2022/08/27/pJaVYW.png)  

基础信息  

![基础信息](https://img-cdn.ccrui.cn/2022/08/27/Hgfuih.png)  

微信信息  

需在微信测试号平台上传测试模板  

![测试模板](https://img-cdn.ccrui.cn/2022/08/27/Jamk4d.png)  

```
{{first.DATA}}

城市：{{city.DATA}}
实况天气：{{weather.DATA}}
气温：{{nowTemperature.DATA}}
风速：{{wind.DATA}}
湿度：{{wet.DATA}}
今天天气：{{day1_wea.DATA}}
明天天气：{{day2_wea.DATA}}
后天天气：{{day3_wea.DATA}}

♥遇见♥: {{meetDate.DATA}}
♥在一起♥: {{togetherDate.DATA}}

距离{{name1.DATA}}生日：{{birthDate1.DATA}}
距离{{name2.DATA}}生日：{{birthDate2.DATA}}
```

> 注！ 下方图片中templateId有误 应为 "templateId": ["微信模板消息id"]

![微信信息](https://img-cdn.ccrui.cn/2022/08/27/urhRDg.png)  
![微信信息2](https://img-cdn.ccrui.cn/2022/08/27/FtPcC6.png)  

第三方信息

![第三方信息](https://img-cdn.ccrui.cn/2022/08/27/Sqtrkk.png)  

### 部署服务器

将下载的文件夹上传到服务器 进入文件夹根路径后直接执行`WechatPublicAccount`文件即可 服务器监听端口为5000

若服务器为非Linux-x86 则需要下载`Portable-net6.0`版本 并在服务器安装[.net6运行时](https://dotnet.microsoft.com/zh-cn/download)  
安装完成后即可在文件夹根目录执行`dotnet WechatPublicAccount.dll` 服务器监听端口为5000  

也可直接使用宝塔面板进行部署  

![宝塔部署](https://img-cdn.ccrui.cn/2022/08/27/wi4oJp.png)  

## 完成部署并测试

在微信公众平台上填写服务器域名 若绑定域名为 `wx.ccrui.cn`则需要填写`http://wx.ccrui.cn/wx`  
![填写域名](https://img-cdn.ccrui.cn/2022/08/27/mAVNPu.png)  

绑定完成后，向微信测试号中随便发一条消息 测试号会回应该条消息并推送每日卡片  
每日的设定时间，微信会向配置文件中的全部用户进行一次推送  
