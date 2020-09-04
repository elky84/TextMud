using SuperSocket.ClientEngine;
using System;
using WebSocket4Net;

namespace TestClient
{
    class Program
    {
        public class WebSocketClient
        {
            public WebSocket WebSocket { get; set; }

            public bool Init()
            {
                WebSocket = new WebSocket("ws://localhost:2012/");
                WebSocket.Opened += new EventHandler(WebSocket_Opened);
                WebSocket.Error += new EventHandler<ErrorEventArgs>(WebSocket_Error);
                WebSocket.Closed += new EventHandler(WebSocket_Closed);
                WebSocket.MessageReceived += new EventHandler<MessageReceivedEventArgs>(WebSocket_MessageReceived);
                WebSocket.Open();
                return true;
            }

            private void WebSocket_Opened(object sender, EventArgs e)
            {
                WebSocket.Send("Hello World!");
            }

            private void WebSocket_Error(object sender, ErrorEventArgs e)
            {
                Console.WriteLine(e.Exception.Message);
            }

            private void WebSocket_Closed(object sender, EventArgs e)
            {
                Console.WriteLine("socket closed");
            }

            private void WebSocket_MessageReceived(object sender, MessageReceivedEventArgs e)
            {
                Console.WriteLine(e.Message);
            }
        };

        static void Main(string[] args)
        {
            var webSocketClient = new WebSocketClient();
            webSocketClient.Init();

            Console.WriteLine("Input `exit` to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadLine().ToLower() != "exit");
        }
    }
}
