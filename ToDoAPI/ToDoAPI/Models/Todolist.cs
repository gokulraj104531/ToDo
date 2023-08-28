using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace ToDoAPI.Models
{
    public class ToDoList
    {
        [Key]
        public int ToDoListId { get; set; }

        [Display(Name = "UserName")]
        [ForeignKey("UserName")]
        public virtual string? UserName { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? DueTime { get; set; }

        public string? ToDoTitle { get; set; }

        public string? ToDoListDescription { get; set; }    

        public bool? isCompleted { get; set; }
        //public virtual User user { get; set; }      
    }
}
