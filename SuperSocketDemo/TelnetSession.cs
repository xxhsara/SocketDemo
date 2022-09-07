using SuperSocket;
using SuperSocket.Channel;
using SuperSocket.Server;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketDemo
{
    // Socket 三大要素之一：AppSession 代表的是其他进程和当前进程建立的链接
    public class TelnetSession : AppSession
    {
        public TelnetSession()
        {

        }

        public string UserName { get; set; }

        /// <summary>
        /// 当有链接进来后触发
        /// </summary>
        /// <returns></returns>
        protected override async ValueTask OnSessionConnectedAsync()
        {
            Console.WriteLine($"{SessionID}:已连接....");

            ////ISessionContainer sessionContainer = base.Server.GetSessionContainer();

            string msg = $"Hello~~{DateTime.Now.ToString()}";

            byte[] sendByte = Encoding.ASCII.GetBytes(msg);
            ReadOnlyMemory<byte> data = new ReadOnlyMemory<byte>(sendByte);
            await ((IAppSession)this).SendAsync(data);

            await base.OnSessionConnectedAsync();
        }

        /// <summary>
        /// 当Session连接关闭的时候触发
        /// </summary>
        /// <param name="e"></param>
        /// <returns></returns>
        protected override ValueTask OnSessionClosedAsync(CloseEventArgs e)
        {
            Console.WriteLine($"{SessionID}:已关闭....");
            return base.OnSessionClosedAsync(e);
        }

        /// <summary>
        /// 关闭时触发
        /// </summary>
        /// <returns></returns>
        public override ValueTask CloseAsync()
        {
            Console.WriteLine($"{SessionID}:已关闭...");
            return base.CloseAsync();
        }

        /// <summary>
        /// 重置
        /// </summary>
        protected override void Reset()
        {
            base.Reset();
        }


    }
}
