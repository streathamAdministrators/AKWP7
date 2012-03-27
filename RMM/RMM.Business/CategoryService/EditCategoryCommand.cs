using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace RMM.Business.CategoryService
{
   public class EditCategoryCommand
    {
        public int id { get; set; }

        public string Name { get; set; }

        public string Color { get; set; }
    }
}
