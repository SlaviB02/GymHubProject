using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data.Interfaces;
using GymHub.Services.Data;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MockQueryable;
using GymHub.Web.ViewModels.Class;
using System.Globalization;
using System.Linq.Expressions;

namespace GymHub.Services.Tests
{
    public class ClassServiceTests
    {
        private IList<Class> classData;
        private Mock<IRepository<ClassUser>> classUserRepository;
        private Mock<IRepository<Class>> classRepository;

        [SetUp]
        public void Setup()
        {
            this.classUserRepository = new Mock<IRepository<ClassUser>>();
            this.classRepository = new Mock<IRepository<Class>>();
            Gym g = new Gym()
            {
                Id = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"),
                Name = "Iron Paradise Gym",
                Address = "123 Fitness St, Muscle City, MC 54321",
                ImageUrl = "/images/DefaultGym.jfif",
                Description = "A fully equipped gym offering various workout zones, personal training, and group classes.",
                OpeningHour = 6,
                ClosingHour = 22
            };
            Gym g1 =new Gym()
            {
                Id = Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"),
                Name = "Planet Fitness",
                Address = "789 Cardio Blvd, Enduro City, EC 12345",
                ImageUrl = "/images/PlanetFitness.jfif",
                Description = "A cardio-focused gym with a wide variety of machines and group cardio classes.",
                OpeningHour = 5,
                ClosingHour = 21
            };
            classData = new List<Class>()
            {


                new Class()
                {
                    Id = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d"),
                    Name = "HIIT Training",
                    Instructor = "Mike Turner",
                    StartTimeAndDate = DateTime.Parse("02/11/2024 12:30"),
                    Duration = 45,
                    GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"),
                    Gym=g
                },
                new Class()
                {
                    Id = Guid.Parse("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57e6a9"),
                    Name = "Yoga Basics",
                    Instructor = "Sarah Lee",  
                    StartTimeAndDate = DateTime.Parse("04/05/2024 15:40"),
                    Duration = 60,
                    GymId = Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"),
                    Gym=g1
                }
            };
          

            ClassUser classUser = new ClassUser()
            {
                ClassId = Guid.Parse("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57e6a9"),
                UserId = Guid.Parse("69178581-3d61-4a35-b5c3-2403663b7734")
            };
            classData[1].ClassesUsers.Add(classUser);

        }
        [Test]
        public async Task DeleteClassPositive()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IClassService classService = new ClassService(this.classRepository.Object,classUserRepository.Object);

            var res = await classService.DeleteClassAsync(Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d"));

            Assert.That(res, Is.True);
            Assert.That(classData[0].isDeleted, Is.True);

        }
        [Test]
        public async Task DeleteClassUserNegative()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var res = await classService.DeleteClassAsync(Guid.Parse("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57e6a9"));

            Assert.That(res, Is.False);
            Assert.That(classData[1].isDeleted, Is.False);

        }
        [Test]
        public async Task DeleteNonExistingClass()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var res = await classService.DeleteClassAsync(Guid.Parse("a1e4b2ff-4d1a-4c2a-a0b2-1f5f4f57ebbb"));

            Assert.That(res, Is.False);

        }

        [Test]
        public async Task EditClassTest()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            this.classRepository.Setup(x => x.UpdateAsync(It.IsAny<Class>()))
                .ReturnsAsync(true);

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            EditClassFormModel model = new EditClassFormModel()
            {
                Name = "Basketball",
                Duration = classData[0].Duration,
                DateAndTime = classData[0].StartTimeAndDate.ToString("dd/MM/yyyy HH:mm"),
                Instructor = classData[0].Instructor,
                Id = classData[0].Id,
                GymId = classData[0].GymId,
            };

            var res = await classService.EditClassAsync(model);

            Assert.That(res, Is.True);
            this.classRepository.Verify(x=>x.UpdateAsync(It.IsAny<Class>()), Times.Once());

        }

      
        [Test]
        public async Task GetAllClasses()
        {
            

            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);


            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            IEnumerable<AllClassViewModel> gymClasses = await classService.GetAllClassesAsync();

            Assert.That(gymClasses, Is.Not.Null);
            Assert.That(gymClasses.Count(),Is.EqualTo(2));

        }

        [Test]
        public async Task GetAllClassesForGym()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            var gymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0");

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            IEnumerable<AllClassViewModel> gymClasses = await classService.GetAllClassesForGymAsync(gymId);

            Assert.That(gymClasses,Is.Not.Null);
            Assert.That(gymClasses.Count(), Is.EqualTo(1)); 
            Assert.That(gymClasses.First().Id, Is.EqualTo(Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d")));
        }
        [Test]
        public async Task GetAllClassesForNonExistingGym()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            var gymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8ebbb");

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            IEnumerable<AllClassViewModel> gymClasses = await classService.GetAllClassesForGymAsync(gymId);

            Assert.That(gymClasses, Is.Empty);
        }
        [Test]
        public async Task GetAllClassesForUser()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            var userId = Guid.Parse("69178581-3d61-4a35-b5c3-2403663b7734");

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            IEnumerable<UserClassViewModel> gymClasses = await classService.GetClassesForUserAsync(userId);

            Assert.That(gymClasses, Is.Not.Null);
            Assert.That(gymClasses.Count, Is.EqualTo(1));
        }
        [Test]
        public async Task GetAllClassesForUserWithoutClasses()
        {
            IQueryable<Class> query = classData.BuildMock();

            this.classRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            var userId = Guid.Parse("69178581-3d61-4a35-b5c3-2403663b7333");

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            IEnumerable<UserClassViewModel> gymClasses = await classService.GetClassesForUserAsync(userId);

            Assert.That(gymClasses, Is.Empty);
        }
        [Test]
        public async Task GetDeleteModelPositive()
        {


            Guid classId = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d");

            this.classRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Class, bool>>>()))
                .Returns((Expression<Func<Class, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(classData.FirstOrDefault(compiledPredicate))!;
                });

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var deleteModel = await classService.GetDeleteModelAsync(classId);

            Assert.That(deleteModel, Is.Not.Null);
            Assert.That(deleteModel.Id, Is.EqualTo(classId));
            Assert.That(deleteModel.Name, Is.EqualTo(classData[0].Name));

        }
        [Test]
        public async Task GetDeleteModelNegative()
        {


            Guid classId = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9aaa");

            this.classRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Class, bool>>>()))
                .Returns((Expression<Func<Class, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(classData.FirstOrDefault(compiledPredicate))!;
                });

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var deleteModel = await classService.GetDeleteModelAsync(classId);

            Assert.That(deleteModel, Is.Null);

        }
        [Test]
        public async Task GetEditModelPositive()
        {


            Guid classId = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9c8d");

            this.classRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Class, bool>>>()))
                .Returns((Expression<Func<Class, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(classData.FirstOrDefault(compiledPredicate))!;
                });

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var deleteModel = await classService.GetEditModelAsync(classId);

            Assert.That(deleteModel, Is.Not.Null);
            Assert.That(deleteModel.Id, Is.EqualTo(classId));
            Assert.That(deleteModel.Name, Is.EqualTo(classData[0].Name));

        }
        [Test]
        public async Task GetEditModelNegative()
        {


            Guid classId = Guid.Parse("b2c7d8ff-2a3b-4e1d-b0a3-3d6e9a5f9aaa");

            this.classRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Class, bool>>>()))
                .Returns((Expression<Func<Class, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(classData.FirstOrDefault(compiledPredicate))!;
                });

            IClassService classService = new ClassService(this.classRepository.Object, classUserRepository.Object);

            var deleteModel = await classService.GetEditModelAsync(classId);

            Assert.That(deleteModel, Is.Null);

        }


    }

}
