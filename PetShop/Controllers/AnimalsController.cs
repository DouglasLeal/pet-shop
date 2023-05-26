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
    [Route("animais")]
    public class AnimalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IMapper _mapper;
        private readonly IAnimalRepository _repository;

        public AnimalsController(IAnimalRepository repository, ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
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
            var animal = await _repository.GetById(id);

            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpGet("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Color,Observation,Type,PhotoFile")] AnimalViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var animal = _mapper.Map<Animal>(viewModel);

                if (viewModel.PhotoFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", viewModel.PhotoFile.FileName);

                    if (!await ImageUtil.Upload(viewModel.PhotoFile, path))
                    {
                        return View(viewModel);
                    }

                    animal.Photo = viewModel.PhotoFile.FileName;
                }

                await _repository.Create(animal);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet("editar")]
        public async Task<IActionResult> Edit(int id)
        {
            var animal = await _repository.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }
            return View(animal);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Color,Observation,Type")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                await _repository.Update(animal);                                
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        [HttpGet("excluir")]
        public async Task<IActionResult> Delete(int id)
        {
            var animal = await _repository.GetById(id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        [HttpPost("excluir"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var animal = await _repository.GetById(id);
            if (animal != null)
            {
                await ImageUtil.Delete($"wwwroot/imagens/{animal.Photo}");
                await _repository.Delete(animal);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
