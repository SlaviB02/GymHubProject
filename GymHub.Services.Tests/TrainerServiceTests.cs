using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Services.Data;
using Moq;
using System.Linq.Expressions;
using MockQueryable;


namespace GymHub.Services.Tests
{
    public class TrainerServiceTests
    {
        private Mock<IRepository<Trainer>> trainerRepository;
        private IList<Trainer> trainerData;

        [SetUp]
        public void SetUp()
        {
            trainerRepository = new Mock<IRepository<Trainer>>();

            trainerData = new List<Trainer>()
            {
                new Trainer()
                {
                    Id=Guid.Parse("a1e7b2ff-4f1a-4c0a-a0c2-1a5f4f57e6d9"),
                    FirstName="John",
                    LastName="Doe",
                    PhoneNumber="+12345678901",
                    Email="johndoe@email.com",
                    ImageUrl="/images/DefaultTrainer.jfif",
                    GymId=Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0")
                },
                new Trainer()
                {
                    Id=Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c"),
                    FirstName="Jane",
                    LastName="Smith",
                    PhoneNumber="+19876543210",
                    Email="janesmith@example.com",
                    ImageUrl="/images/Ka.jfif",
                    GymId=Guid.Parse("b21d17f5-9c92-4a72-8b77-33c4f08d8df8")
                }
            };
        }
        [Test]
        public async Task DeleteTrainerPositive()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
               .Returns((Expression<Func<Trainer, bool>> predicate) =>
               {
                   var compiledPredicate = predicate.Compile();
                   return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
               });

            ITrainerService trainerService=new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c");

            var res = await trainerService.DeleteTrainerAsync(trainerId);

            Assert.That(res, Is.True);
            Assert.That(trainerData[1].isDeleted, Is.True);
        }
        [Test]
        public async Task DeleteTrainerNegative()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
               .Returns((Expression<Func<Trainer, bool>> predicate) =>
               {
                   var compiledPredicate = predicate.Compile();
                   return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
               });

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9aaa");

            var res = await trainerService.DeleteTrainerAsync(trainerId);

            Assert.That(res, Is.False);
        }

        [Test]
        public async Task GetAllTrainers()
        {
            IQueryable<Trainer> query = trainerData.BuildMock();

            this.trainerRepository.Setup(r => r.GetAllAttached())
                .Returns(query);

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainers= await trainerService.GetAllTrainersAsync();

            Assert.That(trainers, Is.Not.Null);
            Assert.That(trainers.Count(), Is.EqualTo(2));
        }

        [Test]
        public async Task GetDeleteModelPositive()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
              .Returns((Expression<Func<Trainer, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
              });

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c");

            var deleteModel = await trainerService.GetDeleteModelAsync(trainerId);

            Assert.That(deleteModel,Is.Not.Null);
            Assert.That(deleteModel.Id,Is.EqualTo(trainerId));
            Assert.That(deleteModel.Name,Is.EqualTo("Jane Smith"));
        }
        [Test]
        public async Task GetDeleteModelNegative()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
              .Returns((Expression<Func<Trainer, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
              });

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9444");

            var deleteModel = await trainerService.GetDeleteModelAsync(trainerId);

            Assert.That(deleteModel, Is.Null);
        }
        [Test]
        public async Task GetEditModelPositive()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
              .Returns((Expression<Func<Trainer, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
              });

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9b9c");

            var editModel = await trainerService.GetEditModelAsync(trainerId);

            Assert.That(editModel, Is.Not.Null);
            Assert.That(editModel.Id, Is.EqualTo(trainerId));
            Assert.That(editModel.FirstName, Is.EqualTo("Jane"));
        }
        [Test]
        public async Task GetEditModelNegative()
        {
            this.trainerRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Trainer, bool>>>()))
              .Returns((Expression<Func<Trainer, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(trainerData.FirstOrDefault(compiledPredicate))!;
              });

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var trainerId = Guid.Parse("b1c3d8ff-2f9b-4d3d-b0f8-3d7e9b5f9444");

            var editModel = await trainerService.GetEditModelAsync(trainerId);

            Assert.That(editModel, Is.Null);
        }
        [Test]
        public async Task GetAllTrainersForGymPositive()
        {
            IQueryable<Trainer> query = trainerData.BuildMock();

            this.trainerRepository.Setup(r => r.GetAllAttached())
                .Returns(query);

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var gymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0");

            var trainers = await trainerService.GetTrainersForGymAsync(gymId);

            Assert.That(trainers, Is.Not.Null);
            Assert.That(trainers.Count(), Is.EqualTo(1));
            Assert.That(trainers.First().FirstName, Is.EqualTo("John"));
        }
        [Test]
        public async Task GetAllTrainersForGymNegative()
        {
            IQueryable<Trainer> query = trainerData.BuildMock();

            this.trainerRepository.Setup(r => r.GetAllAttached())
                .Returns(query);

            ITrainerService trainerService = new TrainerService(trainerRepository.Object);

            var gymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8eaaa");

            var trainers = await trainerService.GetTrainersForGymAsync(gymId);

            Assert.That(trainers, Is.Empty);
        }

    }
}
