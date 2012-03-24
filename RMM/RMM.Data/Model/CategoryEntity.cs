using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;

namespace RMM.Data.Model
{
    [Table(Name="Category")]
    public class CategoryEntity
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Balance { get; set; }

        [Column]
        public string Color { get; set; }

        private EntitySet<Transaction> transactionList = new EntitySet<Transaction>();
        [Association(Name = "FK_Category_Transaction", Storage = "transactionList", ThisKey = "id", OtherKey = "transactionid")]
        public EntitySet<Transaction> TransactionList
        {
            get { return this.transactionList; }
            set { transactionList = value; }
        }

        [Column]
        public DateTime CreatedDate { get; set; }
    }
}
