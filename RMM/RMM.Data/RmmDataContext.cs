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
            this.Account = this.GetTable<AccountEntity>();
            this.Category = this.GetTable<CategoryEntity>();
            this.Transaction = this.GetTable<Transaction>();
            this.Option = this.GetTable<Option>();
        }

        public Table<AccountEntity> Account { get; set; }

        public Table<CategoryEntity> Category { get; set; }

        public Table<Transaction> Transaction { get; set; }

        public Table<Option> Option { get; set; }
    }


}
