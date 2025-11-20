using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.DTOs
{
    public class ShiftDto
    {
        public Guid ShiftId { get; set; }
        public string ShiftCode { get; set; }
        public string ShiftName { get; set; }
        public TimeSpan ShiftBeginTime { get; set; }
        public TimeSpan ShiftEndTime { get; set; }
        public TimeSpan? ShiftBeginBreakTime { get; set; }
        public TimeSpan? ShiftEndBreakTime { get; set; }
        public int ShiftStatus { get; set; }

        /// <summary>
        /// Thời gian làm việc (tính bằng giờ)
        /// </summary>
        public double WorkTimeHours { get; set; }

        /// <summary>
        /// Thời gian nghỉ (tính bằng giờ)
        /// </summary>
        public double BreakTimeHours { get; set; }

        public string? CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? ModifiedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
    }
}
