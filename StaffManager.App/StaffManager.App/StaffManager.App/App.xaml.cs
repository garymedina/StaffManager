using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using StaffManager.App.Views;
using Xamarin.Forms;

namespace StaffManager.App
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new StaffListPage();
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}
