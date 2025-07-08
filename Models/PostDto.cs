namespace postsPraktikum.Models
{
    public class PostDto{
        public required string PostTitle { get; set; }
        public string? PostDescription { get; set; }
        public required string PostText { get; set; }
        public required DateTime PostCreationDate { get; set; }
        public required string[] Photos { get; set; }
        public Boolean IsPrivate { get; set; }

        // --- ADD THIS LINE ---
        public Guid UserId { get; set; }

    }
}
