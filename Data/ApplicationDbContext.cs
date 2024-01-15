using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;
using System.ComponentModel.DataAnnotations;

public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<IPN> IPNs { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
           base(options)
    {

    }

    public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);
    }
}


