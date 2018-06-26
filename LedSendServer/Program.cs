
using Abp;
using LedSendServer.Common;
using Topshelf;

namespace LedSendServer
{
    class Program
    {
        
        static void Main(string[] args)
        {
            LedSendServer ledSendServer;


            using (var bootstrapt = AbpBootstrapper.Create<LedServerModule>())
            {
                bootstrapt.Initialize();
                ledSendServer = bootstrapt.IocManager.Resolve<LedSendServer>();
            }

            HostFactory.Run(a =>
            {
                log4net.Config.XmlConfigurator.Configure();

               // a.UseLog4Net();

                a.Service<LedSendServer>(b =>
                {
                    b.ConstructUsing(c => ledSendServer);
                    b.WhenStarted(d => d.Start());
                    b.WhenStopped(d => d.Stop());
                });

                a.RunAsLocalService();
                a.SetDescription("正元LED发送平台v1.0");
                a.SetDisplayName("正元LED发送平台v1.0");
                a.SetServiceName("ZY-LEDSendService");

               
            });
         
        }
    }
}
