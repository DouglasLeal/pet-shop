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
    [Route("pessoas")]
    public class PersonsController : Controller
    {
        private readonly IPersonRepository _repository;
        private readonly IMapper _mapper;

        public PersonsController(IPersonRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.List());
        }

        [HttpGet("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var person = await _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpGet("novo")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("novo")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Type,PhotoFile")] PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = _mapper.Map<Person>(viewModel);

                if (viewModel.PhotoFile != null)
                {
                    var path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", viewModel.PhotoFile.FileName);

                    if (!await ImageUtil.Upload(viewModel.PhotoFile, path))
                    {
                        return View(viewModel);
                    }

                    person.Photo = viewModel.PhotoFile.FileName;
                }

                await _repository.Create(person);
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet("editar")]
        public async Task<IActionResult> Edit(int id)
        {
            var person = await _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            return View(person);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,Type")] PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = _mapper.Map<Person>(viewModel);
                await _repository.Update(person);                
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet("excluir")]
        public async Task<IActionResult> Delete(int id)
        {
            var person = await _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }

            return View(person);
        }

        [HttpPost("excluir"), ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var person = await _repository.GetById(id);
            if (person != null)
            {
                await ImageUtil.Delete($"wwwroot/imagens/{person.Photo}");
                await _repository.Delete(person);
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
