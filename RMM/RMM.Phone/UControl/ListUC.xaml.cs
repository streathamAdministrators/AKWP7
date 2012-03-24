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
using System.Collections;

namespace RMM.Phone.UControl
{
    public partial class ListUC : UserControl
    {
        public ListUC()
        {
            InitializeComponent();
            LayoutRoot.DataContext = this;
        }

        public static readonly DependencyProperty LBItemsProperty = DependencyProperty.Register("LBItems", typeof(IEnumerable), typeof(ListUC), null);

        public IEnumerable LBItems
        {
            get { return (IEnumerable)GetValue(LBItemsProperty); }
            set { SetValue(LBItemsProperty, value); }
        }
    }
}
