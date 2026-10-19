namespace Core.Models;

public class Hotel
{  
       public int id { get; set; }

     public DateOnly DateEnter { get; set; }

     public DateOnly DateExit { get; set; }

     public  int priceForNight{ get; set;}

     public String? HotelRoom {get; set;}
    
}
