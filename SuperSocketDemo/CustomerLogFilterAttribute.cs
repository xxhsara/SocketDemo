using Newtonsoft.Json;
using SuperSocket.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SuperSocketDemo
{
    public class CustomerLogFilterAttribute : CommandFilterAttribute
    {
        public override void OnCommandExecuted(CommandExecutingContext commandContext)
        {
            Console.WriteLine($"After Command:{JsonConvert.SerializeObject(commandContext.Package)}");
        }

        public override bool OnCommandExecuting(CommandExecutingContext commandContext)
        {
            Console.WriteLine($"Before Command:{JsonConvert.SerializeObject(commandContext.Package)}");
            return true;
        }
    }
}
