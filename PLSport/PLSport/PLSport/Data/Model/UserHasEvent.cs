// User_Has_Event database model

namespace PLSport.Data.Model
{
    public class UserHasEvent
    {
        public int User_ID { get; set; }
        public int Event_ID { get; set; }
        public string Type { get; set; }
    }
}
