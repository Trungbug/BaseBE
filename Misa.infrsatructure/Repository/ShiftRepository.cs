using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.infrsatructure.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Misa.demo.core.Interface.Repository
{
    public class ShiftRepository : BaseRepo<Shift>, IShiftRepository
    {
        public ShiftRepository(IConfiguration config) : base(config) { }

        public PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search)
        {
            using (var connection = GetOpenConnection())
            {
                var whereClause = new StringBuilder();
                var parameters = new DynamicParameters();

                if (!string.IsNullOrEmpty(search))
                {
                    
                    whereClause.Append("WHERE (shift_code LIKE @Search OR shift_name LIKE @Search)");
                    parameters.Add("@Search", $"%{search}%");
                }
                var sql = $@"
                    SELECT 
                        shift_id, shift_code, shift_name, 
                        shift_begin_time, shift_end_time, 
                        shift_begin_break_time, shift_end_break_time, 

                        -- Trả về trực tiếp shift_status và alias trùng với DTO
                        shift_status AS ShiftStatus,

                        -- Tính thời gian làm việc (alias khớp với DTO)
                        (TIMESTAMPDIFF(SECOND, shift_begin_time, shift_end_time) / 3600.0) AS WorkTimeHours,

                        -- Tính thời gian nghỉ (trả về 0 nếu NULL) (alias khớp với DTO)
                        COALESCE((TIMESTAMPDIFF(SECOND, shift_begin_break_time, shift_end_break_time) / 3600.0), 0) AS BreakTimeHours,
                        
                        created_by,
                        created_date,
                        modified_by,
                        modified_date
                    FROM 
                        shift
                    {whereClause}
                    ORDER BY 
                        shift_code ASC
                    LIMIT @PageSize OFFSET @Offset";

                parameters.Add("@PageSize", pageSize);
                parameters.Add("@Offset", (pageNumber - 1) * pageSize);
                var sqlTotal = $"SELECT COUNT(*) FROM shift {whereClause}";
                var data = connection.Query<ShiftDto>(sql, parameters);
                var totalRecords = connection.ExecuteScalar<int>(sqlTotal, parameters);
                return new PagedResult<ShiftDto>
                {
                    TotalRecords = totalRecords,
                    Data = data
                };
            }
        }

        public int UpdateMultipleStatus(List<Guid> ids, int status)
        {
            if(ids == null || ids.Count == 0)
            {
                return 0;
            }using (var connection = GetOpenConnection())
            {
                var sql = $"UPDATE shift SET shift_status = @Status WHERE shift_id IN @Ids";
                var parameters = new { Status = status, Ids = ids };

                var result = connection.Execute(sql, parameters);
                return result;
            }
           
        }
    }
}
