using Misa.demo.core.Attibute;

using System;
using System.Diagnostics.Metrics;
using System.Xml;

namespace Misa.demo.core.Entity
{
    /// <summary>
    /// thông tin ca làm việc
    /// </summary>
    [Table("shift")]
    public class Shift
    {
        /// <summary>
        /// Id ca làm việc
        /// </summary>
        [PrimaryKey]
        [ColumnName("shift_id")]
        public Guid ShiftId { get; set; }

        /// <summary>
        /// Mã ca làm việc
        /// </summary>
        [ColumnName("shift_code")]
        [NotEmpty("Mã ca không được để trống")] 
        [Unique("Mã ca đã tồn tại trong hệ thống")] 
        [MaxLength(20, "Mã ca không được vượt quá 20 ký tự")] 
        public string ShiftCode { get; set; }

        /// <summary>
        /// Tên ca làm việc
        /// </summary>
        [ColumnName("shift_name")]
        [NotEmpty("Tên ca không được để trống")] 
        [MaxLength(50, "Tên ca không được vượt quá 50 ký tự")] 
        public string ShiftName { get; set; }

        /// <summary>
        /// thời gian bắt đầu ca làm việc
        /// </summary>
        [ColumnName("shift_begin_time")]
        public TimeSpan ShiftBeginTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc ca làm việc
        /// </summary>
        [ColumnName("shift_end_time")]
        public TimeSpan ShiftEndTime { get; set; }

        /// <summary>
        /// Thời gian bắt đầu nghỉ
        /// </summary>
        [ColumnName("shift_begin_break_time")]
        public TimeSpan? ShiftBeginBreakTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc nghỉ
        /// </summary>
        [ColumnName("shift_end_break_time")]
        public TimeSpan? ShiftEndBreakTime { get; set; }

        /// <summary>
        /// Mô tả ca làm việc
        /// </summary>
        [ColumnName("shift_description")]
        public string? ShiftDescription { get; set; }

        /// <summary>
        /// Trạng thái ca làm việc: 1 - Hoạt động, 0 - Ngừng hoạt động
        /// </summary>
        [ColumnName("shift_status")]
        public int ShiftStatus { get; set; }

        /// <summary>
        /// Được tao bởi ai
        /// </summary>
        [ColumnName("created_by")]
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        [ColumnName("created_date")]
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Người sửa
        /// </summary>
        [ColumnName("modified_by")]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày sửa
        /// </summary>
        [ColumnName("modified_date")]
        public DateTime? ModifiedDate { get; set; }
    }
}