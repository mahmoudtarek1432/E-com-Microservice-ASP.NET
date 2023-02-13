using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class ItegrationBaseEvent
    {
        public ItegrationBaseEvent()
        {
            Id = new Guid();
            CreationDate = DateTime.UtcNow;
        }

        public ItegrationBaseEvent(Guid Id, DateTime time)
        {
            Id = Id;
            CreationDate = time;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
