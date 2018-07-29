// User database model

namespace PLSport.Data.Model
{
    public class User
    {
        public int ID { get; set; }
        public string Surname { get; set; }
        public string Name { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string Gender { get; set; }
        public int Height { get; set; }
        public int Weight { get; set; }
        public System.DateTime Birthday { get; set; }
        public int Status_ID { get; set; }
    }
}
