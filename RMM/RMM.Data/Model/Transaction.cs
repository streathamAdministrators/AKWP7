using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;
using System.Data.Linq;


namespace RMM.Data.Model
{
    [Table(Name="Transaction")]
    public class Transaction
    {
        [Column(IsPrimaryKey=true)]
        public int Id { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Balance { get; set; }

        private EntityRef<AccountEntity> account = new EntityRef<AccountEntity>();
        [Association(Name="FK_Account_Transaction", Storage="account", ThisKey="AccountId",OtherKey="Id")]
        public AccountEntity Account 
        {
            get { return this.account.Entity; }
            set 
            {
                AccountEntity previousValue = this.account.Entity; 
                 
                 if (previousValue != value || this.account.HasLoadedOrAssignedValue == false)
                 {
                     if (previousValue != null)
                     {
                         this.account.Entity = null; 
                         previousValue.TransactionList.Remove(this);
                     } 
                     
                     this.account.Entity = value; 
                     
                     if (value != null)
                     {
                         value.TransactionList.Add(this);
                         this.Id = value.Id;
                     } 
                     else
                     {
                         this.Id = default(int);
                     }
                 }
            }
        }

        private EntityRef<CategoryEntity> category = new EntityRef<CategoryEntity>();
        [Association(Name = "FK_Category_Transaction", Storage = "category", ThisKey = "CategoryId", OtherKey = "Id")]
        public CategoryEntity Category 
        {
            get { return this.category.Entity; }
            set
            {
                CategoryEntity previousValue = this.category.Entity;

                if (previousValue != value || this.category.HasLoadedOrAssignedValue == false)
                {
                    if (previousValue != null)
                    {
                        this.category.Entity = null;
                        previousValue.TransactionList.Remove(this);
                    }

                    this.category.Entity = value;

                    if (value != null)
                    {
                        value.TransactionList.Add(this);
                        this.Id = value.Id;
                    }
                    else
                    {
                        this.Id = default(int);
                    }
                }
            }
        }
    }
}
