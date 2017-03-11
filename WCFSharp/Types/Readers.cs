using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;

namespace WCFSharp.Types
{
    internal static class StaticPointerWriter
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
    }

    internal static class StaticPointerReader
    {
        public static int ReadInteger(IntPtr MemoryPointer, ref int Position)
        {
            var intbytes = new byte[4];
            Marshal.Copy(MemoryPointer + Position, intbytes, 0, 4);
            var integer = BitConverter.ToInt32(intbytes, 0);
            Position += 4;

            return integer;
        }

        public static string ReadString(IntPtr MemoryPointer, ref int Position)
        {
            var intbytes = new byte[4];
            Marshal.Copy(MemoryPointer + Position, intbytes, 0, 4);
            Position += 4;

            var count = BitConverter.ToInt32(intbytes, 0);

            var stringbytes = new byte[count * 2];
            Marshal.Copy(MemoryPointer + Position, stringbytes, 0, stringbytes.Length);
            Position += stringbytes.Length;

            var str = Encoding.Unicode.GetString(stringbytes);
            return str;
        }

        public static byte[] ReadData(IntPtr MemoryPointer, ref int Position)
        {
            var intbytes = new byte[4];
            Marshal.Copy(MemoryPointer + Position, intbytes, 0, 4);
            Position += 4;

            var count = BitConverter.ToInt32(intbytes, 0);

            var bytes = new byte[count];
            Marshal.Copy(MemoryPointer + Position, bytes, 0, bytes.Length);
            Position += bytes.Length;

            return bytes;
        }
    }

    internal class OutBuffer : IDisposable
    {
        public IntPtr MemoryPointer;
        public int Size;

        public OutBuffer(params object[] objects)
        {
            Size = 0;

            // Calculate size
            foreach (var obj in objects)
            {
                if (obj is string)
                {
                    Size += 4 + 2 * (obj as string).Length;
                }
                else if (obj is byte[])
                {
                    Size += 4 + 2 * (obj as byte[]).Length;
                }
                else if (obj is int)
                {
                    Size += 4;
                }
            }

            MemoryPointer = Marshal.AllocHGlobal(Size);
            int offset = 0;
            // Write size
            foreach (var obj in objects)
            {
                if (obj is string)
                {
                    StaticPointerWriter.WriteString(MemoryPointer, ref offset, obj as string);
                }
                else if (obj is byte[])
                {
                    StaticPointerWriter.WriteData(MemoryPointer, ref offset, obj as byte[]);
                }
                else if (obj is int)
                {
                    StaticPointerWriter.WriteInteger(MemoryPointer, ref offset, (int)obj);
                }
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(MemoryPointer);
        }
    }
}
