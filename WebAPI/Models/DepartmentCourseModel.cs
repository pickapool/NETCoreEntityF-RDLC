using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("DepartmentCourses")]
    [PrimaryKey("DepartmentCourseId")]
    public class DepartmentCourseModel
    {
        public int DepartmentCourseId { get; set; }
        public int DepartmentId { get; set; }
        public int CourseId { get; set; }
        [ForeignKey("CourseId")]
        public CourseModel Course { get; set; } = new();
        [ForeignKey("DepartmentId")]
        public DepartmentModel? Department { get; set; } = new();
    }
}
