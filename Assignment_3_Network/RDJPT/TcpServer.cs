using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Newtonsoft.Json;

namespace Assignment_3_Network.RDJPT
{
    public class TcpServer
    {
        private bool _isRunning;
        private TcpListener _tcpServer;

        public TcpServer()
        {
            _tcpServer = new TcpListener(IPAddress.Loopback, 5000);

            Console.WriteLine("Starting TCP Server...");
            _tcpServer.Start();

            _isRunning = true;

            LoopClients();
            //if (Console.ReadKey().Key == ConsoleKey.Escape) server.Stop();
        }

        public void LoopClients()
        {
            while (_isRunning)
            {
                //Waiting for connection
                Console.WriteLine("Waiting for clients...");
                var client = _tcpServer.AcceptTcpClient();

                Thread t = new Thread(new ParameterizedThreadStart(HandleClient));
                t.Start(client);

            }
        }

        public void HandleClient(Object obj)
        {
            Console.WriteLine("Client connected! ");
            //Getting the client from the parameter
            TcpClient client = (TcpClient)obj;

            var stream = client.GetStream();

            var buffer = new Byte[client.ReceiveBufferSize];

            var rcnt = stream.Read(buffer, 0, buffer.Length);

            var message = Encoding.UTF8.GetString(buffer, 0, rcnt);

            //Passing the message and handling it
            var request = HandleRequest.ConvertRequest(message);

            var response = HandleRequest.ValidateRequest(request);

            buffer = Encoding.UTF8.GetBytes(response.ToJson());

            stream.Write(buffer, 0, buffer.Length);

            stream.Close();
        }
    }
}
