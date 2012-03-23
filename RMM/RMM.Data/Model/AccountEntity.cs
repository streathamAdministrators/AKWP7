using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RMM.Data.Model
{
    [Table(Name="Account")]
    public class AccountEntity : EntityBase
    {
       
        [Column]
        public string BankName { get; set; }

        [Column]
        public string PhotoUrl { get; set; }

        private EntitySet<Transaction> transactionList = new EntitySet<Transaction>();
        [Association(Name="FK_Account_Transaction", Storage="transactionList", ThisKey="ID", OtherKey="ID")]
        public EntitySet<Transaction> TransactionList 
        {
            get { return this.transactionList; }
            set { transactionList = value; }
        }

    }
}
