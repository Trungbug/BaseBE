using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Repository
{
    public interface IShiftRepository: IBaseRepo<Shift>
    {
        PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search);
        int UpdateMultipleStatus(List<Guid> ids, int status);
    }
}
