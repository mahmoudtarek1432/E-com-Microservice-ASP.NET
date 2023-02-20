using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EventBus.Messages.Events
{
    public class IntegrationBaseEvent
    {
        public IntegrationBaseEvent()
        {
            Id = new Guid();
            CreationDate = DateTime.UtcNow;
        }

        public IntegrationBaseEvent(Guid Id, DateTime time)
        {
            this.Id = Id;
            CreationDate = time;
        }

        public Guid Id { get; private set; }
        public DateTime CreationDate { get; private set; }
    }
}
