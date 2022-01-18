using AutoMapper;
using Api.DataAccess.Data.Repository.IRepository;
using Api.Models;
using Api.Models.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Api.Api.Controllers
{
    public class AppUserOrderController : BaseApiController
    {
        //Unit of work to access DB
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        //Injecting my dependancies into the DI Container using Dependancy Injection.
        public AppUserOrderController(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        /// <summary>
        /// Used for persisting Pizza orders to the underlying database.
        /// </summary>
        /// <param name="Pizza"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        public async Task<ActionResult> AddAppUserOrder(IEnumerable<Pizza> Pizza)
        {
            //Get hold of the username from the token, not by username as we cant trust this.
            //as someone could have stolen the token and is trying to use it to update a different user.
            var username = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            //Get appUser from DB
            var appUser = await _unitOfWork.AppUser.GetUserByUsernameAsync(username);

            //Object to store each AppUser Pizza
            AppUserOrder appUserOrder = new AppUserOrder();

            //User not found
            if (appUser == null)
            {
                //404
                return NotFound();
            }

            //Check if object contains any data
            if (Pizza != null)
            {
                foreach (var item in Pizza)
                {
                    //AppUserID: Retrieved from JWT token. Not user.
                    appUserOrder.AppUserId = appUser.Id;
                    appUserOrder.PizzaId = item.Id;

                    //These 2 fields come from the DB. Preventing hackers overriding these values from front end.
                    appUserOrder.OrderPizzaName = item.PizzaName;
                    appUserOrder.PizzaPurchasePrice = item.PizzaPrice;

                    appUserOrder.OrderDate = DateTime.Now;
                    appUserOrder.UnorderOrderDate = DateTime.Now;
                    appUserOrder.OrderIsDeleted = 0;

                    //Add to EF memory. Not Persisted to DB yet.
                    _unitOfWork.AppUserOrder.Add(appUserOrder);
                }
            }

            //Persist changes to DB
            if (await _unitOfWork.AppUserOrder.SaveAllAsync())
            {
                return NoContent();
            }
            else
            {
                return BadRequest("Failed to update user.");
            }

        }

    }
}
