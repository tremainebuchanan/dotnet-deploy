using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Spendr.Models;
using Dapper;
using MySql.Data.MySqlClient;
using Spendr.DAL;
using shortid;
using shortid.Configuration;

namespace Spendr.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly MySqlConnection conn = DbConnection.GetConnection;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
        
    }

    public IActionResult Index()
    {
        List<Expense> expenses = new List<Expense>();
        string sql = @"select id as 'Id', expense_id as 'ExpenseId', title as 'Title', category as 'Category', amount as 'Amount', created_on as 'CreatedOn' from expenses order by created_on desc";
        expenses = conn.Query<Expense>(sql).ToList();
        ViewBag.expenses = expenses;
        return View();
    }

    public IActionResult Create()
    {
        return View();
    }

    [HttpPost]
    public IActionResult Create(string Title, string Amount, string Location, string Comment, string CardType, string Category)
    {
        var options = new GenerationOptions(length: 9, useSpecialCharacters: false);
            string sql = @"insert into expenses (expense_id, title, amount, location, comment, category, card_type) 
            values (@ExpenseId, @Title, @Amount, @Location, @Comment, @Category, @CardType)";
            string id = ShortId.Generate(options);
            var result = conn.Execute(sql, new
            {
                ExpenseId = id,
                Title = Title,
                Amount = Amount,
                Location = Location,
                Comment = Comment,
                CardType = CardType,
                Category = Category
            });
        return RedirectToAction("Index");
        //return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
