// Event Cell from Event 

using Xamarin.Forms;

namespace PLSport.Data.Model
{
    public class EventCell
    {
        public int ID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Time { get; set; }
        public Color PlaceStatus { get; set; }
        public ImageSource TypePicture { get; set; }
        public string Coach { get; set; }
        public int Owner_ID { get; set; }
    }
}
