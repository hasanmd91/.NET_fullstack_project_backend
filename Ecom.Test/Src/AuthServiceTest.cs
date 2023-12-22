using Ecom.Core.src.Abstraction;
using Ecom.Core.src.Entity;
using Ecom.Core.src.parameters;
using Ecom.Service.src;
using Ecom.Service.src.Abstraction;
using Ecom.Service.src.Service;
using Ecom.Service.src.Shared;
using Moq;

namespace Ecom.Test.Src
{
    public class AuthServiceTest
    {
        [Fact]
        public void Test1()
        {
            Assert.True(true);
        }

        // [Fact]
        // public async void Login_ShouldInvokeTokenService()
        // {
        //     var repo = new Mock<IUserRepo>();
        //     User user = new() { };
        //     repo.Setup(repo => repo.GetOneUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(user);

        //     var tokenService = new Mock<ITokenService>();
        //     tokenService.Setup(tk => tk.GenerateToken(It.IsAny<User>())).Returns("vskjcdakjc");

        //     var authService = new AuthService(repo.Object, tokenService.Object);
        //     var cred = new Credentials() { Email = "usdscsdcer@mail.com", Password = "vdsvv" };

        //     await authService.Login(It.IsAny<Credentials>());

        //     tokenService.Verify(service => service.GenerateToken(It.IsAny<User>()), Times.Once);
        // }



        // [Theory]
        // [ClassData(typeof(LoginData))]
        // public async void Login_ShouldReturnValidResponse(User? foundUser, string tokenServiceResponse, string expected, Type exceptionType)
        // {
        //     var repo = new Mock<IUserRepo>();
        //     repo.Setup(repo => repo.GetOneUserByEmailAsync(It.IsAny<string>())).ReturnsAsync(foundUser);
        //     var tokenService = new Mock<ITokenService>();
        //     tokenService.Setup(ts => ts.GenerateToken(It.IsAny<User>())).Returns(tokenServiceResponse);
        //     var authService = new AuthService(repo.Object, tokenService.Object);
        //     var passwordService = new Mock<IPasswordService>();
        //     passwordService.Setup(ps => ps.VerifyPassword(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<byte[]>())).Returns(true);
        //     var cred = new Credentials() { Email = "user@mail.com", Password = "password" };

        //     if (exceptionType is not null)
        //     {
        //         await Assert.ThrowsAsync(exceptionType, () => authService.Login(cred));
        //     }
        //     else
        //     {
        //         var response = await authService.Login(cred);

        //         Assert.Equivalent(expected, response);
        //     }
        // }

        // public class LoginData : TheoryData<User?, string?, string?, Type?>
        // {
        //     public LoginData()
        //     {
        //         User user1 = new()
        //         {
        //             FirstName = "x",
        //             LastName = "y",
        //             Email = "xssdy@gmail.com",
        //             Password = "asdkasnj",
        //             Avatar = "sncjklna@njas",
        //             Address = "lcsal",
        //             Zip = "makl",
        //             City = "jsna"
        //         };
        //         Add(user1, "token", "token", null);
        //         Add(null, null, null, typeof(CustomException));
        //         Add(user1, null, null, typeof(CustomException));
        //     }
        // }


    }
}