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
    // Socket 三大要素之一：Command 命令，都是基于命令来完成的
    [CustomerLogFilterAttribute]
    public class ChatCommand : IAsyncCommand<IAppSession, StringPackageInfo>, ICommand
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
            string toUser = package.Parameters[0]; //ToUser 目标方需要对方的SessionId
            string toMsg = package.Parameters[1];

            ISessionContainer sessionContainer = session.Server.GetSessionContainer();


            IAppSession toUserSession = null;
            foreach(var se in sessionContainer.GetSessions())
            {
                var telnetSession = (TelnetSession)se;
                if(telnetSession.UserName==toUser)
                {
                    toUserSession = telnetSession;
                }
            }

            byte[] sendByte = Encoding.UTF8.GetBytes(toMsg);

            ReadOnlyMemory<byte> data = new ReadOnlyMemory<byte>(sendByte);


            await toUserSession.SendAsync(sendByte);


            #region 发送完消息之后回发给自己
            //string toMsg = package.Parameters[0];
            //byte[] sendByte = Encoding.UTF8.GetBytes(toMsg);

            //await session.SendAsync(sendByte);
            #endregion


            await Task.CompletedTask;

        }
    }
}
