using Microsoft.AspNetCore.Mvc;

namespace Core.Controllers;

[ApiController]
[Route("[controller]")]
public class HotelController : ControllerBase
{
    private static readonly string[] HotelRooms =
    [
        "vip", "suita", "big_room", "for_family"
    ];

    [HttpGet(Name = "GetHotel")]
    public IEnumerable<Hotel> Get()
    {
        return Enumerable.Range(1, 5).Select(index => 
        {
            var dateEnter = DateOnly.FromDateTime(DateTime.Now.AddDays(index));
            return new Hotel
            {
                DateEnter = dateEnter,
                DateExit = dateEnter.AddDays(3),
                HotelRoom = HotelRooms[Random.Shared.Next(HotelRooms.Length)]
            };
        })
        .ToArray();
    }

    [HttpGet("calculate/{priceForNight}")]
    public int CalculatePrice(int priceForNight, string hroom)
    {
        for (int i = 0; i < HotelRooms.Length; i++)
        {
            if (HotelRooms[i] == hroom)
            {
                priceForNight *= ((i + 1)*100);
            }
        }
        
        return priceForNight;
    }
}
