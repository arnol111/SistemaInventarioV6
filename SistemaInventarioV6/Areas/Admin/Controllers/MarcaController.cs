using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;
using SistemaInventario.Utilidades;

namespace SistemaInventarioV6.Areas.Admin.Controllers;

[Area("Admin")]
public class MarcaController : Controller
{
    private readonly IUnitOfWork _unitOfWork;
    
    public MarcaController(IUnitOfWork unitOfWork)
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
        var marca = new Marca();

        if (id == null)
        {
            //create new Bodega
            marca.Estado = true;
            return View(marca);
        }
        //Update Bodega
        marca = await _unitOfWork.Marca.GetById(id.Value);

        if (marca == null)
        {
            return NotFound();
        }
        return View(marca);
    }
    
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Marca marca)
    {
        if (ModelState.IsValid)
        {
            if (marca.Id == 0)
            {
                await _unitOfWork.Marca.Add(marca);
                TempData[DS.Success] = "Marca agregada correctamente";
            }
            else
            {
                _unitOfWork.Marca.Update(marca);
                TempData[DS.Success] = "Marca actualizada correctamente";
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        TempData[DS.Fail] = "Error";
        return View(marca);
    }
    
    
    #region API

    [HttpGet]
    public async Task<IActionResult> GetMarcas()
    {
        var allMarcas = await _unitOfWork.Marca.GetAll();
        return Json(new {data = allMarcas});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var marcaDB = await _unitOfWork.Marca.GetById(id);

        if (marcaDB == null)
        {
            return Json(new { success = false, message = "la Marca seleccionada no existe." });
        }
        _unitOfWork.Marca.Remove(marcaDB);
        await _unitOfWork.SaveChangesAsync();
        return Json(new { success = true, message = "Marca eliminada correctamente." });
    }

    [ActionName("ValidateName")]
    public async Task<IActionResult> ValidateName(string name, int id = 0)
    {
        bool flag = false;
        var marcas = await _unitOfWork.Marca.GetAll();

        if (id == 0)
        {
            flag = marcas.Any(b=> b.Nombre.ToLower() == name.ToLower().Trim());
        }
        else
        {
            flag = marcas.Any(b=> b.Nombre.ToLower() == name.ToLower().Trim() && b.Id != id);
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