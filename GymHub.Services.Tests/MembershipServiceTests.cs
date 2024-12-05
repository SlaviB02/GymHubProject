using GymHub.Data.Models;
using GymHub.Data.Models.Enums;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using GymHub.Web.ViewModels.Membership;
using MockQueryable;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace GymHub.Services.Tests
{
    public class MembershipServiceTests
    {
        private Mock<IRepository<Membership>> membershipRepository;
        private IList<Membership> memberships;

        [SetUp]
        public void SetUp()
        {
            this.membershipRepository = new Mock<IRepository<Membership>>();

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
            Gym g1 = new Gym()
            {
                Id = Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"),
                Name = "Planet Fitness",
                Address = "789 Cardio Blvd, Enduro City, EC 12345",
                ImageUrl = "/images/PlanetFitness.jfif",
                Description = "A cardio-focused gym with a wide variety of machines and group cardio classes.",
                OpeningHour = 5,
                ClosingHour = 21
            };
            memberships = new List<Membership>()
            {
                new Membership()
                {
                    Id=Guid.Parse("d9fe40c3-120b-4a33-9ae3-960251c721b9"),
                    UserId = Guid.Parse("69178581-3d61-4a35-b5c3-2403663b7734"),
                    PhoneNumber = "087-444-1234",
                    StartDate = DateTime.Parse("2024-10-20"),
                    Type = MembershipType.Normal,
                    GymId = Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"),
                    Gym=g
                },
                new Membership()
                {
                    Id=Guid.Parse("d2f7da75-d8ff-4ae4-90ac-79dc49922134"),
                    UserId = Guid.Parse("bb75e197-5f40-4287-8746-09cd2112c4ff"),
                    PhoneNumber = "089-422-4334",
                    StartDate = DateTime.Parse("2024-05-22"),
                    Type = MembershipType.Vip,
                    GymId = Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b"),
                    Gym=g1
                }
            };


        }
        [Test]
        public async Task GetAllMembershipsForUser()
        {
            IQueryable<Membership> query = memberships.BuildMock();

            this.membershipRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);

            var userId = Guid.Parse("bb75e197-5f40-4287-8746-09cd2112c4ff");

            var userMemberships=await membershipService.GetAllMembershipsAsync(userId);

            Assert.That(userMemberships, Is.Not.Empty);
            Assert.That(userMemberships.Count(), Is.EqualTo(1));
            Assert.That(userMemberships.First().GymName, Is.EqualTo("Planet Fitness"));

        }
        [Test]
        public async Task GetAllMembershipsForUserWithoutActiveMemberships()
        {
            IQueryable<Membership> query = memberships.BuildMock();

            this.membershipRepository.Setup(x => x.GetAllAttached())
              .Returns(query);

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);

            var userId = Guid.Parse("bb75e197-5f40-4287-8746-09cd2112c411");

            var userMemberships = await membershipService.GetAllMembershipsAsync(userId);

            Assert.That(userMemberships, Is.Empty);

        }
        [Test]
        public async Task AddMembershipUserDoesNotHave()
        {
        
            var userId = Guid.NewGuid();
            var gymId = Guid.NewGuid();
            var membershipInput = new AddMembershipInputModel
            {
                GymId = gymId,
                PhoneNumber = "123-456-7890",
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Type = "Premium"
            };
            this.membershipRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Membership, bool>>>()))
                .Returns((Expression<Func<Membership, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(memberships.FirstOrDefault(compiledPredicate))!;
                });

            this.membershipRepository.Setup(x => x.AddAsync(It.IsAny<Membership>()));

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);


            var res = await membershipService.AddMembershipAsync(membershipInput,userId);

            Assert.That(res, Is.True);

        }
        [Test]
        public async Task AddMembershipUserAlreadyHas()
        {

            var userId = Guid.Parse("bb75e197-5f40-4287-8746-09cd2112c4ff");
            var gymId = Guid.Parse("f9e5b2ab-7edc-4d33-888f-0ab0c9b9fd0b");
            var membershipInput = new AddMembershipInputModel
            {
                GymId = gymId,
                PhoneNumber = "123-456-7890",
                StartDate = DateTime.Now.ToString("yyyy-MM-dd"),
                Type = "Premium"
            };
            this.membershipRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Membership, bool>>>()))
                .Returns((Expression<Func<Membership, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(memberships.FirstOrDefault(compiledPredicate))!;
                });

            this.membershipRepository.Setup(x => x.AddAsync(It.IsAny<Membership>()));

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);


            var res = await membershipService.AddMembershipAsync(membershipInput, userId);

            Assert.That(res, Is.False);

        }
        [Test]
        public async Task CancelExistingMembership()
        {
            this.membershipRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Membership, bool>>>()))
              .Returns((Expression<Func<Membership, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(memberships.FirstOrDefault(compiledPredicate))!;
              });

            this.membershipRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>()));

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);

            Guid membershipId = Guid.Parse("d2f7da75-d8ff-4ae4-90ac-79dc49922134");

            var res = await membershipService.CancelMembershipAsync(membershipId);

            Assert.That(res,Is.True);
        }
        [Test]
        public async Task CancelNonExistingMembership()
        {
            this.membershipRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Membership, bool>>>()))
              .Returns((Expression<Func<Membership, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(memberships.FirstOrDefault(compiledPredicate))!;
              });

            this.membershipRepository.Setup(x => x.DeleteByIdAsync(It.IsAny<Guid>()));

            IMembershipService membershipService = new MembershipService(membershipRepository.Object);

            Guid membershipId = Guid.Parse("d2f7da75-d8ff-4ae4-90ac-79dc49922000");

            var res = await membershipService.CancelMembershipAsync(membershipId);

            Assert.That(res, Is.False);
        }

    }
}
