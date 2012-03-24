using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Business.TransactionService;

namespace RMM.Business.AccountService
{
    public class AccountDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string BankName { get; set; }

        public double Balance { get; set; }

        public string PhotoUrl { get; set; }
    }
}
