using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Models
{
    public class TodoItemDto
    {
        [Required]
        [MaxLength(100)]
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
