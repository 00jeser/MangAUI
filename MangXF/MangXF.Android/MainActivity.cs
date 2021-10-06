using System;

using Android.App;
using Android.Content.PM;
using Android.Runtime;
using Android.OS;
using Xamarin.Forms;
using Android.Views;

namespace MangXF.Droid
{
    [Activity(Label = "MangXF", Icon = "@mipmap/icon", Theme = "@style/MainTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation | ConfigChanges.UiMode | ConfigChanges.ScreenLayout | ConfigChanges.SmallestScreenSize )]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);
            MessagingCenter.Subscribe<Object, bool>(this, "Hide", (arg, isHaveBar) => {

                if (isHaveBar)
                {
                    Window.ClearFlags(WindowManagerFlags.Fullscreen);
                    Window.AddFlags(WindowManagerFlags.ForceNotFullscreen);
                }

                else
                {
                    Window.AddFlags(WindowManagerFlags.Fullscreen);
                    Window.ClearFlags(WindowManagerFlags.ForceNotFullscreen);
                }

            });

            Xamarin.Essentials.Platform.Init(this, savedInstanceState);
            global::Xamarin.Forms.Forms.Init(this, savedInstanceState);
            LoadApplication(new App());
        }
        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            Xamarin.Essentials.Platform.OnRequestPermissionsResult(requestCode, permissions, grantResults);

            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}