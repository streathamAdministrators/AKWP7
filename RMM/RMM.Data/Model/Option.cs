﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Linq.Mapping;

namespace RMM.Data.Model
{
    [Table(Name="Option")]
    public class Option
    {
        [Column(IsPrimaryKey = true, IsDbGenerated = true)]
        public int id { get; set; }

        [Column]
        public bool IsPassword { get; set; }

        [Column]
        public bool IsSynchro { get; set; }

        [Column]
        public bool IsPrimaryTile { get; set; }

        [Column]
        public bool IsReport { get; set; }

        [Column]
        public bool IsComparator { get; set; }

        [Column]
        public DateTime ModifiedDate { get; set; }
    }
}
