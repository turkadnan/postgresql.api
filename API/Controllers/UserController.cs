using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models.Dtos;
using AutoMapper;
using Business.Layer.Services;
using Business.Layer.Tools;
using Data.Layer.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : BaseController
    {

        #region Variables
        private UserService userService = null;
        private readonly IMapper _mapper;
        #endregion
        public UserController(ApplicationDbContext dbContext, IMapper mapper) : base(dbContext)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            userService = new UserService(_dbContext);

            var list = await userService.GetAllAsyn(l => l.Status != (byte)Enums.Status.Deleted);
            var dtoList = new List<UserDto>();

            foreach (var item in list)
            {
                dtoList.Add(_mapper.Map<UserDto>(item));
            }

            return Ok(dtoList);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Get(int id)
        {
            userService = new UserService(_dbContext);
            var detail = await userService.GetAsync(l => l.Id == id && l.Status != (byte)Enums.Status.Deleted);
            if (detail == null)
            {
                return NotFound();
            }

            return Ok(detail);
        }
    }
}
