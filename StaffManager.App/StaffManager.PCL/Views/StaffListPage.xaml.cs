using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.RestClient;
using StaffManager.PCL.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManager.PCL.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class StaffListPage : ContentPage
	{
		public StaffListPage ()
		{
			InitializeComponent ();
		}

	    protected override void OnAppearing()
	    {
	        loadStaffListFromServer();
	    }

	    private async void loadStaffListFromServer()
	    {
	        try
	        {
	            ProgressRing.IsRunning = true;
	            ProgressRing.IsVisible = true;
                RestClient<Staff> restClient = new RestClient<Staff>();
	            var listStaff = await restClient.GetAsync("http://192.168.0.3/staffmanager/api/StaffsAPI");
                ObservableCollection<StaffWithImage> staffListwithImage = new ObservableCollection<StaffWithImage>();
	            foreach (var item in listStaff)
	            {
	                ImageSource _imageSource = ImageSource.FromStream(()=> 
                        new MemoryStream(Convert.FromBase64String(item.Image)));

                    staffListwithImage.Add(new StaffWithImage
                    {
                        FullName = item.FullName,
                        Address = item.Address,
                        Phone = item.Phone,
                        Id = item.Id,
                        Image = _imageSource
                    });
	            }
	            StaffListView.ItemsSource = staffListwithImage;
	            ProgressRing.IsRunning = false;
	            ProgressRing.IsVisible = false;
            }
	        catch (Exception ex)
	        {
	            Debug.WriteLine("Loade From Server:" + ex.Message);
	            ProgressRing.IsRunning = false;
	            ProgressRing.IsVisible = false;
            }
	    }

	    private async void ButtonCreateNew_OnClicked(object sender, EventArgs e)
	    {
	        await Navigation.PushModalAsync(new NewStaffPage());
	    }
	}
}