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

namespace RMM.Business.AccountService
{
    public class EditAccountCommand
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string BankName { get; set; }
    }
}
