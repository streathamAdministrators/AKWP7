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

        private Nullable<int> accountID;
        private Nullable<int> categoryID;

        private EntityRef<AccountEntity> AccountRef = new EntityRef<AccountEntity>();
        private EntityRef<CategoryEntity> CategoryRef = new EntityRef<CategoryEntity>();


        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public string Description { get; set; }

        [Column]
        public double Amount { get; set; }


    [Column(Storage="accountID", DbType="Int")]
        public int? AccountID
    {
        get { return this.accountID;}
        set { this.accountID = value; }
    }

            [Column(Storage="categoryID", DbType="Int")]
        public int? CategoryID
    {
        get { return this.categoryID;}
        set { this.categoryID = value; }
    }



        [Association(Name="FK_Account_Transactions", Storage = "AccountRef", ThisKey = "AccountID", OtherKey = "ID", IsForeignKey = true)]
        public AccountEntity Account 
        {
            get { return this.AccountRef.Entity; }
            set 
            {
                AccountEntity previousValue = this.AccountRef.Entity; 
                 
                 if (previousValue != value || this.AccountRef.HasLoadedOrAssignedValue == false)
                 {
                     if (previousValue != null)
                     {
                         this.AccountRef.Entity = null; 
                         previousValue.TransactionList.Remove(this);
                     } 
                     
                     this.AccountRef.Entity = value; 
                     
                     if (value != null)
                     {
                         value.TransactionList.Add(this);
                         this.accountID = value.ID;
                     } 
                     else
                     {
                         this.AccountID = default(Nullable<int>);
                     }
                 }
            }
        }

        
        [Association(Name = "FK_Category_Transactions", Storage = "CategoryRef", ThisKey = "CategoryID", OtherKey = "ID", IsForeignKey = true)]
        public CategoryEntity Category 
        {
            get { return this.CategoryRef.Entity; }
            set
            {
                CategoryEntity previousValue = this.CategoryRef.Entity;

                if (previousValue != value || this.CategoryRef.HasLoadedOrAssignedValue == false)
                {
                    if (previousValue != null)
                    {
                        this.CategoryRef.Entity = null;
                        previousValue.TransactionList.Remove(this);
                    }

                    this.CategoryRef.Entity = value;

                    if (value != null)
                    {
                        value.TransactionList.Add(this);
                        this.categoryID = value.ID;
                    }
                    else
                    {
                        this.categoryID = default(Nullable<int>);
                    }
                }
            }
        }

        [Column]
        public DateTime CreatedDate { get; set; }
    }
}
