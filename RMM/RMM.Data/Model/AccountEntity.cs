using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RMM.Data.Model
{
    [Table(Name="Account")]
    public class AccountEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Balance { get; set; }
       
        [Column]
        public string BankName { get; set; }

        [Column]
        public string PhotoUrl { get; set; }

        private EntitySet<Transaction> transactionList = new EntitySet<Transaction>();
        [Association(Storage = "transactionList", ThisKey = "id", OtherKey = "transactionid")]
        public EntitySet<Transaction> TransactionList 
        {
            get { return this.transactionList; }
            set { transactionList = value; }
        }

        [Column]
        public DateTime CreatedDate { get; set; }

    }
}
