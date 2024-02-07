using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PayPalDemo.Models;
using PayPalDemo.ViewModels;



public class ApplicationDbContext : IdentityDbContext
{
    public DbSet<IPN> IPNs { get; set; }
    //public DbSet<Transaction> transactions { get; set; }
    public DbSet<Product> Products { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
           base(options)
    {
    }

    public DbSet<MyRegisteredUser> MyRegisteredUsers { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Product>().HasData(
            new Product
            {
                ID = 1
                        ,
                ProductName = "Red Cards"
                        ,
                Description = "Are you looking for a fun and affordable way to " +
                                            "pass the time? Look no further than our high-" +
                                            "quality deck of cards! At only $3.79, this deck " +
                                            "of cards is an excellent value."
                        ,
                Price = "3.79"
                        ,
                Currency = "CAD"
                        ,
                ImageName = "DeckOfCards.png"
            },
            new Product
            {
                ID = 2
                        ,
                ProductName = "Extra Ace"
                        ,
                Description = "Are you tired of being caught without an ace up " +
                                        "your sleeve? Well, have no fear! Our special deck " +
                                        "of cards comes with an extra ace, so you can " +
                                        "always have the upper hand. And at just $4.95, " +
                                        "it's a steal!"
                        ,
                Price = "4.95"
                        ,
                Currency = "CAD"
                        ,
                ImageName = "FiveAces.jpg"
            },
            new Product
            {
                ID = 3
                        ,
                ProductName = "Black Deck"
                        ,
                Description = "Upgrade your card game with our premium black-" +
                                        "styled deck of cards. Made with high-quality " +
                                        "materials and featuring a sleek black design. At " +
                                        "just $7.79, it's a small price to pay to make a " +
                                        "big statement!"
                        ,
                Price = "7.79"
                        ,
                Currency = "CAD"
                        ,
                ImageName = "BlackDeck.jpeg"
            });
    }

}


