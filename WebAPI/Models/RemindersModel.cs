using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Reminders")]
    [PrimaryKey("ReminderId")]
    public class RemindersModel
    {
        public int ReminderId { get; set; }
        public string Reminder {  get; set; } = string.Empty;
        
    }
}
