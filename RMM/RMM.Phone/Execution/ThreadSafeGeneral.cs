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
using RMM.Phone.Execution;

namespace RMM.Phone.Execution
{
    public class ThreadSafeGeneral: IThreadSafeGeneral
    {
        public ThreadSafeGeneral()
        {
            //LOG OU STATISTIQUES SUR LES REQUETES UTILISATEURS ET CONSULTATIONS.
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
