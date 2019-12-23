using UserService.Dtos.Requests;
using UserService.Dtos.Responses;
using UserService.Models;
using UserService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using Microsoft.EntityFrameworkCore;
using UserService.DAL;
using System.Collections.Generic;
using UserService.Entities;

namespace UserService.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class UserController: ControllerBase
    {
        private IUserService _userService;
        private UserOperations ops;

        public UserController(IUserService userService,UserOperations userops)
        {
            _userService = userService;
            ops = userops;


        }
        
        [HttpPut("{id}")]
       [ActionName("update-user")]
        public IActionResult PutUser(Guid id, [FromBody]UserUpdateRequest request)
        {
          
            if (!ModelState.IsValid)
            {
                return BadRequest("Please fill out all fields and enter correct values.");
            }

            try
            {
               _userService.UpdateUser(id,request);
            }
            catch (DbUpdateConcurrencyException)
            {
                 if (!UserExists(id))
                 {
                     return NotFound();
                 }
                 else
                 {
                     throw;
                 }
            }

            return Ok("User is succesfully updated.");
        }

       
        [HttpPut("{id}")]
        [ActionName("change-password")]
        public IActionResult ChangePassword(Guid Id, [FromBody]ChangePassword model)
        {
            try
            {
                _userService.ChangePassword(Id,model.NewPassword);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("Password is succesfully updated.");

        }

        [HttpPut("{id}")]
        [ActionName("ForgotPassword")]
        public IActionResult ForgotPassword(Guid Id)
        {
           
            try
            {
                _userService.NewPassword(Id);
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("New password is sucessfuly created. Message will be sent to that address containing new password");

        }
        private bool UserExists(Guid Id)
        {
            return _userService.GetById(Id) != null;
        }
    }
}