using SuperSocket;
using SuperSocket.Command;
using SuperSocket.ProtoBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketDemo
{
    
    public class LoginCommand : IAsyncCommand<IAppSession, StringPackageInfo>, ICommand
    {
        /// <summary>
        /// 客户端可以通过当前这个命令 基于IAppSession 发送消息给服务端
        /// </summary>
        /// <param name="session"></param>
        /// <param name="package"></param>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async ValueTask ExecuteAsync(IAppSession session, StringPackageInfo package)
        {
            
            string username = package.Parameters[0];
            string pwd = package.Parameters[1];

            //注册用户名
            var telnetSession = (TelnetSession)session;
            telnetSession.UserName = username;


            var msg = $"{username}登录成功";
            byte[] sendByte = Encoding.UTF8.GetBytes(msg);

            await session.SendAsync(sendByte);

            await Task.CompletedTask;

        }
    }
}
