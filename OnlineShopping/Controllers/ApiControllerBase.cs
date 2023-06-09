using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace OnlineShopping.Controllers
{
    public abstract class ApiControllerBase<TModel> : ControllerBase
    {
        protected IMapper _mapper => HttpContext.RequestServices.GetRequiredService<IMapper>();
        protected IValidator<TModel> _validator => HttpContext.RequestServices.GetRequiredService<IValidator<TModel>>();
    }
}
