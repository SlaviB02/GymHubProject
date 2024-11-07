﻿using GymHub.Data.Models;
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
    public class ClassSeed : IEntityTypeConfiguration<Class>
    {
        public void Configure(EntityTypeBuilder<Class> builder)
        {
            string inputJson = File.ReadAllText("../GymHub.Data/Seed/ClassSeed.json");
            List<Class> classes = JsonConvert.DeserializeObject<List<Class>>(inputJson)!;


            builder.HasData(classes);
        }
    }
}
