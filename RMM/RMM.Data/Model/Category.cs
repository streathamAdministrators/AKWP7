using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RMM.Data.Model
{
    [Table(Name="Category")]
    public class Category
    {

        private EntitySet<Transaction> transactionRef;

        public Category()
        {
            this.transactionRef = new EntitySet<Transaction>(this.OnTransactionAdded, this.OnTransactionRemoved);

        }

        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Balance { get; set; }

        [Column]
        public string Color { get; set; }


        [Association(Name = "FK_Category_Transaction", Storage = "transactionRef", ThisKey = "ID", OtherKey = "CategoryID")]
        public EntitySet<Transaction> TransactionList
        {
            get { return this.transactionRef; }
        }


        private void OnTransactionAdded(Transaction transaction)
        {
            transaction.Category = this;
        }

        private void OnTransactionRemoved(Transaction transaction)
        {
            transaction.Category = this;
        } 
        

        [Column]
        public DateTime CreatedDate { get; set; }
    }
}
