using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunfireTests
{
    public class Reporter
    {
        public Reporter(string name)
        {
            Name = name;
        }

        protected string Name { get; set; }
        
        protected virtual string FileAReport() => "Reporting.";
    }
}
