using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("Students")]
    [PrimaryKey("StudentId")]
    public class StudentModel
    {
        //Error when getting list dont add not mapped
        public string FacialRecognitionId { get; set; } = string.Empty;
        public string IdNo { get; set; } = string.Empty;
        public int StudentId { get; set; }
        public string StudentName { get; set; } = string.Empty;
        public int CourseId { get; set; }
        public int DepartmentId { get; set; }
        public int SectionId { get; set; }
        public string QRCode { get; set; } = string.Empty;
        //[NotMapped]
        public CourseModel? Course { get; set; } = new CourseModel();
        //[NotMapped]
        public DepartmentModel Department { get; set; } = new DepartmentModel();
        //[NotMapped]
        public SectionModel Section { get; set; } = new SectionModel();
        //[NotMapped]
        public List<UserSanctionModel> Sanctions { get; set; } = new List<UserSanctionModel>();
        public Enums.Enums.YearLevel YearLevel { get; set; }

        [NotMapped]
        public string Departmentname { get; set; } = string.Empty;
        [NotMapped]
        public string CourseName { get; set; } = string.Empty;
        [NotMapped]
        public string SectionName { get; set; } = string.Empty;
        [NotMapped]
        public string SanctionName { get; set; } = string.Empty;
        [NotMapped]
        public decimal SanctionAmount { get; set; }
        [NotMapped]
        public bool IsPaid { get; set; }
        [NotMapped]
        public DateTime DateRecorded { get; set; }
        [NotMapped]
        public decimal Amount { get; set; }
    }
}
