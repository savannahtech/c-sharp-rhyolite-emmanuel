using System.Threading.Tasks;
using RhyoliteERP.Models.TokenAuth;
using RhyoliteERP.Web.Controllers;
using Shouldly;
using Xunit;

namespace RhyoliteERP.Web.Tests.Controllers
{
    public class HomeController_Tests: RhyoliteERPWebTestBase
    {
        [Fact]
        public async Task Index_Test()
        {
            await AuthenticateAsync(null, new AuthenticateModel
            {
                UserNameOrEmailAddress = "admin",
                Password = "123qwe"
            });

            //Act
            var response = await GetResponseAsStringAsync(
                GetUrl<HomeController>(nameof(HomeController.Index))
            );

            //Assert
            response.ShouldNotBeNullOrEmpty();
        }
    }
}