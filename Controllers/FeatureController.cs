using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
    public class FeatureController:Controller
    {
        private readonly VegaDbContext context;

        public FeatureController(VegaDbContext context)
        {
            this.context = context;
        }

        [HttpGet("/api/features")]
        public IEnumerable<Feature> GetFeatures()
        {
            var features= context.Features.ToList();
            return features;
        }
    }
}
