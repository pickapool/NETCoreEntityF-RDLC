namespace WebAPI.Models
{
    public class FilterParameter
    {
        public bool IsDate { get; set; }
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public bool IsYear { get; set; }
        public List<Enums.Enums.YearLevel> YearLevels { get; set; } = new();
        public bool IsDepartment { get; set; }
        public List<DepartmentModel> Departments { get; set; } = new();
        public bool IsSanction { get; set; }
        public List<SanctionModel> Sanctions { get; set; } = new();
        public bool IsCourse { get; set; }
        public List<CourseModel> Courses { get; set; } = new();
        public bool IsSection { get; set; }
        public List<SectionModel> Sections { get; set; } = new();
        public int StudentId { get; set; }
    }
}
