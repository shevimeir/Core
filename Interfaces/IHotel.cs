using Core.Models;

namespace Core.Interfaces;

    public interface IHotel
    {
        List<Hotel> GetAll();

        Hotel Get(int id);

        void Add(Hotel hotel);

        void Delete(int id);

        void Update(int id,Hotel hotel);

        int Count { get; }
    }
