﻿using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace Asd.AbpZeroTemplate.EntityFrameworkCore
{
    public static class AbpZeroTemplateDbContextConfigurer
    {
        public static void Configure(DbContextOptionsBuilder<AbpZeroTemplateDbContext> builder, string connectionString)
        {
            builder.UseSqlServer(connectionString);
        }

        public static void Configure(DbContextOptionsBuilder<AbpZeroTemplateDbContext> builder, DbConnection connection)
        {
            builder.UseSqlServer(connection);
        }
    }
}