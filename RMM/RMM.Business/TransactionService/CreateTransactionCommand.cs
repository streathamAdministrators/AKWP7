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

namespace RMM.Business.TransactionService
{
    public class CreateTransactionCommand
    {
        //Obligatoire
        public string Name { get; set; }

        //Non Obligatoire
        public string Description { get; set; }

        //Obligatoire
        public double Amount { get; set; }

        //Non Obligatoire : Definition fonctionnelle
        public int? categoryId { get; set; }

        //Obligatoire
        public int accountId { get; set; }

    }
}
