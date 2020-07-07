using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace DatabaseModel.Models
{
    // Install-Package Microsoft.EntityFrameworkCore.Tools
    // Add-Migration InitialCreate
    // Update-Database
    public class WorkflowDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Policy> Policys { get; set; }
        public DbSet<PolicyDetail> PolicyDetails { get; set; }
        public DbSet<Request> Requests { get; set; }
        protected override void OnConfiguring(DbContextOptionsBuilder options)
                 => options.UseSqlServer(@"Data Source=(localdb)\MSSQLLocalDB;Initial Catalog=WorkflowDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False");
    }
}
