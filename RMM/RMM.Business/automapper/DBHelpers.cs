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
using System.Data.Linq;
using RMM.Data.Model;
using System.Linq.Expressions;

namespace RMM.Business.Helpers
{
    public class DBHelpers
    {
        public static DataLoadOptions GetConfigurationLoader<T>(params Expression<Func<T, object>>[] parametres)
        {

            DataLoadOptions options = new DataLoadOptions();

            foreach (var item in parametres)
            {
                options.LoadWith<T>(item);
            }

            return options;
        }
    }
}
