using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using PetShop.Data;
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

        public AnimalsController(ApplicationDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        [HttpGet("")]      
        public async Task<IActionResult> Index()
        {
              return _context.Animals != null ? 
                          View(await _context.Animals.ToListAsync()) :
                          Problem("Entity set 'ApplicationDbContext.Animals'  is null.");
        }

        [HttpGet("detalhes")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);
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

                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(viewModel);
        }

        [HttpGet("editar")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
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
            if (id != animal.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(animal);
        }

        [HttpGet("excluir")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .FirstOrDefaultAsync(m => m.Id == id);
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
            if (_context.Animals == null)
            {
                return Problem("Entity set 'ApplicationDbContext.Animals'  is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                await ImageUtil.Delete($"wwwroot/imagens/{animal.Photo}");
                _context.Animals.Remove(animal);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
          return (_context.Animals?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
