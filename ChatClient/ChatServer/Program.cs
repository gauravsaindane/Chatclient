using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace ChatServer
{
     class Program
    {
        Static TcpListener _listener;
        static void main(string[] args)
        {
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.start();

            var client = _listener.AcceptTcpClient();
            Console.WriteLine("Client has connected!");
        }
    }
}
