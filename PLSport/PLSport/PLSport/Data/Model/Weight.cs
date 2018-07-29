// Weight database model

namespace PLSport.Data.Model
{
    public class Weight
    {
        public int ID { get; set; }
        public System.DateTime Date { get; set; }
        public int Value { get; set; }
        public int User_ID { get; set; }
    }
}
