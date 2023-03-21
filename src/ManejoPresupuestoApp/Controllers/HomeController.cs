using AutoMapper;
using ManejoPresupuestoApp.Models;
using ManejoPresupuestoApp.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ManejoPresupuestoApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly IRepositoryUser _repositoryUser;
        private readonly IRepositoryCategoryGroup _repositoryCategoryGroup;
        private readonly IRepositoryTransaction _repositoryTransaction;
        private readonly IRepositoryCategory _repositoryCategory;
        private readonly IMapper _mapper;

        public HomeController(IRepositoryUser repositoryUser,
                              IRepositoryCategoryGroup repositoryCategoryGroup,
                              IRepositoryTransaction repositoryTransaction,
                              IRepositoryCategory repositoryCategory,
                              IMapper mapper)
        {
            this._repositoryUser = repositoryUser;
            this._repositoryCategoryGroup = repositoryCategoryGroup;
            this._repositoryTransaction = repositoryTransaction;
            this._repositoryCategory = repositoryCategory;
            this._mapper = mapper;
        }
        
        public async Task<IActionResult> Index(int month = 0, int year = 0)
        {
            var userId = _repositoryUser.GetCurrentUserId();
            if (month == 0 || year == 0)
            {
                month = DateTime.Now.Month;
                year = DateTime.Now.Year;
            }
            ViewData["CurrentDate"] = new DateTime(year, month, 1);

            var transactions = await _repositoryTransaction.GetByMonth(userId, month, year);

            var transactionsByDate = transactions.GroupBy(t => t.TransactionDate.Date)
                                                  .Select(group => new TransactionGroupByDateModel
                                                  {
                                                      TransactionDate = group.Key,
                                                      Transactions = group.AsEnumerable()
                                                  });
            return View(transactionsByDate);
        }
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            
            var transactionViewModel = new TransactionViewModel()
            {
                CategoryGroups = await GetGroups(),
                TransactionDate = DateTime.Now

            };
            return View(transactionViewModel);
        }
        public async Task<IActionResult> Edit(int id)
        {
            var userId = _repositoryUser.GetCurrentUserId();
            var transaction = await _repositoryTransaction.GetById(id);
            var category = await _repositoryCategory.Get(transaction.CategoryId);
            var categoriesByGroup = await _repositoryCategory.GetByGroupId(category.GroupId);
            var transactionViewModel = new TransactionViewModel()
            {
                CategoryGroups = await GetGroups(),
                GroupId = category.GroupId,
                TransactionDate = transaction.TransactionDate,
                TransactionId= transaction.TransactionId,
                Amount = transaction.Amount,
                CategoryId = transaction.CategoryId,
                Notes = transaction.Notes,
                Categories = categoriesByGroup.Select(c=> new SelectListItem { Text = c.CategoryName, Value = c.CategoryId.ToString()})
            };
            return View(transactionViewModel);
        }
        [HttpPost]
        public async Task<IActionResult> Create(TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                transactionViewModel.CategoryGroups = await GetGroups();
                return View(transactionViewModel);
            }
            var userId = _repositoryUser.GetCurrentUserId();
            transactionViewModel.UserId = userId;

            var transaction = _mapper.Map<TransactionModel>(transactionViewModel);
            
            await _repositoryTransaction.Create(transaction);

            return RedirectToAction("Index");
        }
        [HttpPost]
        public async Task<IActionResult> Edit(TransactionViewModel transactionViewModel)
        {
            if (!ModelState.IsValid)
            {
                transactionViewModel.CategoryGroups = await GetGroups();
                return View(transactionViewModel);
            }
            var userId = _repositoryUser.GetCurrentUserId();
            transactionViewModel.UserId= userId;
            
            var transaction = _mapper.Map<TransactionModel>(transactionViewModel);
            
            await _repositoryTransaction.Create(transaction);

            return RedirectToAction("Index");
        }
        public async Task<IActionResult> Delete(int id)
        {
            await _repositoryTransaction.Delete(id);
            return RedirectToAction("Index");
        }
        public IActionResult InvalidResource()
        {
            return View();
        }
        private async Task<IEnumerable<SelectListItem>> GetGroups()
        {
            int userId = _repositoryUser.GetCurrentUserId();
            var groups = await _repositoryCategoryGroup.GetByUserId(userId);
            var groupsList = groups.ToList();
            groupsList.Insert(0, new CategoryGroupModel { GroupId = 0, GroupName = "-- Seleccione un grupo --" });
            return groupsList.Select(g => new SelectListItem() { Value = g.GroupId.ToString(), Text = g.GroupName });
        }

    }
}