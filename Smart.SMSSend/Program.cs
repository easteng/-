using Abp;
using DT.Data.Core.Common;
using System;
using System.Linq;
using Topshelf;

namespace Smart.SMSSend
{
    class Program
    {
        static void Main(string[] args)
        {
            SmsSendService smsSendService;
            using (var bootstrapt = AbpBootstrapper.Create<BaseModule>())
            {
                bootstrapt.Initialize();
                smsSendService = bootstrapt.IocManager.Resolve<SmsSendService>();
            }

            HostFactory.Run(a =>
            {
                a.Service<SmsSendService>(b =>
                {
                    b.ConstructUsing(c => smsSendService);
                    b.WhenStarted(d => d.Start());
                    b.WhenStopped(d => d.Stop());
                });

                a.RunAsLocalService();

                a.SetDescription("正元统一短信发送平台v1.0");
                a.SetDisplayName("正元统一短信发送平台v1.0");
                a.SetServiceName("ZY-SMSSendService");
            });
        }
    }
}
