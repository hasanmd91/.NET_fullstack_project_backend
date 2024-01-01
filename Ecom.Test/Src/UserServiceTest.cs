using System.Diagnostics.CodeAnalysis;
using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Moq;

namespace Ecom.Test.Src
{
    public class UserServiceTest
    {
        private static IMapper _mapper;

        public UserServiceTest()
        {

            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            IMapper mapper = configuration.CreateMapper();
            _mapper = mapper;
        }


        [Fact]

        public async void GetAllUserAsync_ShouldReturnValidResponse()
        {
            var repo = new Mock<IUserRepo>();
            GetAllParams options = new() { Limit = 20, Offset = 0 };
            User user1 = new()
            {
                FirstName = "John ",
                LastName = "Doe",
                Email = "john.doe@gmail.com",
                Password = "securePassword123",
                Avatar = "https://example.com/avatar/johndoe.jpg",
                Address = "123 Main St",
                Zip = "12345",
                City = "Anytown"
            };
            User user2 = new()
            {
                FirstName = "John ",
                LastName = "Sminth",
                Email = "john.smith@gmail.com",
                Password = "securePassword123",
                Avatar = "https://example.com/avatar/johndoe.jpg",
                Address = "123 Main St",
                Zip = "12345",
                City = "Anytown"
            };

            IEnumerable<User> users = new List<User> { user1, user2 };
            IEnumerable<UserReadDTO> expected = _mapper.Map<IEnumerable<User>, IEnumerable<UserReadDTO>>(users);

            repo.Setup(repo => repo.GetAllUserAsync(options)).Returns(users);

            var userService = new UserService(repo.Object, _mapper);
            var response = await userService.GetAllUserAsync(options);

            Assert.Equivalent(expected, response);
        }



        [Theory]
        [ClassData(typeof(SingleUserData))]
        public async void GetOneUserByIdAsync_ShouldReturn_User(User? response, UserReadDTO? result, Type? type)
        {
            var repo = new Mock<IUserRepo>();
            repo.Setup(repo => repo.GetOneUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            var userService = new UserService(repo.Object, _mapper);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => userService.GetOneUserByIdAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await userService.GetOneUserByIdAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }


        public class SingleUserData : TheoryData<User?, UserReadDTO?, Type?>
        {
            public SingleUserData()
            {
                User user = new() { };
                UserReadDTO userReadDTO = _mapper.Map<User, UserReadDTO>(user);
                Add(user, userReadDTO, null);
                Add(null, userReadDTO, typeof(CustomException));
            }
        }


        [Theory]
        [ClassData(typeof(DeleteSingleUserData))]
        public async void DeleteOneUserAsyncById_ShouldReturnValidResponse(User? foundUser, bool response, bool? result, Type? type)
        {
            var repo = new Mock<IUserRepo>();
            if (foundUser is not null)
            {
                repo.Setup(repo => repo.DeleteOneUserAsync(It.IsAny<Guid>())).ReturnsAsync(response);
            }
            else
            {
                repo.Setup(repo => repo.DeleteOneUserAsync(It.IsAny<Guid>())).ReturnsAsync(false);
            }
            var userService = new UserService(repo.Object, _mapper);

            if (type is not null)
            {
                await Assert.ThrowsAsync(type, () => userService.DeleteOneUserAsync(It.IsAny<Guid>()));
            }
            else
            {
                var expectedResult = await userService.DeleteOneUserAsync(It.IsAny<Guid>());
                Assert.Equivalent(result, expectedResult);
            }
        }
        public class DeleteSingleUserData : TheoryData<User?, bool, bool?, Type?>
        {
            public DeleteSingleUserData()
            {
                User user = new() { };
                Add(user, true, true, null);
                Add(null, false, false, typeof(CustomException));
            }
        }


        [Theory]
        [ClassData(typeof(CreateUserData))]
        public async void CreateOneUserAsync_ShouldReturnValidResponse(User user, UserCreateDTO userCreateDTO, UserReadDTO expected, Type? exceptionType)
        {
            var repo = new Mock<IUserRepo>();
            repo.Setup(repo => repo.CreateOneUserAsync(It.IsAny<User>())).ReturnsAsync(user);
            var userService = new UserService(repo.Object, _mapper);

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => userService.CreateOneUserAsync(userCreateDTO));
            }
            else
            {
                var response = await userService.CreateOneUserAsync(userCreateDTO);
                Assert.Equivalent(expected, response);
            }
        }


        public class CreateUserData : TheoryData<User?, UserCreateDTO, UserReadDTO?, Type?>
        {
            public CreateUserData()
            {
                UserCreateDTO userCreateDTO = new()
                {
                    FirstName = "John ",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "securePassword123",
                    Avatar = "https://example.com/avatar/johndoe.jpg",
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "Anytown"
                };

                User user = _mapper.Map<UserCreateDTO, User>(userCreateDTO);
                UserReadDTO userReadDTO = _mapper.Map<User, UserReadDTO>(user);
                Add(user, userCreateDTO, userReadDTO, null);
                Add(null, userCreateDTO, null, typeof(CustomException));
            }
        }



        [Theory]
        [ClassData(typeof(UserUpdateData))]
        public async void UpdateOneUserAsync_ShouldReturnValidResponse(User user, UserUpdateDTO userUpdateDTO, UserReadDTO expected, Type? exceptionType)
        {
            var repo = new Mock<IUserRepo>();
            repo.Setup(repo => repo.UpdateOneUserAsync(It.IsAny<User>())).ReturnsAsync(user);
            repo.Setup(repo => repo.GetOneUserByIdAsync(It.IsAny<Guid>())).ReturnsAsync(user);
            var userService = new UserService(repo.Object, _mapper);

            if (exceptionType is not null)
            {
                await Assert.ThrowsAsync(exceptionType, () => userService.UpdateOneUserAsync(It.IsAny<Guid>(), userUpdateDTO));
            }
            else
            {
                var result = await userService.UpdateOneUserAsync(It.IsAny<Guid>(), userUpdateDTO);
                Assert.Equivalent(expected, result);
            }
        }

        public class UserUpdateData : TheoryData<User?, UserUpdateDTO, UserReadDTO?, Type?>
        {
            public UserUpdateData()
            {

                User user = new()
                {
                    FirstName = "John ",
                    LastName = "Doe",
                    Email = "john.doe@gmail.com",
                    Password = "securePassword123",
                    Avatar = "https://example.com/avatar/johndoe.jpg",
                    Address = "123 Main St",
                    Zip = "12345",
                    City = "Anytown"
                };

                UserUpdateDTO userUpdateDTO = new()
                {
                    FirstName = "John ",
                    LastName = "Smith",
                    Address = "Green St 123",
                    Zip = "12345",
                    City = "Uk Anytown"
                };


                User Updateduser = _mapper.Map(userUpdateDTO, user);
                UserReadDTO userReadDTO = _mapper.Map<User, UserReadDTO>(Updateduser);
                Add(user, userUpdateDTO, userReadDTO, null);
                Add(null, userUpdateDTO, userReadDTO, typeof(CustomException));
                Add(null, userUpdateDTO, null, typeof(CustomException));
            }
        }

    }
}