using AutoMapper;
using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.DTO;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Moq;

namespace Ecom.Test.Src
{
    public class AuthServiceTest
    {
        private static IMapper _mapper;

        public AuthServiceTest()
        {
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile<MapperProfile>());
            IMapper mapper = configuration.CreateMapper();
            _mapper = mapper;

        }

        [Fact]
        public async Task Login_ShouldReturn_Valid_Token()
        {
            var userRepoMock = new Mock<IUserRepo>();
            var tokenServiceMock = new Mock<ITokenService>();
            var authService = new AuthService(userRepoMock.Object, tokenServiceMock.Object);

            var validCredentials = new Credentials
            {
                Email = "john@example.com",
                Password = "password123"
            };
            var salt = new byte[16];

            PasswordService.HashPassword("somthing", out string hashedPassword, out byte[] salta);
            PasswordService.VerifyPassword(validCredentials.Password, hashedPassword, salt);

            var userId = new Guid();
            User validUser = new()
            {
                Id = userId,
                FirstName = "John",
                LastName = "Doe",
                Email = "john@example.com",
                Password = hashedPassword,
                Avatar = "avatarUrl",
                Address = "123 Street",
                Zip = "12345",
                City = "Sample City",
                Salt = salt
            };


            userRepoMock.Setup(u => u.GetOneUserByEmailAsync(validCredentials.Email))
                .ReturnsAsync(await Task.FromResult(validUser));
            tokenServiceMock.Setup(t => t.GenerateToken(validUser))
                .Returns("validToken");

            var result = await authService.Login(validCredentials);

            Assert.Equal("validToken", result);
            tokenServiceMock.Verify(t => t.GenerateToken(validUser), Times.Once);
        }


        [Fact]
        public async Task Login_ShouldThrowNotFoundExceptionWhen_UserNotFound()
        {
            var userRepoMock = new Mock<IUserRepo>();
            var tokenServiceMock = new Mock<ITokenService>();
            var authService = new AuthService(userRepoMock.Object, tokenServiceMock.Object);

            var invalidCredintial = new Credentials
            {
                Email = "john@example.com",
                Password = "password123"
            };
            var salt = new byte[16];

            userRepoMock.Setup(u => u.GetOneUserByEmailAsync(invalidCredintial.Email))
                      .ReturnsAsync((User)null);

            await Assert.ThrowsAsync<CustomException>(async () => await authService.Login(invalidCredintial));
        }

        public class GetByEmailUserData : TheoryData<User?, string, UserReadDTO?, Type?>
        {
            public GetByEmailUserData()
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
                Add(user, user.Email, userReadDTO, null);

            }
        }



    }
}