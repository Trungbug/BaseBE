using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Service
{
    public class ShiftService : BaseService<Shift>, IShiftService
    {
        private readonly IShiftRepository _shiftRepository;
        public ShiftService(IShiftRepository shiftRepository) : base(shiftRepository)
        {
            _shiftRepository = shiftRepository;
        }

        public byte[] ExportExcel(string? search)
        {
            // 1. Lấy dữ liệu từ Repo
            var data = _shiftRepository.GetExportData(search);

            // 2. Cấu hình License cho EPPlus (bắt buộc nếu dùng bản mới)
            ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

            // 3. Tạo file Excel
            using (var package = new ExcelPackage())
            {
                var worksheet = package.Workbook.Worksheets.Add("Danh sách ca làm việc");

                // -- Tạo Header --
                worksheet.Cells[1, 1].Value = "STT";
                worksheet.Cells[1, 2].Value = "Mã ca";
                worksheet.Cells[1, 3].Value = "Tên ca";
                worksheet.Cells[1, 4].Value = "Giờ vào";
                worksheet.Cells[1, 5].Value = "Giờ ra";
                worksheet.Cells[1, 6].Value = "Trạng thái";

                // Style cho Header (In đậm, căn giữa, màu nền xám nhạt)
                using (var range = worksheet.Cells[1, 1, 1, 6])
                {
                    range.Style.Font.Bold = true;
                    range.Style.HorizontalAlignment = ExcelHorizontalAlignment.Center;
                    range.Style.Fill.PatternType = ExcelFillStyle.Solid;
                    range.Style.Fill.BackgroundColor.SetColor(Color.LightGray);
                    range.Style.Border.Bottom.Style = ExcelBorderStyle.Thin;
                }

                // -- Đổ dữ liệu --
                int row = 2;
                int stt = 1;
                foreach (var item in data)
                {
                    worksheet.Cells[row, 1].Value = stt;
                    worksheet.Cells[row, 2].Value = item.ShiftCode;
                    worksheet.Cells[row, 3].Value = item.ShiftName;
                    // Format giờ HH:mm
                    worksheet.Cells[row, 4].Value = item.ShiftBeginTime.ToString(@"hh\:mm");
                    worksheet.Cells[row, 5].Value = item.ShiftEndTime.ToString(@"hh\:mm");
                    worksheet.Cells[row, 6].Value = item.ShiftStatus == 1 ? "Đang sử dụng" : "Ngưng sử dụng";

                    stt++;
                    row++;
                }

                // Tự động chỉnh độ rộng cột
                worksheet.Cells.AutoFitColumns();

                // Trả về mảng byte
                return package.GetAsByteArray();
            }
        }


        /// <summary>
        /// hàm GetPaged để nhận filters
        /// </summary>
        /// <param name="pageSize"></param>
        /// <param name="pageNumber"></param>
        /// <param name="search"></param>
        /// <param name="filters"></param>
        /// <returns></returns>
        public PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search, List<FilterCondition>? filters)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;
            return _shiftRepository.GetPaged(pageSize, pageNumber, search, filters);
        }

        public int UpdateMultipleStatus(List<Guid> ids, int status)
        {
            return _shiftRepository.UpdateMultipleStatus(ids, status);
        }
    }
}
