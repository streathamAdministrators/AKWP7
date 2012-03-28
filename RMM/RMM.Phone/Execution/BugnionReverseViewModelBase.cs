using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;

namespace RMM.Phone.Execution
{
    public class BugnionReverseViewModelBase:ViewModelBase
    {
        public BugnionReverseViewModelBase()
        {

        }

        public void NavigateTo(string urlPattern, int? id)
        {
            var rootFrame = (App.Current as App).RootFrame;

            if (id.HasValue)
            {
                rootFrame.Navigate(new System.Uri(string.Format(urlPattern, id), System.UriKind.Relative));
            }
            else
            {
                rootFrame.Navigate(new System.Uri(urlPattern, System.UriKind.Relative));
            }
        }

        public void ExecuteSafeDispatcher(params Action[] actionsAExecuter)
        {
            foreach (var action in actionsAExecuter)
            {
                Deployment.Current.Dispatcher.BeginInvoke(action);
            }
        }
    }
}
