﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using TraceNest.Dto;
using TraceNest.Services.CategoryServices;
using TraceNest.Services.FoundServices;
using TraceNest.Services.LostServices;
using TraceNest.Services.MunicipalityServices;

namespace TraceNest.Controllers
{
	[Route("Found")]
	public class FoundController : Controller
	{
		private readonly IFoundService _services;
		private readonly IMunicipalityService _ser;
		private readonly ICategoryService _categoryService;
		public FoundController(IFoundService service, IMunicipalityService service1, ICategoryService categoryService)
		{
			_services = service;
			_ser = service1;
			_categoryService = categoryService;
		}
		[HttpGet("ReportFound")]
		public IActionResult ReportFound()
		{
			var res = _ser.GetAll();
			var category = _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(res, "Id", "MunicipalityName");
			return View();
		}
		[Authorize]
		[HttpPost("ReportFound")]
		public async Task<IActionResult> ReportFound([FromForm] FoundPostDto dto, IFormFile Photo)
		{
			if (!ModelState.IsValid)
			{
				Console.WriteLine("something fishy");
				foreach (var state in ModelState)
				{
					foreach (var error in state.Value.Errors)
					{
						Console.WriteLine($"Field: {state.Key}, Error: {error.ErrorMessage}");
					}
				}

				var hlo = _ser.GetAll();
				var category = _categoryService.GetAll();
				ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
				ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
				return View(dto);
			}
			Console.WriteLine("hlooooooooooooo");
			if (dto.CategoryId == null && !string.IsNullOrEmpty(dto.CustomCategory))
			{
				Console.WriteLine("Keruniillllllaaaaaaaaa");
				// Save the new category
				var newCategoryId = _categoryService.AddCategoryAsync(dto.CustomCategory.Trim());
				dto.CategoryId = newCategoryId;
			}
			if (dto.MunicipalityId == null && !string.IsNullOrEmpty(dto.customMunicipality))
			{
				
				var newCategoryId =await _ser.AddMuncipality(dto.customMunicipality);
				dto.MunicipalityId = newCategoryId;	
			}
			var userIdString = User.FindFirstValue(ClaimTypes.NameIdentifier);
			if (Guid.TryParse(userIdString, out Guid userId))
			{
				var res = _services.PostFoundProduct(dto, userId, Photo);
				return RedirectToAction("Home", "Home");
			}
			else
			{
				return RedirectToAction("Login", "Auth");
			}
		}
		[HttpGet("Found")]
		public IActionResult Found()
		{
			var hlo = _ser.GetAll();
			var category = _categoryService.GetAll();
			ViewBag.CategoryList = new SelectList(category, "Id", "CategoryName");
			ViewBag.MunicipalityList = new SelectList(hlo, "Id", "MunicipalityName");
			var res = _services.GetAll();

			return View(res);
		}
	}
}
