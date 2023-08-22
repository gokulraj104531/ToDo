using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using ToDoAPI.Models;

namespace ToDoAPI.DTO
{
    public class TodolistDTO
    {
        [Key]
        public int ToDoListId { get; set; }

        [Display(Name = "UserName")]
        public virtual string? UserName { get; set; }

        [ForeignKey("UserName")]
        public virtual User user { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? DueTime { get; set; }

        public string? ToDoTitle { get; set; }

        public string? ToDoList { get; set; }

        public bool? isCompleted { get; set; }
    }
}
