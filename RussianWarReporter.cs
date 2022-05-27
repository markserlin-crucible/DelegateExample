using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunfireTests
{
    public class RussianWarReporter : WarReporter
    {
        /// <summary>
        /// All this feller does is listen out for gunfire and report when it "hears" one.
        /// Likely works for RT or the BBC
        /// </summary>
        /// <param name="name"></param>
        public RussianWarReporter(string name) : base(name)
        {
            // -- No setup, name is set in the base class
        }

        /// <summary>
        /// As an example, this is the "longhand" version.
        /// </summary>
        /// <returns>string</returns>
        protected override string FileAReport()
        {
            return "I heard NO shot!";
        }
    }
}
