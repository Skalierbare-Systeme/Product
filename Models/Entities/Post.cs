namespace postsPraktikum.Models.Entities
{
    public class Post
    {
        public Guid Id { get; set; }
        public required string PostTitle { get; set; }
        public string? PostDescription { get; set; }
        public required string PostText { get; set; }
        public required DateTime PostCreationDate { get; set; }
        public required string[] Photos { get; set; }
        
        public Guid UserId { get; set; } 
    }
}
