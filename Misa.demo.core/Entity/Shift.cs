using Misa.demo.core.Attibute;

using System;
using System.Diagnostics.Metrics;
using System.Xml;

namespace Misa.demo.core.Entity
{
//    CREATE TABLE amis_production_development.shift(
//  shift_id char (36) NOT NULL DEFAULT(UUID()) COMMENT 'ID ca làm việc',
//  shift_code varchar(20) NOT NULL COMMENT 'Mã ca làm việc (người dùng nhập)',
//  shift_name varchar(255) NOT NULL COMMENT 'Tên ca làm việc',
//  shift_begin_time time NOT NULL COMMENT 'Giờ vào ca ',
//  shift_end_time time NOT NULL COMMENT 'Giờ hết ca ',
//  shift_begin_break_time time DEFAULT NULL COMMENT 'Giờ bắt đầu nghỉ giữa ca ',
//  shift_end_break_time time DEFAULT NULL COMMENT 'Giờ kết thúc nghỉ giữa ca ',
//  shift_description varchar(255) DEFAULT '' COMMENT 'Mô tả ca làm việc',
//  shift_status tinyint NOT NULL DEFAULT 1 COMMENT 'Trạng thái. 1: Đang sử dụng (API Inactive=false), 0: Ngừng sử dụng (API Inactive=true)',
//  created_by varchar(100) DEFAULT NULL COMMENT 'Người tạo',
//  created_date datetime DEFAULT CURRENT_TIMESTAMP COMMENT 'Ngày giờ tạo',
//  modified_by varchar(100) DEFAULT NULL COMMENT 'Người sửa cuối cùng',
//  modified_date datetime DEFAULT NULL ON UPDATE CURRENT_TIMESTAMP COMMENT 'Ngày giờ sửa cuối cùng',
//  PRIMARY KEY(shift_id)
//)
//ENGINE = INNODB,
//AVG_ROW_LENGTH = 2340,
//CHARACTER SET utf8mb4,
//COLLATE utf8mb4_0900_as_ci,
//COMMENT = 'Ca làm việc';

//    ALTER TABLE amis_production_development.shift
//    ADD UNIQUE INDEX uix_workshift_workshiftcode(shift_code);
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