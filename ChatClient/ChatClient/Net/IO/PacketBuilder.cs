﻿using System;
using System.IO;
using System.Text;

namespace ChatClient.Net.IO
{
    internal class PacketBuilder
    {
        MemoryStream _ms;
        public PacketBuilder()
        {
            _ms = new MemoryStream();
        }

        public void WriteOpCode(byte opcode)
        {
            _ms.WriteByte(opcode);
        }

        public void WriteString(string msg)
        {
            var msgLength = msg.Length;
            _ms.Write(BitConverter.GetBytes(msgLength),0,msgLength);
            _ms.Write(Encoding.ASCII.GetBytes(msg),0,msgLength);
        }

        public byte[] GetPacketByte()
        {
            return _ms.ToArray();
        }
    }
}
