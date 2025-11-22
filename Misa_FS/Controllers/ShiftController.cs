using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Service;
using Misa.demo.core.Service;
using System.Text.Json;

namespace Misa_FS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ShiftController : MSBaseController<Shift>
    {
        
        private readonly IShiftService _shiftService;

        
        public ShiftController(IShiftService shiftService) : base(shiftService)
        {
            _shiftService = shiftService;
        }

        /// <summary>
        /// API lấy danh sách ca làm việc có phân trang + tìm kiếm
        /// </summary>
        /// <param name="pageSize">Số bản ghi mỗi trang</param>
        /// <param name="pageNumber">Trang hiện tại</param>
        /// <param name="search">Từ khóa tìm kiếm</param>
        /// <returns>PagedResult chứa danh sách ShiftDto</returns>
        [HttpGet]
        /// <summary>
        /// API lấy danh sách ca làm việc có phân trang + tìm kiếm + lọc nâng cao
        /// </summary>
        /// <param name="pageSize">Số bản ghi mỗi trang (Mặc định 10)</param>
        /// <param name="pageNumber">Trang hiện tại (Mặc định 1)</param>
        /// <param name="search">Từ khóa tìm kiếm chung (Mã hoặc Tên)</param>
        /// <param name="filters">Chuỗi JSON chứa danh sách điều kiện lọc theo cột</param>
        /// <returns>PagedResult chứa danh sách ShiftDto</returns>
        [HttpGet]
        public IActionResult GetPaged(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1,
            [FromQuery] string? search = null,
            [FromQuery] string? filters = null)
        {
            
            List<FilterCondition>? filterConditions = null;

            if (!string.IsNullOrEmpty(filters))
            {
                try
                {
                    
                    filterConditions = JsonSerializer.Deserialize<List<FilterCondition>>(filters, new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive = true 
                    });
                }
                catch (Exception ex)
                {
                    
                    Console.WriteLine($"Lỗi deserialize filter: {ex.Message}");
                    filterConditions = new List<FilterCondition>();
                }
            }

            // 4. Gọi Service để lấy dữ liệu
            var pagedResult = _shiftService.GetPaged(pageSize, pageNumber, search, filterConditions);

            // 5. Trả về kết quả chuẩn
            return Ok(ServiceResponse<PagedResult<ShiftDto>>.Ok(pagedResult, "Lấy dữ liệu thành công"));
        }

        /// <summary>
        /// API cập nhật trạng thái cho nhiều bản ghi cùng lúc
        /// (Sử dụng / Ngừng sử dụng hàng loạt)
        /// </summary>
        /// <param name="dto">Danh sách Id + trạng thái cần cập nhật</param>
        /// <returns>Số bản ghi cập nhật thành công</returns>
        [HttpPut("bulk-status")]
        public IActionResult BulkUpdateStatus([FromBody] BulkUpdateStatusDTO dto)
        {
            // Gọi service xử lý cập nhật hàng loạt
            var result = _shiftService.UpdateMultipleStatus(dto.Ids, dto.Status);

            return Ok(ServiceResponse<int>.Ok(result, $"Đã cập nhật trạng thái cho {result} bản ghi"));
        }

        /// <summary>
        /// API xuất khẩu danh sách ca làm việc ra file Excel
        /// </summary>
        /// <param name="search">Từ khóa tìm kiếm (nếu có)</param>
        /// <returns>File Excel (.xlsx)</returns>
        [HttpGet("export")]
        public IActionResult Export([FromQuery] string? search)
        {
            try
            {
                // Gọi service xử lý export, trả về dạng byte[]
                var fileContent = _shiftService.ExportExcel(search);

                // Trả file excel về FE
                return File(
                    fileContent,
                    "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet",
                    $"Danh_sach_ca_{DateTime.Now:ddMMyyyy}.xlsx"
                );
            }
            catch (Exception ex)
            {
                // Xử lý lỗi nếu export thất bại
                return StatusCode(500, ServiceResponse<object>.Error(ex.Message));
            }
        }

    }
}
