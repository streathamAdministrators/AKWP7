using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;

namespace RMM.Business.TransactionService
{
    public class TransactionDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }
        public int AccountId { get; set; }
        public int CategoryId { get; set; }

        public DateTime CreatedDate { get; set; }
    }
}
