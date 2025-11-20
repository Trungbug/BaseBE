using Microsoft.AspNetCore.Mvc;
using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Service;
using Misa.demo.core.Service;

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
        [HttpGet]
        public IActionResult GetPaged(
            [FromQuery] int pageSize = 10,
            [FromQuery] int pageNumber = 1,
            [FromQuery] string? search = null)
        {
            var pagedResult = _shiftService.GetPaged(pageSize, pageNumber, search);

            // Trả về ServiceResponse bọc PagedResult
            return Ok(ServiceResponse<PagedResult<ShiftDto>>.Ok(pagedResult, "Lấy dữ liệu thành công"));
        }
        [HttpPut("bulk-status")]
        public IActionResult BulkUpdateStatus([FromBody] BulkUpdateStatusDTO dto)
        {
            var result = _shiftService.UpdateMultipleStatus(dto.Ids, dto.Status);

            // Sử dụng ServiceResponse có sẵn của bạn để trả về
            return Ok(ServiceResponse<int>.Ok(result, $"Đã cập nhật trạng thái cho {result} bản ghi"));
        }



    }
}