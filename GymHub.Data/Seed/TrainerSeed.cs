using GymHub.Data.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Data.Seed
{
    public class TrainerSeed : IEntityTypeConfiguration<Trainer>
    {
        public void Configure(EntityTypeBuilder<Trainer> builder)
        {
            string inputJson = File.ReadAllText("../GymHub.Data/Seed/TrainerSeed.json");
            List<Trainer> trainers = JsonConvert.DeserializeObject<List<Trainer>>(inputJson)!;


            builder.HasData(trainers);
        }
    }
}
