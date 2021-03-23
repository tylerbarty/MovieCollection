﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieCollection.Models
{
    public class EFMovieRepository
    {
        private MovieDbContext _context;

        //Constructor

        public EFMovieRepository(MovieDbContext context)
        {
            _context = context;
        }
        public IQueryable<Movie> Movies => _context.Movies;
    }
}
