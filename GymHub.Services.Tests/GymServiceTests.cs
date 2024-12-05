using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Gym;
using Microsoft.EntityFrameworkCore.Query.Internal;
using MockQueryable;
using Moq;
using System.Collections.Generic;
using System.Linq.Expressions;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace GymHub.Services.Tests
{
    public class Tests
    {

        private IList<Gym> gymData;
        private Mock<IRepository<Gym>> gymRepository;


        [SetUp]
        public void Setup()
        {
            this.gymRepository = new Mock<IRepository<Gym>>();
             gymData = new List<Gym>()
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
            Class gymClass = new Class()
            {
                Id = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d"),
                Name = "HIIT Training",
                Instructor = "Mike Turner",
                StartTimeAndDate = DateTime.Parse("2023-11-07T09:30:00"),
                Duration = 45,
            };
            gymData[1].Classes.Add(gymClass);
        }

        [Test]
        public async Task GetAllGymsWithoutSearchText()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
                .Returns(query);

             IGymService gymService=new GymService(this.gymRepository.Object);

            IEnumerable<AllGymViewModel> allGymsActual=await gymService.GetAllGymsAsync("");

            Assert.That(allGymsActual,Is.Not.Null);
            Assert.That(this.gymData.Count(),Is.EqualTo(allGymsActual.Count()));
            int i = 0;
            foreach(AllGymViewModel returnedGym in allGymsActual)
            {
                Assert.That(this.gymData[i++].Id, Is.EqualTo(returnedGym.Id));
            }
        }
        [Test]
        public async Task GetAllGymsWithSearchText()
        {

            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
                .Returns(query);

            IGymService gymService = new GymService(this.gymRepository.Object);

            IEnumerable<AllGymViewModel> allGymsActual = await gymService.GetAllGymsAsync("Pl");
            Assert.That(allGymsActual, Is.Not.Null);
            Assert.That(allGymsActual.Count(),Is.EqualTo(1));
            Assert.That(allGymsActual.First().Id, Is.EqualTo(Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b")));
           
        }
        [Test]
        public async Task DeleteGymPositive()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IGymService gymService = new GymService(this.gymRepository.Object);

            var res = await gymService.DeleteGymAsync(Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"));

            Assert.That(res,Is.True);
            Assert.That(this.gymData[0].IsDeleted, Is.True);
        }
        [Test]
        public async Task DeleteNonExistentGym()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IGymService gymService = new GymService(this.gymRepository.Object);

            var res = await gymService.DeleteGymAsync(Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bcaaaaa"));

            Assert.That(res, Is.False);
        }
        [Test]
        public async Task DeleteGymWithGymClass()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IGymService gymService = new GymService(this.gymRepository.Object);

            var res = await gymService.DeleteGymAsync(Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"));

            Assert.That(res, Is.False);
            Assert.That(gymData[1].IsDeleted, Is.False);
        }
        [Test]
        public async Task GetDeleteModelPositive()
        {
            

            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var deleteModel = await gymService.GetDeleteModelAsync(GymId);

            Assert.That(deleteModel,Is.Not.Null);
            Assert.That(deleteModel.Id, Is.EqualTo(GymId));
            Assert.That(deleteModel.Name,Is.EqualTo(gymData[0].Name));

        }
        [Test]
        public async Task GetDeleteModelNegative()
        {
            

            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8eaaa");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var deleteModel = await gymService.GetDeleteModelAsync(GymId);

            Assert.That(deleteModel,Is.Null);

        }
        [Test]
        public async Task GetDetailsForGymPositve()
        {


            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var detailsModel = await gymService.GetDetailsGymAsync(GymId);

            Assert.That(detailsModel, Is.Not.Null);
            Assert.That(detailsModel.Id, Is.EqualTo(GymId));
            Assert.That(detailsModel.Name, Is.EqualTo(gymData[0].Name));
        }
        [Test]
        public async Task GetDetailsForGymNegative()
        {


            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8eaaa");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var detailsModel = await gymService.GetDetailsGymAsync(GymId);

            Assert.That(detailsModel, Is.Null);
        }
        [Test]
        public async Task GetAllGymNames()
        {
            IQueryable<Gym> query = gymData.BuildMock();

            this.gymRepository.Setup(x => x.GetAllAttached())
                .Returns(query);

            IGymService gymService = new GymService(this.gymRepository.Object);

            var gymNames = await gymService.GetGymNamesAsync();

            Assert.That(gymNames, Is.Not.Null);
            Assert.That(gymData.Count(),Is.EqualTo(gymNames.Count()));
            int i = 0;
            foreach (GymNamesViewModel gym in gymNames)
            {
                Assert.That(this.gymData[i++].Name, Is.EqualTo(gym.Name));
            }
        }
        [Test]
        public async Task GetEditModelForGymPositve()
        {


            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var editModel = await gymService.GetEditModelAsync(GymId);

            Assert.That(editModel, Is.Not.Null);
            Assert.That(editModel.Id, Is.EqualTo(GymId));
            Assert.That(editModel.Name, Is.EqualTo(gymData[0].Name));
        }
        [Test]
        public async Task GetEditModelForGymNegative()
        {


            Guid GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8eaaa");

            this.gymRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Gym, bool>>>()))
                .Returns((Expression<Func<Gym, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(gymData.FirstOrDefault(compiledPredicate))!;
                });

            IGymService gymService = new GymService(this.gymRepository.Object);

            var editModel = await gymService.GetEditModelAsync(GymId);

            Assert.That(editModel, Is.Null);
        }



    }
}