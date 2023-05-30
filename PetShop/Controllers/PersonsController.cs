using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
using PetShop.Enum;
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
        private readonly IAnimalRepository _animalRepository;
        private readonly IMapper _mapper;

        public PersonsController(IPersonRepository repository, IMapper mapper, IAnimalRepository animalRepository)
        {
            _repository = repository;
            _mapper = mapper;
            _animalRepository = animalRepository;
        }

        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return View(await _repository.List());
        }

        [HttpGet("clientes")]
        public async Task<IActionResult> Clientes()
        {
            return View("Index", await _repository.ListByType(PersonType.Customer));
        }

        [HttpGet("funcionarios")]
        public async Task<IActionResult> Funcionarios()
        {
            return View("Index", await _repository.ListByType(PersonType.Employee));
        }

        [HttpGet("detalhes")]
        public async Task<IActionResult> Details(int id)
        {
            var person = await _repository.GetById(id);
            if (person == null)
            {
                return NotFound();
            }
            ViewData["animals"] = await _animalRepository.ListByOwner(id);

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
            var viewModel = _mapper.Map<PersonViewModel>(person);
            return View(viewModel);
        }

        [HttpPost("editar")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhotoFile")] PersonViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var person = await _repository.GetById(id);
                _mapper.Map(viewModel, person);

                if (viewModel.PhotoFile != null)
                {
                    var path = "";
                    if (person.Photo != null) 
                    {
                         Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", person.Photo);
                        await ImageUtil.Delete(person.Photo);
                    }                    
                    path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/imagens/", viewModel.PhotoFile.FileName);
                    await ImageUtil.Upload(viewModel.PhotoFile, path);
                    person.Photo = viewModel.PhotoFile.FileName;
                }
                

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
