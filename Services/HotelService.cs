using Core.Models;
using Core.Interfaces;

namespace Core.Services
{
    public class HotelService : IHotel
    {
        private readonly List<Hotel> _rooms;
        private int nextId = 4;

        public HotelService() // הגדרת רשימה של חדרים במלון
        {
            _rooms = new List<Hotel>
            {
                new Hotel { id = 1, DateEnter = DateOnly.FromDateTime(DateTime.Now), HotelRooms = "VIP" },
                new Hotel { id = 2, DateEnter = DateOnly.FromDateTime(DateTime.Now).AddDays(1), HotelRooms = "for-family" },
                new Hotel { id = 3, DateEnter = DateOnly.FromDateTime(DateTime.Now).AddDays(-1), HotelRooms = "swita" }
            };
        }

        // Get
        public List<Hotel> GetAll() => _rooms;

        public Hotel Get(int id) => _rooms.FirstOrDefault(h => h.id == id);

        // Add
        public void Add(Hotel hotel)
        {
            hotel.id = nextId++;
            _rooms.Add(hotel); // תוקן ל-Add
        }

        // Delete
        public void Delete(int id)
        {
            var hotel = Get(id);
            if (hotel is null)
                return;

            _rooms.Remove(hotel); // תוקן ל-Remove
        }

        // Update
        public void Update(int id, Hotel hotel)
        {
            var index = _rooms.FindIndex(h => h.id == id && h.id == hotel.id); // תוקן ל-FindIndex
            if (index == -1)
                return;
        
            _rooms[index] = hotel;
        }

        // ספירת כמות החדרים כעת במלון
        public int Count => _rooms.Count; // תוקן ל-Count
    }
}