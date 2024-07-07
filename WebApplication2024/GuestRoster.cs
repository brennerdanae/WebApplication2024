

namespace WebApplication2024
{
    public static class GuestRoster
    {

        static GuestRoster()
        {
            allGuests = new List<Guest>();
        }

        public static IList<Guest> allGuests { get; set; }

    }
}
