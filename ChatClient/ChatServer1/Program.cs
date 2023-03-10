using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using ChatServer1.Net.IO;

namespace ChatServer1
{
    class Program
    {
        static List<client> _users;
        static TcpListener _listener;
        static void Main(string[] args)
        {
            _users = new List<client>();
            _listener = new TcpListener(IPAddress.Parse("127.0.0.1"), 7891);
            _listener.Start();

            while (true)
            {
                var client = new client(_listener.AcceptTcpClient());
                _users.Add(client);

                /* Broadcast the connection to everyone on the server*/
                BroadcastConnection();
            }
        }

        static void BroadcastConnection()
        {
           foreach ( var user in _users)
            {
                foreach(var usr in _users)
                {
                    var broadcastpacket = new PacketBuilder();
                    broadcastpacket.WriteOpCode(1);
                    broadcastpacket.WriteString(usr.Username);
                    broadcastpacket.WriteString(usr.UID.ToString());
                    user.ClientSocket.Client.Send(broadcastpacket.GetPacketByte());
                }
            }
        }

        public static void BroadcastMessage(string message)
        {
            foreach(var user in _users)
            {
                var msgPacket = new PacketBuilder();
                msgPacket.WriteOpCode(5);
                msgPacket.WriteString(message);
                user.ClientSocket.Client.Send(msgPacket.GetPacketByte());
            }
        }

        public static void BroadcastDisconnect(string uid)
        {
                var disconnectdUser = _users.Where(x =>x.UID.ToString() == uid).FirstOrDefault();
            _users.Remove(disconnectdUser);
               foreach(var user in _users)
                {
                    var broadcastPacket=new PacketBuilder();
                     broadcastPacket.WriteOpCode(10);
                      broadcastPacket.WriteString(uid);
                      user.ClientSocket.Client?.Send(broadcastPacket.GetPacketByte());
                }

            BroadcastMessage($"[{disconnectdUser.Username}].Disconnected!");
        }
    }

}
