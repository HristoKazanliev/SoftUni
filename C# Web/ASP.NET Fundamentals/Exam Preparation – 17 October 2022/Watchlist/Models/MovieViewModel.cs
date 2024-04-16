﻿namespace Watchlist.Models
{
    using System.ComponentModel.DataAnnotations.Schema;
    using System.ComponentModel.DataAnnotations;
    using Watchlist.Data.Models;

    public class MovieViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; } = null!;

        public string Director { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;

        public decimal Rating { get; set; }

        public string Genre { get; set; } = null!;
    }
}
