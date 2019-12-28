using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using vega.Controllers.Resources;
using vega.Models;
using vega.Persistence;

namespace vega.Controllers
{
            [Route("/api/vehicles/{vehicleId}/photos")]
    public class PhotosController:Controller
    {
        private readonly VegaDbContext context;
        private readonly IHostingEnvironment host;
        private readonly IVehicleRepository repository;
        private readonly IMapper mapper;
        private readonly IPhotoRepository photoRepository;
        private readonly PhotoSettings photoSettings;

        public PhotosController(VegaDbContext context, IHostingEnvironment host, IVehicleRepository repository,
                                IMapper mapper, IOptionsSnapshot<PhotoSettings> options, IPhotoRepository photoRepository)
        {
            this.context = context;
            this.host = host;
            this.repository = repository;
            this.mapper = mapper;
            this.photoRepository = photoRepository;
            this.photoSettings = options.Value;
        }

        [HttpPost]
        public async Task<IActionResult> Upload(int vehicleId, IFormFile File)
        {
            var vehicle = await repository.GetVehicle(vehicleId);
            if (vehicle == null)
                return NotFound();

            if (File == null)
                return BadRequest("Null file");
            if (File.Length == 0)
                return BadRequest("Empty File");
            if (File.Length > photoSettings.MaxBytes)
                return BadRequest("Max size file exceeded");
            if (!photoSettings.IsSupported(File.FileName))
                return BadRequest("Invalid file type");

            var uploadsFolderPath = Path.Combine(host.WebRootPath, "uploads");
            if (!Directory.Exists(uploadsFolderPath))
                Directory.CreateDirectory(uploadsFolderPath);

            var fileName = Guid.NewGuid().ToString() + Path.GetExtension(File.FileName);
            var filePath = Path.Combine(uploadsFolderPath, fileName);

            using(var stream = new FileStream(filePath,FileMode.Create))
            {
                await File.CopyToAsync(stream);
            }

            var photo = new Photo { FileName = fileName };
            photo.VehicleId = vehicleId;

            vehicle.Photos.Add(photo);
            await context.SaveChangesAsync();

            return Ok(mapper.Map<Photo, PhotoResource>(photo));
        }

        [HttpGet]
        public async Task<IEnumerable<PhotoResource>> GetPhotos(int vehicleId)
        {
            var photos = await photoRepository.GetPhotos(vehicleId);

            return mapper.Map<IEnumerable<Photo>, IEnumerable<PhotoResource>>(photos);
        }
    }
}
