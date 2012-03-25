using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using Microsoft.Phone.Controls;
using RMM.Phone.ViewModel;

namespace RMM.Phone.View
{
    public partial class EditCategory : PhoneApplicationPage
    {
        public EditCategory()
        {
            InitializeComponent();
        }

        private EditCategoryViewModel ViewModel
        {
            get
            {
                return DataContext as EditCategoryViewModel;
            }
        }

        protected override void OnNavigatedTo(System.Windows.Navigation.NavigationEventArgs e)
        {
            Dispatcher.BeginInvoke(() =>
            {
                var id = NavigationContext.QueryString.Values.First();
                ViewModel.SelectIndex(id);
            });
            base.OnNavigatedTo(e);
        }


    }
}