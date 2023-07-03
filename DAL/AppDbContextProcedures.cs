﻿// <auto-generated> This file has been auto generated by EF Core Power Tools. </auto-generated>
using DAL.Entities;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace DAL
{
    public partial class AppDbContext
    {
        private IAppDbContextProcedures _procedures;

        public virtual IAppDbContextProcedures Procedures
        {
            get
            {
                if (_procedures is null) _procedures = new AppDbContextProcedures(this);
                return _procedures;
            }
            set
            {
                _procedures = value;
            }
        }

        public IAppDbContextProcedures GetProcedures()
        {
            return Procedures;
        }

        protected void OnModelCreatingGeneratedProcedures(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<usp_getproductResult>().HasNoKey().ToView(null);
        }
    }

    public partial class AppDbContextProcedures : IAppDbContextProcedures
    {
        private readonly AppDbContext _context;

        public AppDbContextProcedures(AppDbContext context)
        {
            _context = context;
        }

        public virtual async Task<List<usp_getproductResult>> usp_getproductAsync(int? ProductId, OutputParameter<int> returnValue = null, CancellationToken cancellationToken = default)
        {
            var parameterreturnValue = new SqlParameter
            {
                ParameterName = "returnValue",
                Direction = System.Data.ParameterDirection.Output,
                SqlDbType = System.Data.SqlDbType.Int,
            };

            var sqlParameters = new []
            {
                new SqlParameter
                {
                    ParameterName = "ProductId",
                    Value = ProductId ?? Convert.DBNull,
                    SqlDbType = System.Data.SqlDbType.Int,
                },
                parameterreturnValue,
            };
            var _ = await _context.SqlQueryAsync<usp_getproductResult>("EXEC @returnValue = [dbo].[usp_getproduct] @ProductId", sqlParameters, cancellationToken);

            returnValue?.SetValue(parameterreturnValue.Value);

            return _;
        }
    }
}