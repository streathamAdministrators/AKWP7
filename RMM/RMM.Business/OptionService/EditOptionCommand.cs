using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace RMM.Business.OptionService
{
    public class EditOptionCommand
    {
        public bool? IsPassword { get; set; }

        public bool? IsSynchro { get; set; }

        public bool? IsPrimaryTile { get; set; }

        public int Favorite { get; set; }
    }
}
