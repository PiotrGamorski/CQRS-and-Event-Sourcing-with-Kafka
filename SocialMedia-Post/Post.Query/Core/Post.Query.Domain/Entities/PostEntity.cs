using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Query.Domain.Entities
{
    [Table("Post")]
    public class PostEntity
    {
        [Key]
        public Guid PostId { get; set; }
        public string Author { get; init; } = null!;
        public DateTime DatePosted { get; init; }
        public string Message { get; set; } = null!;
        public int Likes { get; set; }
        public virtual ICollection<CommentEntity> Comments { get; init; } = null!;
    }
}
