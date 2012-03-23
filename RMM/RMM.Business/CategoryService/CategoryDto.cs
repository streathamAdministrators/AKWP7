using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using RMM.Business.TransactionService;

namespace RMM.Business.CategoryService
{
    public class CategoryDto
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public double Balance { get; set; }

        public string Color { get; set; }

        public List<TransactionDto> TransactionList { get; set; }

        public CategoryDto()
        {
            TransactionList = new List<TransactionDto>();
        }

    }
}
