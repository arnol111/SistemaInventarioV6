using Microsoft.AspNetCore.Mvc;
using SistemaInventario.AccesoDatos.Context;
using SistemaInventario.AccesoDatos.Repository.IRepository;
using SistemaInventario.Modelos.Models;

namespace SistemaInventarioV6.Areas.Admin.Controllers;

[Area("Admin")]
public class BodegaController : Controller
{
    private readonly IUnitOfWork _unitOfWork;

    public BodegaController(IUnitOfWork unitOfWork)
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
        var bodega = new Bodega();

        if (id == null)
        {
            //create new Bodega
            bodega.Estado = true;
            return View(bodega);
        }
        //Update Bodega
        bodega = await _unitOfWork.Bodega.GetById(id.Value);

        if (bodega == null)
        {
            return NotFound();
        }
        return View(bodega);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Upsert(Bodega bodega)
    {
        if (ModelState.IsValid)
        {
            if (bodega.Id == 0)
            {
                await _unitOfWork.Bodega.Add(bodega);
            }
            else
            {
                _unitOfWork.Bodega.Update(bodega);
            }

            await _unitOfWork.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }
        return View(bodega);
    }
    
    #region API

    [HttpGet]
    public async Task<IActionResult> GetBodegas()
    {
        var allBodegas = await _unitOfWork.Bodega.GetAll();
        return Json(new {data = allBodegas});
    }

    [HttpPost]
    public async Task<IActionResult> Delete(int id)
    {
        var bodegaDb = await _unitOfWork.Bodega.GetById(id);

        if (bodegaDb == null)
        {
            return Json(new { success = false, message = "El bodega seleccionada no existe." });
        }
        _unitOfWork.Bodega.Remove(bodegaDb);
        await _unitOfWork.SaveChangesAsync();
        return Json(new { success = true, message = "Bodega eliminada correctamente." });
    }
    #endregion
}