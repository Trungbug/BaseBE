using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    /// <summary>
    /// đại diện cho ca làm việc
    /// </summary>
    public class ShiftDto
    {

        /// <summary>
        /// ID ca làm việc
        /// </summary>
        public Guid ShiftId { get; set; }

        /// <summary>
        /// Mã ca làm việc
        /// </summary>
        public string ShiftCode { get; set; }

        /// <summary>
        /// Tên ca làm việc
        /// </summary>
        public string ShiftName { get; set; }

        /// <summary>
        /// Thời gian bắt đầu ca làm việc
        /// </summary>
        public TimeSpan ShiftBeginTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc ca làm việc
        /// </summary>
        public TimeSpan ShiftEndTime { get; set; }

        /// <summary>
        /// Thoì gian bắt đầu nghỉ
        /// </summary>
        public TimeSpan? ShiftBeginBreakTime { get; set; }

        /// <summary>
        /// Thời gian kết thúc nghỉ
        /// </summary>
        public TimeSpan? ShiftEndBreakTime { get; set; }

        /// <summary>
        /// Trạng thái ca làm việc: 1 - Hoạt động, 0 - Ngừng hoạt động
        /// </summary>
        public int ShiftStatus { get; set; }

        /// <summary>
        /// Thời gian làm việc (tính bằng giờ)
        /// </summary>
        public decimal WorkTimeHours { get; set; }

        /// <summary>
        /// Thời gian nghỉ (tính bằng giờ)
        /// </summary>
        public decimal BreakTimeHours { get; set; }

        /// <summary>
        /// Được tạo bởi
        /// </summary>
        public string? CreatedBy { get; set; }

        /// <summary>
        /// Ngày tạo
        /// </summary>
        public DateTime CreatedDate { get; set; }

        /// <summary>
        /// Mô tả ca làm việc
        /// </summary>
        public string? ShiftDescription { get; set; }

        /// <summary>
        /// Được sửa bởi
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// Ngày điều chỉnh
        /// </summary>
        public DateTime? ModifiedDate { get; set; }


    }
}
