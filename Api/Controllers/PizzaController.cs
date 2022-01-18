using AutoMapper;
using Api.DataAccess.Data.Repository.IRepository;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Api.Controllers
{
#pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class PizzaController : BaseApiController
    {
        //Unit of work to access DB
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITokenService _tokenService;
        private readonly IMapper _mapper;

        //Injecting my dependancies into the DI Container using Dependancy Injection.
        public PizzaController(IUnitOfWork unitOfWork, ITokenService tokenService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _tokenService = tokenService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get list of all Books on sale.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(IEnumerable<PizzaDto>))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Bad Request
        [ProducesDefaultResponseType] //Any error that doesn't fall above
        public async Task<ActionResult<IEnumerable<PizzaDto>>> GetPizzas()
        {
            //Below we have to use async version of ToList
            var pizza = await _unitOfWork.Pizza.GetPizzaAsync();

            if (pizza == null)
            {
                return NotFound();
            }

            //Map to DTO
            //Source: Pizza
            //Output: <IEnumerable<BookDto>>

            var pizzaToReturn = _mapper.Map<IEnumerable<PizzaDto>>(pizza);

            //Wrap result in an OK response
            return Ok(pizzaToReturn);
        }

        /// <summary>
        /// Get individual Pizza
        /// </summary>
        /// <returns></returns>
        [HttpGet("{p}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PizzaDto))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not Found
        [ProducesDefaultResponseType] //Any error that doesn't fall above
        public async Task<ActionResult<PizzaDto>> GetPizza(string p)
        {
            if (String.IsNullOrWhiteSpace(p))
            {
                //BadRequest 400
                return BadRequest("Invalid pizzaName.");
            }
            var book = await _unitOfWork.Pizza.GetBookByBookNameAsync(p);

            //FindAsync method rather than Find. Map
            return _mapper.Map<PizzaDto>(book);
        }

        //Update Pizza
        [HttpPut]
        [Authorize]//Only users who have been authenticated and part of admin role, can have access to this. Roles coming soon.
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)] //Bad Request
        [ProducesResponseType(StatusCodes.Status404NotFound)] //Not Found
        [ProducesDefaultResponseType] //Any error that doesn't fall above
        public async Task<ActionResult> UpdateBook(PizzaUpdateDto bookUpdateDto)
        {
            //Get pizza from DB by Id
            var pizza = await _unitOfWork.Pizza.GetBookByIdAsync(bookUpdateDto.Id);

            if (pizza == null)
            {
                //404
                return NotFound();
            }

            //Map the input Dto to our Pizza class
            _mapper.Map(bookUpdateDto, pizza);

            //Pizza object is flagged as being updated by EF
            _unitOfWork.Pizza.Update(pizza);

            //Persist changes to DB
            if (await _unitOfWork.Pizza.SaveAllAsync())
            {
                return NoContent();
            }
            else
            {
                //400
                return BadRequest("Failed to update Pizza.");
            }

        }
    }
#pragma warning restore CS1591 // Missing XML comment for publicly visible type or member
}
