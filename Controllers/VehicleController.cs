using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using vega.Models;
using vega.Persistence;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.ObjectModel;
using AutoMapper;
using vega.Controllers.Resources;
using Microsoft.AspNetCore.Authorization;

namespace vega.Controllers
{
    [Route("/api/vehicles")]
    [ApiController]
    public class VehicleController: ControllerBase
    {
        private readonly VegaDbContext context;
        private readonly IMapper mapper;
        private readonly IVehicleRepository repository;

        public VehicleController(VegaDbContext context, IMapper mapper, IVehicleRepository repository)
        {
            this.context = context;
            this.mapper = mapper;
            this.repository = repository;
        }

        //GET: api/vehicles
        [HttpGet]
        public async Task<QueryResultResource<SaveVehicleResource>> GetVehicles(int? makeId, int? modelId,
                                    bool isSortAscending = true, string sortBy="id", int page=1, byte pageSize=10)
        {
            if (sortBy is null)
            {
                throw new ArgumentNullException(nameof(sortBy));
            }

            VehicleQueryResource filterResource = new VehicleQueryResource();
                filterResource.MakeId = makeId;
                filterResource.ModelId = modelId;
                filterResource.IsSortAscending = isSortAscending;
                filterResource.SortBy = sortBy;
                filterResource.Page = page;
                filterResource.PageSize = pageSize;

            var filter = mapper.Map<VehicleQueryResource, VehicleQuery>(filterResource);
            try
            {
                var queryResult = await repository.GetVehicleList(filter);
                return mapper.Map<QueryResult<Vehicle>, QueryResultResource< SaveVehicleResource>>(queryResult);
            }
            catch (Exception e)
            {
                
                throw e;
            }


        }
        //GET: api/vehicles/5
        [HttpGet("{id}")]
        public async Task<ActionResult<VehicleResource>> GetVehicle(int id)
        {
            //var paymentDetail = await _context.PaymentDetails.FindAsync(id);
            var vehicle = await repository.GetVehicle(id);
            if (vehicle == null)
            {
                throw new Exception("No existe");
            }
            return  mapper.Map<Vehicle, VehicleResource>(vehicle);
            
        }
        // POST: api/vehicles
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> PostVehicle(SaveVehicleResource vehicleResource)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            if (! repository.ModelExists(vehicleResource.ModelId))
            {
                ModelState.AddModelError("ModelId", "Invalid ModelID");
                return BadRequest(ModelState);
            }

            //ICollection<VehicleFeatures> VehicleFeatures = newVehicle.VehicleFeatures;
            var newVehicle = mapper.Map<SaveVehicleResource, Vehicle>(vehicleResource);
            newVehicle.LastUpdate = DateTime.Today ;
            repository.Add(newVehicle);
            await context.SaveChangesAsync();
            newVehicle = await repository.GetVehicle(newVehicle.Id);

            var result= mapper.Map<Vehicle, VehicleResource>(newVehicle);
            return CreatedAtAction("GetVehicles", new { id = newVehicle.Id }, result);
        }

        // Put: api/vehicles/5
        [HttpPut("{id}")]
        [Authorize]
        public async Task<ActionResult<VehicleResource>> PutVehicles(int id, SaveVehicleResource updateVehicle)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            var vehicle = await repository.GetVehicle(id);
            if (id != updateVehicle.Id || vehicle==null)
            {
                ModelState.AddModelError("Vehicle", "Invalid Vehicle");
                return BadRequest(ModelState);
            }
            mapper.Map<SaveVehicleResource, Vehicle>(updateVehicle, vehicle);
            vehicle.LastUpdate = DateTime.Today;
            vehicle.Model = await repository.GetModel(vehicle.ModelId);


            try
            {
                await context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!repository.VehicleExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

             vehicle = await repository.GetVehicle( vehicle.Id);
            vehicle.Model = await repository.GetModel(vehicle.ModelId);

            return mapper.Map<Vehicle, VehicleResource>(vehicle);
            // return CreatedAtAction("GetVehicles", new { id = vehicle.Id }, result);
            
        }

        [HttpDelete("{id}")]
        [Authorize]
        public async Task<ActionResult<Vehicle>> DeleteVehicle(int id)
        {
            var vehicle = await repository.GetVehicle(id,IncludeRelated:false);
            if (vehicle==null)
            {
                return NotFound();
            }
            repository.Remove(vehicle);
            await context.SaveChangesAsync();
            return vehicle;
        }
    }
}
