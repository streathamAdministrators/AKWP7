﻿using System;
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
    public class AccountViewData : ViewDataBase
    {
        public int Id { get; set; }

        private string name;
        public string Name
        {
            get { return name; }
            set
            {
                name = value;
                IsEntityDataChanged = true;
            }
        }

        private string bankName;
        public string BankName
        {
            get { return bankName; }
            set
            {
                bankName = value;
                IsEntityDataChanged = true;
            }
        }




        public double Balance { get; set; }

        public Visibility Favorite { get; set; }

        public List<TransactionViewData> ListTransaction { get; set; }
    }
}
