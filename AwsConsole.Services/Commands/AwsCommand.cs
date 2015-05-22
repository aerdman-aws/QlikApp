using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AwsConsole.Services.Commands
{
    public abstract class AwsCommand<T>
    {
        public abstract T Execute();
    }
}
