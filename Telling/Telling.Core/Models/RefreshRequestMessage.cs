using MvvmCross.Plugins.Messenger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Telling.Core.Models
{
    class RefreshRequestMessage : MvxMessage
    {
        public RefreshRequestMessage(object sender)
        : base(sender)
        {
        }
    }
}
