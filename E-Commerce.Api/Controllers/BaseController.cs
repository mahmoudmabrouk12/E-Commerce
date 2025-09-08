using AutoMapper;
using E_Commerce.Core.InterFaces;
using E_Commerce.InfraStructure.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace E_Commerce.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BaseController : ControllerBase
    {
        protected readonly IUnitOfWork work;
        protected readonly  IMapper mapper;

        public BaseController(IUnitOfWork work, IMapper mapper)
        {
            this.work = work;
            this.mapper = mapper;
        }
    }
}
