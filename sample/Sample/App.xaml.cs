﻿using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using XamarinBridge.PCL;
using XamarinBridge.PCL.Types;

[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
namespace Sample
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            MainPage = new MainPage();
        }

        protected override void OnStart()
        {
            // Handle when your app starts
            NearPCL.GetNotificationHistory( (notifications) => {
                foreach(XCHistoryItem item in notifications) {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                }
            }, (error) => {

            } ); 
            // NearPCL.ProcessCustomTrigger("test_trigger");

        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}