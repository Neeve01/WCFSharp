using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WCFSharp
{
    internal static class ManagedIO
    {
        internal static void WriteInteger(IntPtr OutBuffer, ref int Offset, int Value)
        {
            Marshal.Copy(new int[1] { Value }, 0, OutBuffer + Offset, 1);
            Offset += 4;
        }

        internal static void WriteString(IntPtr OutBuffer, ref int Offset, string Value)
        {
            int Length = Value.Length;
            Marshal.Copy(new int[1] { Length }, 0, OutBuffer + Offset, 1);
            Offset += 4;

            var strbytes = Encoding.Unicode.GetBytes(Value);
            Marshal.Copy(strbytes, 0, OutBuffer + Offset, strbytes.Length);
            Offset += Length * 2;
        }

        internal static void WriteData(IntPtr OutBuffer, ref int Offset, byte[] Data)
        {
            int Length = Data.Length;
            Marshal.Copy(new int[1] { Length }, 0, OutBuffer + Offset, 1);
            Offset += 4;

            Marshal.Copy(Data, 0, OutBuffer + Offset, Length);
            Offset += Length;
        }

        internal static int ReadInteger(IntPtr InBuffer, ref int Offset)
        {
            var intbytes = new byte[4];
            Marshal.Copy(InBuffer + Offset, intbytes, 0, 4);
            var integer = BitConverter.ToInt32(intbytes, 0);
            Offset += 4;

            return integer;
        }

        internal static string ReadString(IntPtr InBuffer, ref int Offset)
        {
            var intbytes = new byte[4];
            Marshal.Copy(InBuffer + Offset, intbytes, 0, 4);
            Offset += 4;

            var count = BitConverter.ToInt32(intbytes, 0);

            var stringbytes = new byte[count * 2];
            Marshal.Copy(InBuffer + Offset, stringbytes, 0, stringbytes.Length);
            Offset += stringbytes.Length;

            var str = Encoding.Unicode.GetString(stringbytes);
            return str;
        }

        internal static byte[] ReadData(IntPtr InBuffer, ref int Offset)
        {
            var intbytes = new byte[4];
            Marshal.Copy(InBuffer + Offset, intbytes, 0, 4);
            Offset += 4;

            var count = BitConverter.ToInt32(intbytes, 0);

            var bytes = new byte[count];
            Marshal.Copy(InBuffer + Offset, bytes, 0, bytes.Length);
            Offset += bytes.Length;

            return bytes;
        }
    }
}
