using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GunfireTests
{
    /// <summary>
    /// All this feller does is listen out for gunfire and report when it "hears" one.
    /// Likely works for Reuters or AlJazeera 
    /// </summary>
    public class WarReporter : Reporter
    {
        public WarReporter(string name) : base(name)
        {
            // -- No setup, name is set in the base class
        }

        // This function just returns a string
        // As an example, the => is shorthand for writing
        // {
        //      return "I heard a shot!";
        // } 
        protected override string FileAReport() => "I heard a shot!";


        /// <summary>
        /// Having these params means we can call this using an Event
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        public void OnHearingAShot(object sender, EventArgs e)
        {
            Console.WriteLine(Name + " says: \"" + FileAReport() + "\"");
        }

    }
}
