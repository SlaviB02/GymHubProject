using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MockQueryable;
using Moq;

namespace GymHub.Services.Tests
{
    public class Tests
    {
        private IList<Gym> gymData = new List<Gym>()
        {
            new Gym()
            {
                Id=Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"),
                Name= "Iron Paradise Gym",
                Address="123 Fitness St, Muscle City, MC 54321",
                ImageUrl= "/images/DefaultGym.jfif",
                Description="A fully equipped gym offering various workout zones, personal training, and group classes.",
                OpeningHour= 6,
                ClosingHour=22
            },
            new Gym()
            {
                Id=Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"),
                Name= "Planet Fitness",
                Address="789 Cardio Blvd, Enduro City, EC 12345",
                ImageUrl= "/images/PlanetFitness.jfif",
                Description="A cardio-focused gym with a wide variety of machines and group cardio classes.",
                OpeningHour= 5,
                ClosingHour=21
            }
        };

        private Mock<IRepository<Gym>> gymRepository;


        [SetUp]
        public void Setup()
        {
            this.gymRepository = new Mock<IRepository<Gym>>();
        }

        [Test]
        public async Task GetAllGymsTest()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
                .Returns(query);

             IGymService gymService=new GymService(this.gymRepository.Object);

            IEnumerable<AllGymViewModel> allGymsActual=await gymService.GetAllGymsAsync();

            Assert.That(allGymsActual,Is.Not.Null);
            Assert.That(this.gymData.Count(),Is.EqualTo(allGymsActual.Count()));
            int i = 0;
            foreach(AllGymViewModel returnedGym in allGymsActual)
            {
                Assert.That(this.gymData[i++].Id, Is.EqualTo(returnedGym.Id));
            }
        }
    }
}