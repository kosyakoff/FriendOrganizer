using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FriendOrganizer.UI.Event
{
    using Prism.Events;

    public class AfterFriendSaveEvent : PubSubEvent<AfterFriendSaveEventArgs>
    {
    }
}
