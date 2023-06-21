using System.Threading.Tasks;
using RhyoliteERP.Configuration.Dto;

namespace RhyoliteERP.Configuration
{
    public interface IConfigurationAppService
    {
        Task ChangeUiTheme(ChangeUiThemeInput input);
    }
}
