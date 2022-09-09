using KF.Common.Model.Automapper;
using KF.CommonModel.Models;
using KF.Core.Data;
using KF.Core.DomainModels;
using KF.Services.User;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using System;
using System.Linq;

namespace KF.Service.Tests
{
    public class UserServiceTests
    {
        private EFCoreRepository<User> userRepository;

        [OneTimeSetUp]
        public void Setup()
        {
            //Set up DbContext
            var options = new DbContextOptionsBuilder<KronsoftFarmaContext>();
            options.UseSqlServer("Server=ADISABAULAPDELL;Database=KronsoftFarma;Trusted_Connection=True;");
            KronsoftFarmaContext _dbContext = new KronsoftFarmaContext(options.Options);

            //Set up Repos
            userRepository = new(_dbContext);

            //Set up Automapper
            AutoMapperConfiguration.Init();
            AutoMapperConfiguration.MapperConfiguration.AssertConfigurationIsValid();
        }

        [Test]
        public void GetUsersTest()
        {
            //arrange
            UserService service = GetService();

            //act
            var users = service.GetUsers();

            //assert
            Assert.That(users.Any());

        }

        [Test]
        public void GetUsersByIdTest()
        {
            //arrange
            UserService service = GetService();

            //act
            String source = "2c60a581-0f43-44b6-a4d8-200557638b76";
            Guid result = new Guid(source);
            var product = service.GetUserById(result);

            //assert
            Assert.That(product != null);

        }

        [Test]
        public void CreateUserTest()
        {
            UserService service = GetService();
            Guid createdUserId = Guid.Empty;
            try
            {
                //arrange
                User user = CreateUserModel("Test", "Testing", false);

                //act
                UserModel createdUser = service.CreateUser(user.ToModel());
                createdUserId = createdUser.UserId;

                //assert
                Assert.That(createdUser != null);
                Assert.That(createdUser?.Username == user.Username);
                Assert.That(createdUser?.Password == user.Password);
                Assert.That(createdUser?.IsAdmin == user.IsAdmin);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveUserById(createdUserId);
            }
        }

        [Test]
        public void DeleteUserTest()
        {
            //arrange
            UserService service = GetService();
            User user = CreateUserModel("Test", "Testing", false);
            UserModel createdUser = service.CreateUser(user.ToModel());

            //act
            service.RemoveUserById(createdUser.UserId);
            var deletedUser = service.GetUserById(createdUser.UserId);

            //assert
            Assert.That(deletedUser == null);
        }

        [Test]
        public void UpdateUserTest()
        {
            UserService service = GetService();
            Guid createdUserId = Guid.Empty;

            try
            {
                //arrange
                User user = CreateUserModel("Test", "Testing", false);
                UserModel createdUser = service.CreateUser(user.ToModel());
                createdUserId = createdUser.UserId;

                //act
                Setup();
                service = GetService();
                createdUser.Username = "TTTT";
                service.UpdateUser(createdUser);
                var updatedUser = service.GetUserById(createdUserId);

                //assert
                Assert.That(updatedUser != null);
                Assert.That(updatedUser.Username == "TTTT");

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                service.RemoveUserById(createdUserId);
            }

        }

        private User CreateUserModel(string username, string password, bool isAdmin)
        {
            KF.Core.DomainModels.User user = new()
            {
                Username = username,
                Password = password,
                IsAdmin = isAdmin,
            };

            return user;
        }

        private UserService GetService()
        {
            return new(userRepository);
        }
    }
}