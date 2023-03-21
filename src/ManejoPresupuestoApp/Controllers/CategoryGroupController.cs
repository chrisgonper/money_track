using ManejoPresupuestoApp.Models;
using ManejoPresupuestoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using System.ComponentModel;

namespace ManejoPresupuestoApp.Controllers
{
    public class CategoryGroupController : Controller
    {
        private readonly IRepositoryCategoryGroup _categoryGroupRepository;
        private readonly IRepositoryUser _repositoryUser;

        public CategoryGroupController(IRepositoryCategoryGroup categoryGroupRepository,
                                       IRepositoryUser repositoryUser)
        {
            this._categoryGroupRepository = categoryGroupRepository;
            this._repositoryUser = repositoryUser;
        }
        public async Task<IActionResult> Index()
        {
            int userId = _repositoryUser.GetCurrentUserId();
            var categoryGroups = await _categoryGroupRepository.GetByUserId(userId);
            return View(categoryGroups);
        }

        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryGroupModel categoryGroupModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryGroupModel);
            }
            int userId = _repositoryUser.GetCurrentUserId();
            categoryGroupModel.UserId = userId;
            await _categoryGroupRepository.Create(categoryGroupModel);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            
            var categoryGroup = await _categoryGroupRepository.Get(id);
            if (categoryGroup is null)
            {
                return RedirectToAction("InvalidResource", "Home");
            }
            return View(categoryGroup);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryGroupModel categoryGroupModel)
        {
            if (!ModelState.IsValid)
            {
                return View(categoryGroupModel);
            }
            categoryGroupModel.UserId = _repositoryUser.GetCurrentUserId();
            await _categoryGroupRepository.Create(categoryGroupModel);
            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                await _categoryGroupRepository.Delete(id);

            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    return Ok(new { Success = false, Message = "Grupo en uso. No es posible borrar!" });
                }
            }
            return Ok(new { Success = true, Message = "Registro borrado correctamente." });
        }
    }
}
