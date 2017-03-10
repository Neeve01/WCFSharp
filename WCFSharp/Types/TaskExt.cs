using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WCFSharp.Types
{
    internal static class TaskExt2
    {
        private static Task completedTask = Task.FromResult(false);
        public static Task CompletedTask()
        {
            return completedTask;
        }
    }
}
