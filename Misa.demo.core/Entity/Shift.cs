using Misa.demo.core.Attibute;

using System;

namespace Misa.demo.core.Entity
{
    [Table("shift")]
    public class Shift
    {
        [PrimaryKey]
        [ColumnName("shift_id")]
        public Guid ShiftId { get; set; }

        [ColumnName("shift_code")]
        [NotEmpty("Mã ca không được để trống")] 
 
        [Unique("Mã ca đã tồn tại trong hệ thống")] 

        [MaxLength(20, "Mã ca không được vượt quá 20 ký tự")] 
        public string ShiftCode { get; set; }

        [ColumnName("shift_name")]

        [NotEmpty("Tên ca không được để trống")] 

        [MaxLength(50, "Tên ca không được vượt quá 50 ký tự")] 
        public string ShiftName { get; set; }

        [ColumnName("shift_begin_time")]
     
        public TimeSpan ShiftBeginTime { get; set; }

        [ColumnName("shift_end_time")]

        public TimeSpan ShiftEndTime { get; set; }

        [ColumnName("shift_begin_break_time")]
        public TimeSpan? ShiftBeginBreakTime { get; set; }

        [ColumnName("shift_end_break_time")]
        public TimeSpan? ShiftEndBreakTime { get; set; }

        [ColumnName("shift_description")]
        public string? ShiftDescription { get; set; }

        [ColumnName("shift_status")]
        public int ShiftStatus { get; set; }

        [ColumnName("created_by")]
        public string? CreatedBy { get; set; }

        [ColumnName("created_date")]
        public DateTime CreatedDate { get; set; }

        [ColumnName("modified_by")]
        public string? ModifiedBy { get; set; }

        [ColumnName("modified_date")]
        public DateTime? ModifiedDate { get; set; }
    }
}