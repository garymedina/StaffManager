using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StaffManager.PCL.Views;
using Xamarin.Forms;

namespace StaffManager.PCL

{
	public partial class MainPage : MasterDetailPage
	{
		public MainPage()
		{
			InitializeComponent();
		    Detail = new NavigationPage(new StaffListPage());
		    IsPresented = false;
        }

	    private void ButtonStafList_OnClicked(object sender, EventArgs e)
	    {
	        Detail = new NavigationPage(new StaffListPage());
	        IsPresented = false;
	    }
	}
}
