using Dapper;
using Microsoft.Extensions.Configuration;
using Misa.demo.core.DTOs;
using Misa.demo.core.Entity;
using Misa.infrsatructure.Repository;
using System;
using System.Collections.Generic;
using System.Text;

namespace Misa.demo.core.Interface.Repository
{
    public class ShiftRepository : BaseRepo<Shift>, IShiftRepository
    {
        public ShiftRepository(IConfiguration config) : base(config) { }

        /// <summary>
        /// Ham lay du lieu de xuat file
        /// </summary>
        /// <param name="search"></param>
        /// <returns>danh sách file cần xuất</returns>
        public IEnumerable<ShiftDto> GetExportData(string? search)
        {
            using var connection = GetOpenConnection();

            var (where, parameters) = BuildSearchCondition(search);

            var sql = $@"
                SELECT 
                    shift_id, shift_code, shift_name, 
                    shift_begin_time, shift_end_time, 
                    shift_begin_break_time, shift_end_break_time, 
                    shift_status AS ShiftStatus,
                    TIMESTAMPDIFF(SECOND, shift_begin_time, shift_end_time) / 3600.0 AS WorkTimeHours,
                    COALESCE(TIMESTAMPDIFF(SECOND, shift_begin_break_time, shift_end_break_time) / 3600.0, 0) AS BreakTimeHours,
                    created_by, created_date, modified_by, modified_date
                FROM shift
                {where}
                ORDER BY shift_code ASC";

            return connection.Query<ShiftDto>(sql, parameters);
        }

        /// <summary>
        /// Phân trang và tìm kiếm ca làm việc
        /// </summary>
        /// <param name="pageSize">số ca 1 trang</param>
        /// <param name="pageNumber">trang hiện tại</param>
        /// <param name="search">từ khóa tìm kiếm</param>
        /// <returns>danh sách ca làm việc</returns>
        public PagedResult<ShiftDto> GetPaged(int pageSize, int pageNumber, string? search, List<FilterCondition>? filters)
        {
            using var connection = GetOpenConnection();

            var (where, parameters) = BuildSearchCondition(search);
            AppendFilterConditions(filters, where, parameters);

            var sqlData = $@"
                SELECT 
                    shift_id, shift_code, shift_name, 
                    shift_begin_time, shift_end_time, 
                    shift_begin_break_time, shift_end_break_time, 
                    shift_status AS ShiftStatus,
                    work_time_hours AS WorkTimeHours,
                    break_time_hours AS BreakTimeHours,
                    created_by, created_date, modified_by, modified_date, shift_description
                FROM shift
                {where}
                ORDER BY created_date DESC
                LIMIT @PageSize OFFSET @Offset";

            parameters.Add("@PageSize", pageSize);
            parameters.Add("@Offset", (pageNumber - 1) * pageSize);

            var sqlTotal = $"SELECT COUNT(*) FROM shift {where}";

            var data = connection.Query<ShiftDto>(sqlData, parameters);
            var total = connection.ExecuteScalar<int>(sqlTotal, parameters);

            return new PagedResult<ShiftDto>
            {
                TotalRecords = total,
                Data = data
            };
        }

        /// <summary>
        /// cập nhật trạng thái nhiều ca làm việc
        /// </summary>
        /// <param name="ids">danh sách id ca làm việc</param>
        /// <param name="status">trạng thái cần sửa</param>
        /// <returns>kết quả</returns>
        public int UpdateMultipleStatus(List<Guid> ids, int status)
        {
            if (ids == null || ids.Count == 0)
                return 0;

            using var connection = GetOpenConnection();

            const string sql = "UPDATE shift SET shift_status = @Status WHERE shift_id IN @Ids";

            return connection.Execute(sql, new { Status = status, Ids = ids });
        }

        // ============================================================
        // =============  PRIVATE CLEAN HELPERS  ======================
        // ============================================================

        private (StringBuilder where, DynamicParameters parameters) BuildSearchCondition(string? search)
        {
            var where = new StringBuilder("WHERE 1=1");
            var parameters = new DynamicParameters();

            if (!string.IsNullOrWhiteSpace(search))
            {
                where.Append(" AND (shift_code LIKE @Search OR shift_name LIKE @Search)");
                parameters.Add("@Search", $"%{search}%");
            }

            return (where, parameters);
        }

        private void AppendFilterConditions(List<FilterCondition>? filters, StringBuilder where, DynamicParameters parameters)
        {
            if (filters == null || filters.Count == 0)
                return;

            foreach (var filter in filters)
            {
                if (!ColumnMappings.TryGetValue(filter.Column, out var dbColumn))
                    continue;

                string param = $"@{filter.Column}Filter";
                string value = filter.Value.Trim();

                switch (filter.Operator)
                {
                    case "Contains":
                        where.Append($" AND {dbColumn} LIKE {param}");
                        parameters.Add(param, $"%{value}%");
                        break;

                    case "NotContains":
                        where.Append($" AND {dbColumn} NOT LIKE {param}");
                        parameters.Add(param, $"%{value}%");
                        break;

                    case "StartsWith":
                        where.Append($" AND {dbColumn} LIKE {param}");
                        parameters.Add(param, $"{value}%");
                        break;

                    case "EndsWith":
                        where.Append($" AND {dbColumn} LIKE {param}");
                        parameters.Add(param, $"%{value}");
                        break;

                    case "Equals":
                    default:
                        where.Append($" AND {dbColumn} = {param}");
                        parameters.Add(param, value);
                        break;
                }
            }
        }

        private static readonly Dictionary<string, string> ColumnMappings = new()
        {
            { "shiftCode", "shift_code" },
            { "shiftName", "shift_name" },
        };
    }
}
