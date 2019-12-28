using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using vega.Extensions;
using vega.Models;

namespace vega.Persistence
{
    public class VehicleRepository : IVehicleRepository
    {
        private readonly VegaDbContext context;

        public VehicleRepository(VegaDbContext context)
        {
            this.context = context;
        }

        public async Task<Vehicle> GetVehicle(int Id, bool IncludeRelated=true)
        {
            if (!IncludeRelated)
                return await context.Vehicles.FindAsync(Id);

            return await context.Vehicles

            .Include(m => m.Model)
                .ThenInclude(m => m.Make)
            .Include(vf => vf.VehicleFeatures)
                .ThenInclude(f => f.Feature)
            .SingleOrDefaultAsync(v => v.Id == Id);
        }
        public async Task<QueryResult<Vehicle>> GetVehicleList(VehicleQuery queryObj)
        {
            var query = context.Vehicles
               .Include(m => m.Model)
                    .ThenInclude(m => m.Make)
               .Include(m => m.VehicleFeatures)
                    .ThenInclude(n => n.Feature)
               .AsQueryable();

            if (queryObj.MakeId.HasValue)
                query = query.Where(v => v.Model.MakeId == queryObj.MakeId.Value);

            if (queryObj.ModelId.HasValue)
                query = query.Where(v => v.ModelId == queryObj.ModelId.Value);

            Dictionary<string, Expression<Func<Vehicle, object>>> columnsMap = new Dictionary<string, Expression<Func<Vehicle, object>>>()
            {
                ["make"]=v=>v.Model.Make.Name,
                ["model"]=v=>v.Model.Name,
                ["contactName"]=v=>v.ContactName,
                ["id"]=v=>v.Id
            };
            var queryResult = new QueryResult<Vehicle>();
            query = query.ApplyOrdering(queryObj, columnsMap);
            queryResult.TotalItems = await query.CountAsync();
            query = query.ApplyPaging(queryObj);
            

            queryResult.Items= await query.ToListAsync();
            return queryResult;
        }

        public void Add(Vehicle vehicle)
        {
            context.Vehicles.Add(vehicle);
        }
        public void Remove(Vehicle vehicle)
        {
            context.Vehicles.Remove(vehicle);
        }

        public bool VehicleExists(int id)
        {
            return context.Vehicles.Any(e => e.Id == id);
        }

        public  bool ModelExists(int id)
        {
            return  context.Models.Any(e => e.Id == id);
        }
        
        public async Task<Model> GetModel(int id, bool IncludeRelated = true)
        {
            return await context.Models
                .Include(m => m.Make)
                .SingleOrDefaultAsync(mo => mo.Id == id);
        }
    }
}
