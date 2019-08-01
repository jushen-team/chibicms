# ChibiCms

ChibiCms是一个非常简单的，如果不是最简单的一个flat file cms系统。基于asp.net core 2.1编写，支持Linux Win MacOS。他完全没有数据库，所有的内容都在文件中。通过复制粘贴来部署，也可以通过复制粘贴来备份。它通过Markdown方式写作，支持github webhook，可以把您的内容存在github中，只要push一下，它会自动的拉去最新的内容。支持多个github仓库，可以实现多人维护不同的区域内容。支持利用文件夹建立树形结构和在其中导航。不需要修改程序，就可以实现切换换模板，对不同的内容应用不同的模板。整个工程代码少得可怜不到1000行吧，我也没数过。

## 如何部署
1. 下载或clone本项目，或着点这里下载release版本。
2. 如果您用的源码，请自己编译VS2017，装了core 2.1以上sdk就可以，编译的话是ChibiCmsWeb这个项目哈，release的话直接要到一个文件夹就可以。
3. 服务器需要装  .net core运行环境，[参考这个安装说明](https://dotnet.microsoft.com/learn/dotnet/hello-world-tutorial/install)
3. 把release的文件夹拷贝到您的服务器上，用下面的指令运行，不管您是啥系统windows linux都行
```bash
dotnet ChibiCmsWeb.dll
```
4. 建议您zailinux上用systemd建一个服务，怎么弄网上很多这里给一个例子：
    1. 创建`/etc/systemd/system/chibicms.service`这个文件
    2. 内容如下：
    ```bash
    
        [Unit]
        Description=chibicms

        [Service]
        WorkingDirectory=/home/jushen/chibicms/release
        ExecStart=/usr/bin/dotnet /home/jushen/chibicms/release/ChibiCmsWeb.dll
        Restart=always
        RestartSec=10  # Restart service after 10 seconds if dotnet service crashes
        SyslogIdentifier=biochase-staging
        User=www-data
        Environment=ASPNETCORE_ENVIRONMENT=Production
        Environment=DOTNET_PRINT_TELEMETRY_MESSAGE=false

        [Install]
        WantedBy=multi-user.target
    ```
    这里要注意，User这个字段一定要是对content下面所有的文件都有读写权限的，只读不行，因为修改日期之类的是系统自动生成要写进去的。  
    3. `sudo systemctl enable chibicms.service`  
        `sudo systemctl start chibicms.service`  



4. 好了已经好了，打开浏览器输入http://127.0.0.1:8003网址端口就可以使用了
5. 这个玩玩可以，如果您真的要放到internet上建议您用nginx做个反向代理，然后在加上https。

## 管理内容

这个东西的内容在wwwroot/contents下面，这个文件夹下面的内容都会被系统检索到。
1. 什么是内容  
内容就是一个文件夹，他兼备系统以网页的形式渲染出来。文件夹下面有两个非常重要的文件：

**content.md**: 这是一个markdown文件，就是您会显示在网页上的内容，文件名不能变，大小写敏感，请一定使用unicode编码，不然中文乱码。这个markdown会被渲染成HTML，如果您插入了图片，请把图片也放在这个文件夹下面，

**meta.json**: 这是这个内容的metadata，描述这个内容的，也可以存放一些其他额外的信息，先贴出文件内容，再详细解释一下(这个文件一样要是unicode编码)：
```json
{
  "WebPath": "not applicapable",
  "Title": "He Heh呵呵",
  "ChangeTime": "2019-06-10T19:46:34+08:00",
  "CreatedTime": "2019-03-24T22:45:57.0893043+08:00",
  "ViewedTimes": 0,
  "Template": null,
  "Author": null,
  "Cover": null,
  "Extras": {
    "photos": [
      "2019-03-16-01-05-20.png",
      "DSC_0840.jpg",
      "DSC_0841.jpg",
      "DSC_0842.jpg",
      "DSC_0843.jpg"
    ]
  },
  "TopPath": "没用别管",
  "ContentType": "Content"
}
```
解释？？？？

**dmeta.json**
这个文件不是所有的内容都有，仅仅只有一种情况有，就是这个文件夹本身的类型是Content，但是他也是个目录在他的下面还有别的内容，就放这个文件来表示，他目前仅有Title是有意义的

2. 内容的类型
    1. **Content**  
        ？？
    2. **Directory**  
        ？？

3. 内容、目录的链接  
    ？？
4. 隐藏内容


##  如何配置

ChibiCms有两个配置文件一个是hostsettings.json这个是配置监听ip和端口的，另一个是配置其他有用的东西的
### **hostsettings.json**
hostsettings.json这个是配置监听ip和端口的，内容如下：
```json
{
  "urls": "http://*:8003"
}
```
这个我不用说什么了吧"urls"后面跟您需要监听的ip和端口就行。
### **appsettings.json**
先贴出内容：
```json
{
  "StartPath": "example",
  "RootPath": "root",
  "TemplatePath": "/ShutterView",
  "ContentPathEnvVar": "ContentPath",
  "UpdateScripts": {
    "the-real-jushen/literature": "jushenLiterature.sh",
    "test": "test.bat"
  }
}
```

## 如何通过Github更新
 ？？

## 如何修改模板

## 未来计划
1. 可能会加入Docker支持，现在已经有了，但是我懒得晚上您可以自己加上然后给我个pull request


