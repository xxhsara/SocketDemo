using System.Net.WebSockets;

namespace WebSocketDemo
{
    public static class WebSocketException
    {
        public static async Task Echo(this HttpContext context,WebSocket webSocket)
        {
            CancellationToken cancellationToken = new CancellationToken();
            while (webSocket.State == WebSocketState.Open)
            {
                ArraySegment<byte> buf = new ArraySegment<byte>(new byte[2048]);
                WebSocketReceiveResult result = await webSocket.ReceiveAsync(buf, cancellationToken); //这一行不可以注释掉，不然的话前端会一直接收到消息
                await webSocket.SendAsync(buf, WebSocketMessageType.Text, true, CancellationToken.None);
            }
        }
    }
}
