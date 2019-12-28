﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Models;

namespace vega.Persistence
{
    public class VegaDbContext : DbContext
    {
        public DbSet<Make> Make { get; set; }
        public DbSet<Feature> Features { get; set; }

        public DbSet<Model> Models { get; set; }
        public DbSet<ModelFeatures> ModelFeatures { get; set; }
        public DbSet<Vehicle> Vehicles { get; set; }
        public DbSet<VehicleFeatures> VehicleFeatures { get; set; }
        public DbSet<Photo> Photos { get; set; }

        public VegaDbContext(DbContextOptions<VegaDbContext> options) : base(options)
        {

        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<VehicleFeatures>().HasKey(vf => new { vf.VehicleId, vf.FeatureId });
        }
            
     }
}
