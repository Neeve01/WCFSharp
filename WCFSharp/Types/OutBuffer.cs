using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp
{
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
                    ManagedIO.WriteString(MemoryPointer, ref offset, obj as string);
                }
                else if (obj is byte[])
                {
                    ManagedIO.WriteData(MemoryPointer, ref offset, obj as byte[]);
                }
                else if (obj is int)
                {
                    ManagedIO.WriteInteger(MemoryPointer, ref offset, (int)obj);
                }
            }
        }

        public void Dispose()
        {
            Marshal.FreeHGlobal(MemoryPointer);
        }
    }
}
