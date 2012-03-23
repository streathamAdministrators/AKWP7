using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RMM.Data.Model
{
    [Table(Name="Category")]
    public class CategoryEntity : EntityBase
    {
        [Column]
        public string Color { get; set; }

        private EntitySet<Transaction> transactionList = new EntitySet<Transaction>();
        [Association(Name = "FK_Category_Transaction", Storage = "transactionList", ThisKey = "Id", OtherKey = "CategoryId")]
        public EntitySet<Transaction> TransactionList
        {
            get { return this.transactionList; }
            set { transactionList = value; }
        }
    }
}
