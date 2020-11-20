using System;
using System.Collections.Generic;
using System.Text;
using CommonLayer.RequestModel;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CommonLayer
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<AdminRegisterModel> AdminTable { get; set; }

        public DbSet<UserRegisterModel> UserTable { get; set; }

    }
}
