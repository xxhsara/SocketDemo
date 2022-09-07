// See https://aka.ms/new-console-template for more information
using Autofac.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using SuperSocketDemo;
using System.Text;


//创建一个主机
var hostBuilder = (SuperSocketHostBuilder<StringPackageInfo>)SuperSocketHostBuilder.Create<StringPackageInfo, CommandLinePipelineFilter>();

//配置监听
hostBuilder = (SuperSocketHostBuilder<StringPackageInfo>)hostBuilder.ConfigureSuperSocket(options =>   //配置服务器
{
    options.Name = "SuperSocketDemoServer";
    options.Listeners = new List<ListenOptions>
    {
        new ListenOptions
        {
            Ip="Any",//本地Ip
            Port=9799 //端口号
        }
    };
});
hostBuilder.UsePackageDecoder<CustomerPackageDecoder>();

//配置Session
hostBuilder.UseSession<TelnetSession>();  //配置自定义的链接生效

//配置自定义的AppService
hostBuilder.UseHostedService<TelnetServer<StringPackageInfo>>();


//配置命令
hostBuilder.UseCommand<StringPackageInfo>(options =>
{
    options.AddCommand<ChatCommand>();
    options.AddCommand<LoginCommand>();

    options.AddGlobalCommandFilter<CustomerLogFilterAttribute>();
});

hostBuilder.UseInProcSessionContainer();

hostBuilder.UseServiceProviderFactory(new AutofacServiceProviderFactory());

IHost host = hostBuilder.Build();

host.StartAsync();





