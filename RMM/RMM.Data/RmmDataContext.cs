using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq;
using RMM.Data.Model;

namespace RMM.Data
{
    public class RmmDataContext : DataContext
    {
        public const string CONNECTIONSTRING = "isostore:/rmmdatabase.sdf";

        public RmmDataContext(string connectionString)
            :base(connectionString)
        {
            this.Option = this.GetTable<Option>();
            this.Account = this.GetTable<Account>();
            this.Category = this.GetTable<Category>();
            this.Transaction = this.GetTable<Transaction>();
            
        }

        public Table<Account> Account { get; set; }

        public Table<Category> Category { get; set; }

        public Table<Transaction> Transaction { get; set; }

        public Table<Option> Option { get; set; }
    }


}
