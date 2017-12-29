using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Plugin.Media;
using Plugin.Media.Abstractions;
using Plugin.RestClient;
using StaffManager.PCL.Models;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace StaffManager.PCL.Views
{
	[XamlCompilation(XamlCompilationOptions.Compile)]
	public partial class NewStaffPage : ContentPage
	{
	    private MediaFile _mediaFile;
	    private byte[] imgByteArray;
	    private string imgString64 = ""; //use to stre main image as string array

        public NewStaffPage ()
		{
			InitializeComponent ();
		}

	    private async void ButtonSave_OnClicked(object sender, EventArgs e)
	    {
            Staff newStaff = new Staff();
	        newStaff.FullName = EntryFullName.Text;
	        newStaff.Address = EntryAddress.Text;
	        newStaff.Phone = EntryPhone.Text;
	        newStaff.Image = imgString64;

	        try
	        {
	            MyProRing.IsRunning = true;
                RestClient<Staff> restClient = new RestClient<Staff>();
	            var results = await restClient.PostAsync(newStaff, "http://192.168.0.3/staffmanager/api/StaffsAPI");
	            if (results)
	            {
	                await DisplayAlert("Alert!", "New Staff added", "Close");
	                await Navigation.PopModalAsync();
	            }
	            else
	            {
	                await DisplayAlert("Warning!", "Add new error", "Close");
                }
	            MyProRing.IsRunning = false;
	        }
	        catch (Exception ex)
	        {
	            Debug.WriteLine("SERVER CONNECTION ERROR: " + ex.Message);
	        }
	    }

	    private async void ButtonTakePicture_OnClicked(object sender, EventArgs e)
	    {
	        await CrossMedia.Current.Initialize();

	        //Check if device supports taking a photo and display warning if not
	        if (!CrossMedia.Current.IsCameraAvailable || !CrossMedia.Current.IsTakePhotoSupported)
	        {
	            await DisplayAlert("No Camera", ":( No camera available.", "OK");
	            return;
	        }

	        //Take the photo and store in a temporary place
	        _mediaFile = await CrossMedia.Current.TakePhotoAsync(new StoreCameraMediaOptions
	        {
	            Directory = "Sample",
	            Name = "myImage.jpg",
	            PhotoSize = PhotoSize.Medium
	        });

	        if (_mediaFile == null)
	            return;

	        //Get the path of file on device
	        messageLabel.Text = _mediaFile.Path;

	        //Get the image and display it in UI
	        ImageAvatar.Source = ImageSource.FromStream(() =>
	        {
	            return _mediaFile.GetStream();
	        });

            //Convert image from camaera to ByteArray
	        imgByteArray = convertFileToByteArray(_mediaFile);
            //Convert ByteArray to Base64String
	        imgString64 = Convert.ToBase64String(imgByteArray);


	    }

	    private byte[] convertFileToByteArray(MediaFile mediaFile)
	    {
	        using (var memoryStream = new MemoryStream())
	        {
	            mediaFile.GetStream().CopyTo(memoryStream);
	            return memoryStream.ToArray();
	        }
	    }
	}
}
