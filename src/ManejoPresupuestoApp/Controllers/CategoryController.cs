using AutoMapper;
using ManejoPresupuestoApp.Models;
using ManejoPresupuestoApp.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Data.SqlClient;

namespace ManejoPresupuestoApp.Controllers
{
    public class CategoryController : Controller
    {
        private readonly IRepositoryCategory _repositoryCategory;
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryCategoryGroup _repositoryCategoryGroup;
        private readonly IMapper _mapper;

        public CategoryController(IRepositoryCategory repositoryCategory,
                                  IRepositoryUser repositoryUser,
                                  IRepositoryCategoryGroup repositoryCategoryGroup,
                                  IMapper mapper)
        {
            this._repositoryCategory = repositoryCategory;
            this._repositoryUser = repositoryUser;
            this._repositoryCategoryGroup = repositoryCategoryGroup;
            this._mapper = mapper;
        }
        public async Task<IActionResult> Index()
        {
            int userId = _repositoryUser.GetCurrentUserId();
            var categories = await _repositoryCategory.GetByUserId(userId);
            var categoriesByGroup = categories.GroupBy(c => c.GroupName)
                                              .Select(group => new CategoryByGroupModel
                                              {
                                                  GroupName = group.Key,
                                                  Categories = group.Select(g => _mapper.Map<CategoryModel>(g))
                                              });
            return View(categoriesByGroup);
        }

        public async Task<IActionResult> Create()
        {
            var categoryCreateModel = new CategoryCreateModel();
            categoryCreateModel.Groups = await GetGroups();
            return View(categoryCreateModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(CategoryCreateModel categoryCreateModel)
        {
            if (categoryCreateModel.GroupId == 0)
            {
                ModelState.AddModelError(nameof(categoryCreateModel.GroupId), "Seleccione un grupo.");
                categoryCreateModel.Groups = await GetGroups();
                return View(categoryCreateModel);
            }
            if (!ModelState.IsValid)
            {
                categoryCreateModel.Groups = await GetGroups();
                return View(categoryCreateModel);
            }
            var category = new CategoryModel()
            {
                CategoryName = categoryCreateModel.CategoryName,
                GroupId = categoryCreateModel.GroupId,
            };
            await _repositoryCategory.Create(category);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await _repositoryCategory.Get(id);
            if (category is null)
            {
                return RedirectToAction("InvalidResource", "Home");
            }
            var categoryCreate = new CategoryCreateModel()
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                GroupId = category.GroupId,
                Groups = await GetGroups()
            };
            return View(categoryCreate);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryCreateModel categoryCreateModel)
        {
            if (categoryCreateModel.GroupId == 0)
            {
                ModelState.AddModelError(nameof(categoryCreateModel.GroupId), "Seleccione un grupo.");
                categoryCreateModel.Groups = await GetGroups();
                return View(categoryCreateModel);
            }
            if (!ModelState.IsValid)
            {
                categoryCreateModel.Groups = await GetGroups();
                return View(categoryCreateModel);
            }

            var category = new CategoryModel()
            {
                CategoryId = categoryCreateModel.CategoryId,
                CategoryName = categoryCreateModel.CategoryName,
                GroupId = categoryCreateModel.GroupId,
            };

            await _repositoryCategory.Create(category);

            return RedirectToAction("Index");
        }

        private async Task<IEnumerable<SelectListItem>> GetGroups()
        {
            int userId = _repositoryUser.GetCurrentUserId();
            var groups = await _repositoryCategoryGroup.GetByUserId(userId);
            var groupsList = groups.ToList();
            groupsList.Insert(0, new CategoryGroupModel { GroupId = 0, GroupName = "-- Seleccione un grupo --" });
            return groupsList.Select(g => new SelectListItem() { Value = g.GroupId.ToString(), Text = g.GroupName });
        }
        [HttpPost]
        public async Task<IActionResult> GetByGroupId([FromBody] int groupId)
        {
            var categories = await _repositoryCategory.GetByGroupId(groupId);
            var categoriesList = categories.ToList();
            categoriesList.Insert(0, new CategoryModel { CategoryId = 0, CategoryName = "-- Seleccione una categoria --" });
            return Ok(categoriesList);
        }
        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] int id)
        {
            try
            {
                await _repositoryCategory.Delete(id);

            }
            catch (SqlException sqlEx)
            {
                if (sqlEx.Number == 547)
                {
                    return Ok(new { Success = false, Message = "Categoria en uso. No es posible borrar!" });
                }
            }
            return Ok(new { Success = true, Message = "Registro borrado correctamente."});
        }
    }
}
