
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;

namespace TrackinFull2.Droid
{
    [Activity(Theme = "@style/Theme.Splash", MainLauncher = false, NoHistory = true)]
    public class SplashActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            SimulateStartup();
            // Create your application here
        }
        // Launches the startup task
        protected override void OnResume()
        {
            base.OnResume();
            SimulateStartup();
        }

        // Prevent the back button from canceling the startup process
        public override void OnBackPressed() { }

        // Simulates background work that happens behind the splash screen
        public void SimulateStartup()
        {
            StartActivity(new Intent(Application.Context, typeof(MainActivity)));
            //StartActivity(typeof(MainActivity));
        }
    }
}