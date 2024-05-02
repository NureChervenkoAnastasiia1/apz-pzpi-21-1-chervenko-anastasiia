using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using TastifyAPI.Entities;
using TastifyAPI.Services;
using TastifyAPI.Models;
using Microsoft.AspNetCore.Identity;

namespace TastifyAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class StaffController : ControllerBase
    {
        private readonly StaffService _staffService;
        private readonly IPasswordHasher<Staff> _passwordHasher;

        public StaffController(StaffService staffService, IPasswordHasher<Staff> passwordHasher)
        {
            _staffService = staffService;
            _passwordHasher = passwordHasher;
        }

        [HttpGet]
        public async Task<ActionResult<List<Staff>>> Get()
        {
            var staffList = await _staffService.GetAllAsync();
            return Ok(staffList);
        }

        [HttpGet("{id:length(24)}")]
        public async Task<ActionResult<Staff>> Get(string id)
        {
            var staff = await _staffService.GetAsync(id);
            if (staff == null)
                return NotFound();

            return staff;
        }

        [HttpPost]
        public async Task<IActionResult> Post(Staff newStaff)
        {
            try
            {
                // Хешируем пароль перед сохранением
                newStaff.PasswordHash = _passwordHasher.HashPassword(newStaff, newStaff.PasswordHash);

                await _staffService.CreateAsync(newStaff);
                return CreatedAtAction(nameof(Get), new { id = newStaff.Id }, newStaff);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPut("{id:length(24)}")]
        public async Task<IActionResult> Update(string id, Staff updatedStaff)
        {
            var existingStaff = await _staffService.GetAsync(id);
            if (existingStaff == null)
                return NotFound();

            updatedStaff.Id = existingStaff.Id;
            await _staffService.UpdateAsync(id, updatedStaff);

            return NoContent();
        }

        [HttpDelete("{id:length(24)}")]
        public async Task<IActionResult> Delete(string id)
        {
            var existingStaff = await _staffService.GetAsync(id);
            if (existingStaff == null)
                return NotFound();

            await _staffService.RemoveAsync(id);
            return NoContent();
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginModel loginModel)
        {
            var staff = await _staffService.GetAsync(loginModel.Login);

            if (staff == null)
                return Unauthorized(new { message = "Invalid email or password" });

            var result = _passwordHasher.VerifyHashedPassword(staff, staff.PasswordHash, loginModel.Password);

            if (result == PasswordVerificationResult.Failed)
                return Unauthorized(new { message = "Invalid email or password" });

            return Ok(staff);
        }

    }
}
