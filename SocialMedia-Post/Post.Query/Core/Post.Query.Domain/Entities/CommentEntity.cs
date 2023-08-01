using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Post.Query.Domain.Entities
{
    [Table("Comment")]
    public class CommentEntity
    {
        [Key]
        public Guid CommentId { get; init; }
        public string Username { get; init; } = null!;
        public DateTime CommentDate { get; set; }
        public string Comment { get; set; } = null!;
        public bool Edited { get; set; }
        public Guid PostId { get; init; }
        [System.Text.Json.Serialization.JsonIgnore]
        public virtual PostEntity Post { get; init; } = null!;
    }
}
