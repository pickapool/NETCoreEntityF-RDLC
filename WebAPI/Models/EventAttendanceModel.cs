using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAPI.Models
{
    [Table("EventAttendance")]
    [PrimaryKey("EventAttendanceId")]
    public class EventAttendanceModel
    {
        public int EventAttendanceId { get; set; }
        public int EventId { get; set; }
        public string FacialRecognitionId { get; set; } = string.Empty;
        public int? StudentId {  get; set; }
        public StudentModel? Student { get; set; }
    }
}
