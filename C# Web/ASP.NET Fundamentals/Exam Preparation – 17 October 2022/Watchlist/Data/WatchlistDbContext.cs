﻿namespace Watchlist.Data
{
    using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore;
    using Watchlist.Data.Models;

    public class WatchlistDbContext : IdentityDbContext<User>
    {
        public WatchlistDbContext(DbContextOptions<WatchlistDbContext> options)
            : base(options)
        {
        }

        public DbSet<Movie> Movies { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<UserMovie> UserMovies { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder
                 .Entity<Genre>()
                 .HasData(new Genre()
                 {
                     Id = 1,
                     Name = "Action"
                 },
                 new Genre()
                 {
                     Id = 2,
                     Name = "Comedy"
                 },
                 new Genre()
                 {
                     Id = 3,
                     Name = "Drama"
                 },
                 new Genre()
                 {
                     Id = 4,
                     Name = "Horror"
                 },
                 new Genre()
                 {
                     Id = 5,
                     Name = "Romantic"
                 });
            

            builder.Entity<UserMovie>().HasKey(x => new { x.UserId, x.MovieId });
            builder.Entity<Movie>().Property(r => r.Rating).HasPrecision(18, 2);
            builder.Entity<User>()
                .Property(u => u.UserName)
                .HasMaxLength(20)
                .IsRequired();

            builder.Entity<User>()
                .Property(u => u.Email)
                .HasMaxLength(60)
                .IsRequired();

            base.OnModelCreating(builder);
        }
    }
}