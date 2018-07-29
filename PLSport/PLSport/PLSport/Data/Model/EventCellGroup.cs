// Event Cell List with day title

using System.Collections.Generic;

namespace PLSport.Data.Model
{
    public class EventCellGroup : List<EventCell>
    {
        public string Title { get; set; }
    }
}
