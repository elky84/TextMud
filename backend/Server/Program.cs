using System;
using System.Text;
using WatsonWebsocket;

namespace Server
{
    class Program
    {
        public class WebSocket
        {
            public WatsonWsServer WebSocketServer { get; set; }

            public bool Init()
            {
                WebSocketServer = new WatsonWsServer("127.0.0.1", 2012, false);
                WebSocketServer.ClientConnected += ClientConnected;
                WebSocketServer.ClientDisconnected += ClientDisconnected;
                WebSocketServer.MessageReceived += MessageReceived;
                WebSocketServer.Start();
                return true;
            }

            void ClientConnected(object sender, ClientConnectedEventArgs args)
            {
                Console.WriteLine("Client connected: " + args.IpPort);
                var result = WebSocketServer.SendAsync(args.IpPort, "Test").Result;
            }

            void ClientDisconnected(object sender, ClientDisconnectedEventArgs args)
            {
                Console.WriteLine("Client disconnected: " + args.IpPort);
            }

            void MessageReceived(object sender, MessageReceivedEventArgs args)
            {
                Console.WriteLine("Message received from " + args.IpPort + ": " + Encoding.UTF8.GetString(args.Data));
                var result = WebSocketServer.SendAsync(args.IpPort, Encoding.UTF8.GetString(args.Data)).Result;
                Console.WriteLine("Echo " + result);
            }
        }

        static void Main(string[] args)
        {
            var webSocket = new WebSocket();
            webSocket.Init();

            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    // Do something
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);
        }
    }
}
