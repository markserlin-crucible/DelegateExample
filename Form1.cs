using System;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace GunfireTests
{
    public partial class Form1 : Form
    {
        // This is a new Type that we can assign a method to.
        // It's basically a pointer to any function we want,
        // that (in this case) takes no args and returns void.
        // Can be named anything...
        delegate void CSharpStyleFunctionPointer();

        /// <summary>
        /// This is an INSTANCE of ^^^ that TYPE ie Delegate ^^^
        /// It's called continuously in a timer or other update loop.
        /// </summary>
        CSharpStyleFunctionPointer myReloadAnimator = null;

        /// <summary>
        /// These chaps will listen for the sound of gunfire,
        /// so I can do an example of Object Orientation (they both derive from "Reporter")
        /// and show how a basic event works :-)
        /// </summary>
        WarReporter warReporter;
        RussianWarReporter russianWarReporter;

        /// <summary>
        /// This event is going to be Invoked (fired off) when we "pull the trigger"
        /// </summary>
        public event EventHandler ShotFired;


        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            // Standard init
            InitializeComponent();

            myReloadAnimator = null;

            // Make some reporters and hook up the event
            warReporter = new WarReporter("Wallace");
            ShotFired += warReporter.OnHearingAShot;
            russianWarReporter = new RussianWarReporter("Vladimir");
            ShotFired += russianWarReporter.OnHearingAShot;

            // Start running
            timer1.Start();
        }

        /// <summary>
        /// This is called by the timer in a continuous loop while the app runs, same as Unity's Update() is.
        /// Will be stopped etc automatically on program exit. 
        /// NOTE:
        /// Calling it with the ? means the runtime will skip it if it's null!
        /// This should be more efficient than either checking a bool or running even a void "DoNothing" method.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void timer1_Tick(object sender, EventArgs e)
        {
            // Instead of just having myReloadAnimator();
            myReloadAnimator?.Invoke();
        }

        /// <summary>
        /// when we click reload, assign the reloading function to the delegate
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnReload_Click(object sender, EventArgs e)
        {
            // Set up reload params
            progressBar1.Maximum = int.Parse(tbBullets.Text);   // set max
            progressBar1.Value %= progressBar1.Maximum;         // prevent overflow

            // start reloading by assigning the reload method to our delegate
            myReloadAnimator = ReLoadGun;
        }

        /// <summary>
        /// This is what we want to happen when we hit reload
        /// </summary>
        void ReLoadGun()
        {
            // display how many bullets
            textBox1.Text = "" + progressBar1.Value;

            // Introduce a delay between bullets.
            // Not sure how you're doing this in the Unity project?
            Thread.Sleep(int.Parse(tbInterval.Text));

            // Continue until loaded...
            if (progressBar1.Value < progressBar1.Maximum)
            {
                progressBar1.Value += 1;
            }
            // ... then disconnect the loader function 
            else
            {
                myReloadAnimator = null;// ie do nothing; could also set to = delegate{} ie a blank function body;
                textBox1.Text = "Full";
                progressBar1.Value = progressBar1.Maximum;
            }
        }


        /// <summary>
        /// When we click the fire button shoot a bullet
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnShoot_Click(object sender, EventArgs e)
        {
            // If we have at least one bullet
            if (progressBar1.Value > 0)
            {
                ShootBullet();
            }
        }

        /// <summary>
        /// Do whatever it is that happens when we want to shoot a bullet
        /// </summary>
        private void ShootBullet()
        {
            myReloadAnimator = null;

            // Only gets fired off if someone is listening, otherwise does nothing, saving us processor cycles etc
            ShotFired?.Invoke(this, new EventArgs());

            progressBar1.Value -= 1;
            progressBar1.Update();

            textBox1.Text = "Bang!";
            textBox1.ForeColor = Color.White;
            textBox1.BackColor = Color.Red;
            textBox1.Update();

            int reloadInterval = int.Parse(tbInterval.Text);
            int flashBoxInterval = reloadInterval / 3;
            Thread.Sleep(flashBoxInterval);

            textBox1.Text = "" + progressBar1.Value;
            textBox1.ForeColor = Color.Black;
            textBox1.BackColor = Color.White;
            textBox1.Update();

            Thread.Sleep(reloadInterval - flashBoxInterval);

            myReloadAnimator = ReLoadGun;
        }


    }
}
