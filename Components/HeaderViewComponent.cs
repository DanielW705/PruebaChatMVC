using Microsoft.AspNetCore.Mvc;
using PruebaChatMVC.UseCase;
using System.Threading.Tasks;

namespace PruebaChatMVC.Components
{
    public class HeaderViewComponent: ViewComponent
    {
        private readonly GetUserInformationUseCase _getUserInformationUseCase;
        public HeaderViewComponent(GetUserInformationUseCase getUserInformationUseCase)
        {
            _getUserInformationUseCase = getUserInformationUseCase;
        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var User = await _getUserInformationUseCase.Execute();
            return View(User);
        }
    }
}
