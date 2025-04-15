﻿using System.ComponentModel.DataAnnotations;

namespace TodoListAPI.Models
{
    public class TodoItem
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now;
        public DateTime UpdatedDate { get; set; }
        public DateTime Deadline { get; set; }
        public bool IsCompleted { get; set; } = false;
    }
}
