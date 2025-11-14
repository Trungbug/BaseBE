using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.demo.core.Interface.Repository;
using Misa.demo.core.Interface.Service;
using System;
using System.Collections.Generic;
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

        public PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search)
        {
            if (pageSize <= 0) pageSize = 10;
            if (pageNumber <= 0) pageNumber = 1;

            return _shiftRepository.GetPaged(pageSize, pageNumber, search);
        }
    }
}
