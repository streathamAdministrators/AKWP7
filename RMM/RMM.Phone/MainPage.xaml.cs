using Microsoft.Phone.Controls;

namespace RMM.Phone
{
    public partial class MainPage : PhoneApplicationPage
    {
        // Constructor
        public MainPage()
        {
            InitializeComponent();
        }

        private void appbar_button1_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/About.xaml",System.UriKind.Relative));
        }

        private void addCategoryMenuItem_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/CreateCategory.xaml", System.UriKind.Relative));
        }

        private void addAccountMenuItem_Click(object sender, System.EventArgs e)
        {
            NavigationService.Navigate(new System.Uri("/View/CreateAccount.xaml", System.UriKind.Relative));
        }
    }
}
