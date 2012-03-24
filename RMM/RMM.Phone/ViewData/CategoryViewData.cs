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
using System.Collections.Generic;

namespace RMM.Phone.ViewData.Account
{
    public class CategoryViewData
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string Color { get; set; }

        public List<TransactionViewData> ListTransaction { get; set; }

    }
}
