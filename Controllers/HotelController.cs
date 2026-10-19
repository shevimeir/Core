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
                priceForNight *= ((i + 1) * 100);
            }
        }
        
        return priceForNight;
    } // כאן הסתיימה המתודה CalculatePrice בצורה תקינה

    [HttpGet("{id}")]
    public ActionResult<Hotel> Get(int id)
    {
        var hotel = HotelService.Get(id);

        if (hotel == null)
            return NotFound();

        return hotel;
    }

    [HttpPost] 
    public IActionResult Create(Hotel hotel)
    {
        HotelService.Add(hotel);
        return CreatedAtAction(nameof(Create), new { id = hotel.Id }, hotel);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, Hotel hotel)
    {
        if (id != hotel.Id)
            return BadRequest();

        var existingHotel = HotelService.Get(id);
        if (existingHotel is null)
            return NotFound();

        HotelService.Update(id, hotel);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        // תוקן האות הקטנה ל-H גדולה
        var hotel = HotelService.Get(id);
        if (hotel is null)
            return NotFound();

        HotelService.Delete(id);

        return Content(HotelService.Count.ToString());
    }
}