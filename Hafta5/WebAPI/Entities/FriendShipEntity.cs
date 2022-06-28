using Microsoft.AspNetCore.Identity;

namespace WebAPI.Entities
{
    public class FriendShipEntity
    {
        public int FriendShipId { get; set; }
        public IdentityUser FromUserId { get; set; }
        public IdentityUser ToUserId { get; set; }
        public DateTime OfferTime { get; set; }


    }
}
