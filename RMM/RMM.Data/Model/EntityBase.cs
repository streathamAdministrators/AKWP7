using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace RMM.Data.Model
{
    [Table(Name="EntityBase")]
    [InheritanceMapping(Code="Account",Type=typeof(AccountEntity))]
    [InheritanceMapping(Code="Transaction", Type=typeof(Transaction))]
    public abstract class EntityBase
    {
        [Column(IsPrimaryKey = true, IsDiscriminator=true)]
        public int ID { get; set; }

        [Column]
        public string Name { get; set; }

        [Column]
        public double Balance { get; set; }
    }
}
