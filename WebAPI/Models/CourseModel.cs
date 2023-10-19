using MessagePack;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Courses")]
    [PrimaryKey("CourseId")]
    public class CourseModel
    {
        public int CourseId { get; set; }
        public string CourseName { get; set; } = string.Empty;
        public string ShortcutName { get; set; } = string.Empty;
    }
}
