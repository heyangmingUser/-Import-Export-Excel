using DaoRu.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DaoRu
{
    public class ApiContext:DbContext
    {
        public ApiContext(DbContextOptions<ApiContext> opt):base(opt)
        {
            
        }

        /// <summary>
        /// 数据模型
        /// </summary>
        public DbSet<ImportStudentDto> ImportStudentDtos { get; set; }
    }
}
