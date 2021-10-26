using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace curso.api.tests.Configurations
{
    public interface ITestLoggerFactory
    {
        void WriteLine(string message);
    }
}
