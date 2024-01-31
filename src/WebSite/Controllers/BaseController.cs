using Microsoft.AspNetCore.Mvc;
using MediatR;

namespace WebSite.Controllers
{
    public abstract class BaseController : Controller
    {
        protected readonly ISender _sender;
        public BaseController(ISender sender)
        {
            _sender = sender;
        }
    }
}