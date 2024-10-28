namespace LinkExpiry.Models
{
    public class Link
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Username { get; set; }
        public bool IsAccessed { get; set; } = false;
        public DateTime? Expiration { get; set; }

        public int MaxAccessCount { get; set; } = 1;  // Default: 1 click
        public int CurrentAccessCount { get; set; } = 0;
    }
}
