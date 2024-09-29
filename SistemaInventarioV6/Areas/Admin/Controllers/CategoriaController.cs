using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;
using SistemaInventario.Utilidades;

namespace SistemaInventarioV6.Areas.Admin.Controllers;
[Area("Admin")]
public class CategoriaController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public CategoriaController(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;    
    }
    
    // GET
    public IActionResult Index()
    {
        return View();
    }
    
    public async Task<IActionResult> Upsert(int? id)
    {
        var categoria = new Categoria();

        if (id == null)
        {
            //create new Bodega
            categoria.Estado = true;
            return View(categoria);
        }
        //Update Bodega
        categoria = await _unitOfWork.Categoria.GetById(id.Value);

        if (categoria == null)
        {
            return NotFound();
        }
        return View(categoria);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Categoria categoria)
    {
        if (ModelState.IsValid)
        {
            if (categoria.Id == 0)
            {
                await _unitOfWork.Categoria.Add(categoria);
                TempData[DS.Success] = "Categoria agregada correctamente";
            }
            else
            {
                _unitOfWork.Categoria.Update(categoria);
                TempData[DS.Success] = "Categoria actualizada correctamente";
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData[DS.Fail] = "Error";
        return View(categoria);
    }
    
    #region API

    [HttpGet]
    public async Task<IActionResult> GetCategories()
    {
        var allCategories = await _unitOfWork.Categoria.GetAll();
        return Json(new {data = allCategories});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var categoriaDB = await _unitOfWork.Categoria.GetById(id);

        if (categoriaDB == null)
        {
            return Json(new { success = false, message = "El categoria seleccionada no existe." });
        }
        _unitOfWork.Categoria.Remove(categoriaDB);
        await _unitOfWork.SaveChangesAsync();
        return Json(new { success = true, message = "Categoria eliminada correctamente." });
    }

    [ActionName("ValidateName")]
    public async Task<IActionResult> ValidateName(string name, int id = 0)
    {
           bool flag = false;
           var categoriasDB = await _unitOfWork.Categoria.GetAll();

           if (id == 0)
           {
               flag = categoriasDB.Any(b=> b.Nombre.ToLower() == name.ToLower().Trim());
           }
           else
           {
               flag = categoriasDB.Any(b=> b.Nombre.ToLower() == name.ToLower().Trim() && b.Id != id);
           }

           if (flag)
           {
               return Json(new { data = true });
           }
           else
           {
               return Json(new { data = false });
           }
    }
    #endregion
}