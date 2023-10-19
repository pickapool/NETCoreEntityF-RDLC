using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Students")]
    [PrimaryKey("StudentId")]
    public class StudentModel
    {
        //Error when getting list dont add not mapped
        public int StudentId {  get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
       //[NotMapped]
        public CourseModel Course { get; set; } = new CourseModel();
       //[NotMapped]
        public DepartmentModel Department { get; set; } = new DepartmentModel();
        //[NotMapped]
        public SectionModel Section { get; set; } = new SectionModel();
        //[NotMapped]
        public List<SanctionModel>? Sanctions { get; set;}
        public Enums.Enums.YearLevel YearLevel { get; set; }

    }
}
