using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Interfaces;
using PetShop.Models;
using PetShop.Utils;
using PetShop.ViewModels;

namespace PetShop.Controllers
{
    [Route("produtos")]
    public class ProductsController : Controller
    {
        private readonly IProductRepository _repository;
        private readonly IMapper _mapper;

        public ProductsController(IMapper mapper, IProductRepository repository)
        {
            _mapper = mapper;
            _repository = repository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.List());
        }

        [HttpGet("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpGet("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,CostPrice,SalePrice,Description,Type,PhotoFile")] ProductViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var product = _mapper.Map<Product>(viewModel);

                if (viewModel.PhotoFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", viewModel.PhotoFile.FileName);

                    if (!await ImageUtil.Upload(viewModel.PhotoFile, path))
                    {
                        return View(viewModel);
                    }

                    product.Photo = viewModel.PhotoFile.FileName;
                }

                await _repository.Create(product);
                return RedirectToAction(nameof(Index));
            }

            return View(viewModel);
        }

        [HttpGet("editar")]
        public async Task<IActionResult> Edit(int id)
        {
            var product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }
            return View(product);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,CostPrice,SalePrice,Description,Type")] Product product)
        {
            if (ModelState.IsValid)
            {
                await _repository.Update(product);
                return RedirectToAction(nameof(Index));
            }
            return View(product);
        }

        [HttpGet("excluir")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _repository.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            return View(product);
        }

        [HttpPost("excluir"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var product = await _repository.GetById(id);
            if (product != null)
            {
                await ImageUtil.Delete($"wwwroot/imagens/{product.Photo}");
                await _repository.Delete(product);
            }
            
            return RedirectToAction(nameof(Index));
        }
    }
}
