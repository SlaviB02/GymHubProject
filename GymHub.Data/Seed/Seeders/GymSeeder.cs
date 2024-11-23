using GymHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Data.Seed.Seeders
{
    public class GymSeeder : IEntityTypeConfiguration<Gym>
    {
        public void Configure(EntityTypeBuilder<Gym> builder)
        {
            string inputJson = File.ReadAllText("../GymHub.Data/Seed/Files/Gyms.json");
            List<Gym> gyms = JsonConvert.DeserializeObject<List<Gym>>(inputJson)!;


            builder.HasData(gyms);
        }
    }
}
