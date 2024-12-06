using GymHub.Data.Models;
using GymHub.Data.Repository.Interfaces;
using GymHub.Services.Data;
using GymHub.Services.Data.Interfaces;
using MockQueryable;
using Moq;
using System.Linq.Expressions;


namespace GymHub.Services.Tests
{
    public class ReviewServiceTests
    {
        private Mock<IRepository<Review>> reviewRepository;
        private IList<Review> reviewsData;

        [SetUp]
        public void SetUp()
        {
            reviewRepository = new Mock<IRepository<Review>>();
            var user1 = new ApplicationUser()
            {
                Id = Guid.Parse("fba4be28-7897-4066-8c23-499e61781ae3"),
                FirstName = "John",
                LastName = "Doe",
                UserName = "Who@email.com"
            };
            var user2 = new ApplicationUser()
            {
                Id = Guid.Parse("46fcebdd-6850-40b4-9b18-c9fcff4197a1"),
                FirstName = "George",
                LastName = "George",
                UserName = "George@email.com"
            };
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
                Id = Guid.Parse("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"),
                Name = "Planet Fitness",
                Address = "789 Cardio Blvd, Enduro City, EC 12345",
                ImageUrl = "/images/PlanetFitness.jfif",
                Description = "A cardio-focused gym with a wide variety of machines and group cardio classes.",
                OpeningHour = 5,
                ClosingHour = 21
            };
            reviewsData = new List<Review>()
            {
                new Review()
                {
                    Id=Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90"),
                    Title="Great Gym Atmosphere",
                    MainBody="This gym has an excellent atmosphere and friendly staff. Highly recommend it.",
                    Rating=9.8,
                    GymId=Guid.Parse("d5f4b10a-42a1-4f63-8c7f-1a7f9bc8e4e0"),
                    UserId=Guid.Parse("fba4be28-7897-4066-8c23-499e61781ae3"),
                    User=user1,
                    Gym=g,
                },
                new Review()
                {
                    Id=Guid.Parse("c5f64d62-1a7e-4932-bd4a-065d1b7ef1d7"),
                    Title="Clean and Spacious",
                    MainBody="The gym is always clean, and there's plenty of space for workouts, even during peak hours.",
                    Rating=9.0,
                    GymId=Guid.Parse("b21d17f5-9c92-4a72-8b77-33c4f08d8df8"),
                    UserId=Guid.Parse("46fcebdd-6850-40b4-9b18-c9fcff4197a1"),
                    User=user2,
                    Gym=g1
                }
            };
           
           
        }
        [Test]
        public async Task UserDeleteHisReviewPositive()
        {

            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .Returns((Expression<Func<Review, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
                });

            IReviewService reviewService=new ReviewService(reviewRepository.Object);

            var userId = Guid.Parse("fba4be28-7897-4066-8c23-499e61781ae3");
            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90");

            var res=await reviewService.UserDeleteReview(userId, reviewId);

            Assert.That(res, Is.True);
            Assert.That(reviewsData[0].IsDeleted, Is.True);



        }
        [Test]
        public async Task UserDeleteNonExistingReview()
        {

            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .Returns((Expression<Func<Review, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
                });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);

            var userId = Guid.Parse("fba4be28-7897-4066-8c23-499e61781bbb");
            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90");

            var res = await reviewService.UserDeleteReview(userId, reviewId);

            Assert.That(res, Is.False);

        }
        [Test]
        public async Task UserDeleteSomeoneElseReview()
        {

            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
                .Returns((Expression<Func<Review, bool>> predicate) =>
                {
                    var compiledPredicate = predicate.Compile();
                    return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
                });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);

            var userId = Guid.Parse("fba4be28-7897-4066-8c23-499e61781ae3");
            var reviewId = Guid.Parse("c5f64d62-1a7e-4932-bd4a-065d1b7ef1d7");

            var res = await reviewService.UserDeleteReview(userId, reviewId);

            Assert.That(res, Is.False);
            Assert.That(reviewsData[1].IsDeleted, Is.False);

        }

        [Test]
        public async Task GetDeleteModelPositive()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
               .Returns((Expression<Func<Review, bool>> predicate) =>
               {
                   var compiledPredicate = predicate.Compile();
                   return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
               });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);

           
            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90");

            var deleteModel=await reviewService.GetDeleteModelAsync(reviewId);

            Assert.That(deleteModel, Is.Not.Null);
            Assert.That(deleteModel.Id, Is.EqualTo(reviewId));
            Assert.That(deleteModel.Title, Is.EqualTo("Great Gym Atmosphere"));


        }
        [Test]
        public async Task GetDeleteModelNegative()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
              .Returns((Expression<Func<Review, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
              });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);


            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386444");

            var deleteModel = await reviewService.GetDeleteModelAsync(reviewId);

            Assert.That(deleteModel, Is.Null);

        }

        [Test]
        public async Task GetEditModelPositive()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
               .Returns((Expression<Func<Review, bool>> predicate) =>
               {
                   var compiledPredicate = predicate.Compile();
                   return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
               });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);


            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90");

            var editModel = await reviewService.GetEditReviewModelAsync(reviewId);

            Assert.That(editModel, Is.Not.Null);
            Assert.That(editModel.Id, Is.EqualTo(reviewId));
            Assert.That(editModel.Title, Is.EqualTo("Great Gym Atmosphere"));


        }
        [Test]
        public async Task GetEditModelNegative()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
              .Returns((Expression<Func<Review, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
              });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);


            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386444");

            var editModel = await reviewService.GetEditReviewModelAsync(reviewId);

            Assert.That(editModel, Is.Null);

        }

        [Test]
        public async Task GetAllReviewsForGymPositive()
        {
            IQueryable<Review> query = reviewsData.BuildMock();

            this.reviewRepository.Setup(r=>r.GetAllAttached())
                .Returns(query);


            IReviewService reviewService = new ReviewService(reviewRepository.Object);


            var gymId = Guid.Parse("b21d17f5-9c92-4a72-8b77-33c4f08d8df8");

            var reviews= await reviewService.GetAllReviewsForGymAsync(gymId);

            Assert.That(reviews, Is.Not.Null);
            Assert.That(reviews.Count(), Is.EqualTo(1));
            Assert.That(reviews.First().Title, Is.EqualTo("Clean and Spacious"));

        }
        [Test]
        public async Task GetAllReviewsForNonExistingGymNegative()
        {
            IQueryable<Review> query = reviewsData.BuildMock();

            this.reviewRepository.Setup(r => r.GetAllAttached())
                .Returns(query);


            IReviewService reviewService = new ReviewService(reviewRepository.Object);


            var gymId = Guid.Parse("b21d17f5-9c92-4a72-8b77-33c4f08d8aaa");

            var reviews = await reviewService.GetAllReviewsForGymAsync(gymId);

            Assert.That(reviews, Is.Empty);


        }
        [Test]
        public async Task GetAllReviews()
        {
            IQueryable<Review> query = reviewsData.BuildMock();

            this.reviewRepository.Setup(r => r.GetAllAttached())
                .Returns(query);


            IReviewService reviewService = new ReviewService(reviewRepository.Object);



            var reviews = await reviewService.GetAllReviewsAsync();

            Assert.That(reviews, Is.Not.Null);
            Assert.That(reviews.Count(), Is.EqualTo(2));
        }
        
        [Test]
        public async Task DeleteReviewPositive()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
              .Returns((Expression<Func<Review, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
              });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);

            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386a90");

            var res=await reviewService.DeleteReviewAsync(reviewId);

            Assert.That(res, Is.True);
            Assert.That(reviewsData[0].IsDeleted, Is.True);

        }
        [Test]
        public async Task DeleteReviewNegative()
        {
            this.reviewRepository.Setup(x => x.FirstOrDefaultAsync(It.IsAny<Expression<Func<Review, bool>>>()))
              .Returns((Expression<Func<Review, bool>> predicate) =>
              {
                  var compiledPredicate = predicate.Compile();
                  return Task.FromResult(reviewsData.FirstOrDefault(compiledPredicate))!;
              });

            IReviewService reviewService = new ReviewService(reviewRepository.Object);

            var reviewId = Guid.Parse("f1a93c78-d9a4-4c11-897a-4a7349386444");

            var res = await reviewService.DeleteReviewAsync(reviewId);

            Assert.That(res, Is.False);

        }

    }
}
