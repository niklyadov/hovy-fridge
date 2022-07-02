﻿using HovyFridge.Api.Data.Entity;
using HovyFridge.Api.Services;
using Microsoft.AspNetCore.Mvc;

namespace HovyFridge.Api.Controllers
{
    public class UsersController : BaseController
    {
        private UsersService _usersService;
        public UsersController(UsersService usersService)
        {
            _usersService = usersService;
        }

        public async Task<IActionResult> GetUsers()
        {
            var result = await _usersService.GetAllAsync();

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        public async Task<IActionResult> GetUserById(long id)
        {
            var result = await _usersService.GetByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        public async Task<IActionResult> CreateUser(User user)
        {
            var result = await _usersService.AddAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        public async Task<IActionResult> UpdateUser(User user)
        {
            var result = await _usersService.UpdateAsync(user);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        public async Task<IActionResult> DeleteUserById(long id)
        {
            var result = await _usersService.DeleteByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }

        public async Task<IActionResult> RestoreUserById(long id)
        {
            var result = await _usersService.RestoreByIdAsync(id);

            if (result.IsSuccess)
            {
                return Ok(result.Value);
            }

            return Problem(string.Join(',', result.Errors));
        }
    }
}