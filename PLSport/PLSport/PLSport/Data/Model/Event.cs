// Event database model

namespace PLSport.Data.Model
{
    public class Event
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public System.DateTime Date { get; set; }
        public int Place { get; set; }
        public int Type_ID { get; set; }
        public string Surname { get; set; }
        public int Owner_ID { get; set; }
    }
}
