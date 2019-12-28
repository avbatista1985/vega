using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using vega.Persistence;

namespace vega.Models
{
    public static class SeedData
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {

            using (var context = new VegaDbContext(serviceProvider.GetRequiredService<DbContextOptions<VegaDbContext>>()))
            {
                //look for any object in the dataBase
                if (!context.Make.Any())
                {
                    context.AddRange(
                        new Make { Name = "Make1" },
                        new Make { Name = "Make2" },
                        new Make { Name = "Make3" },
                        new Make { Name = "Make4" }
                        );
                    context.SaveChanges();
                    context.AddRange(
                        new Model { Name = "Make1-ModelA", MakeId = context.Make.First(m => m.Name == "Make1").Id },
                        new Model { Name = "Make1-ModelB", MakeId = context.Make.First(m => m.Name == "Make1").Id },
                        new Model { Name = "Make1-ModelC", MakeId = context.Make.First(m => m.Name == "Make1").Id },
                        new Model { Name = "Make1-ModelD", MakeId = context.Make.First(m => m.Name == "Make1").Id },
                        new Model { Name = "Make2-ModelA", MakeId = context.Make.First(m => m.Name == "Make2").Id },
                        new Model { Name = "Make2-ModelB", MakeId = context.Make.First(m => m.Name == "Make2").Id },
                        new Model { Name = "Make2-ModelC", MakeId = context.Make.First(m => m.Name == "Make2").Id },
                        new Model { Name = "Make2-ModelD", MakeId = context.Make.First(m => m.Name == "Make2").Id },
                        new Model { Name = "Make3-ModelA", MakeId = context.Make.First(m => m.Name == "Make3").Id },
                        new Model { Name = "Make3-ModelB", MakeId = context.Make.First(m => m.Name == "Make3").Id },
                        new Model { Name = "Make3-ModelC", MakeId = context.Make.First(m => m.Name == "Make3").Id },
                        new Model { Name = "Make3-ModelD", MakeId = context.Make.First(m => m.Name == "Make3").Id },
                        new Model { Name = "Make4-ModelA", MakeId = context.Make.First(m => m.Name == "Make4").Id },
                        new Model { Name = "Make4-ModelB", MakeId = context.Make.First(m => m.Name == "Make4").Id },
                        new Model { Name = "Make4-ModelC", MakeId = context.Make.First(m => m.Name == "Make4").Id },
                        new Model { Name = "Make4-ModelD", MakeId = context.Make.First(m => m.Name == "Make4").Id }
                        );

                    context.SaveChanges();
                }
                if (!context.Features.Any())
                {
                    context.AddRange(
                    new Feature { Name = "Features1" },
                    new Feature { Name = "Features2" },
                    new Feature { Name = "Features3" },
                    new Feature { Name = "Features4" },
                    new Feature { Name = "Features5" },
                    new Feature { Name = "Features6" }
                    );
                context.SaveChanges();
                }
                if (!context.ModelFeatures.Any())
                {
                    List<int> features = new List<int>();
                    List<int> model = new List<int>();
                    foreach (Feature item in context.Features)
                    {
                        features.Add(item.Id);
                    }
                    foreach (Model item in context.Models)
                    {
                        model.Add(item.Id);
                    }


                    foreach (int item in features)
                    {
                        foreach (var item2 in model)
                        {
                            context.AddRange(
                            new ModelFeatures { FeatureId = item, ModelId = item2 }
                            );
                        }                       
                    }
                    

                    
                    context.SaveChanges();
                }
            }
        }

    }
}
