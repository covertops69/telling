using MvvmCross.Core.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    public class Player : MvxViewModel
    {
        public int PlayerId { get; set; }
        public string Name { get; set; }
    }
}
