using Microsoft.EntityFrameworkCore;

namespace Market.Entities
{
    [Owned]
    public class refresh_token
    {
        public string token { get; set; }
        public DateTime expires_on { get; set; }
        public bool is_expired=>DateTime.UtcNow>=expires_on;
        public DateTime? created_on { get; set; }
        public DateTime? revoke_on { get; set; }
        public bool is_active =>revoke_on==null && !is_expired;

    }
}