using MySql.Data.MySqlClient;
using Spendr.Models;
using Dapper;
using shortid;
using shortid.Configuration;

namespace Spendr.DAL
{
    public class ExpenseService
    {
        private readonly MySqlConnection conn = DbConnection.GetConnection;
        public List<Expense> GetAll()
        {
            List<Expense> expenses = new List<Expense>();
            string sql = @"select id as 'Id', expense_id as 'ExpenseId', amount as 'Amount', created_on as 'CreatedOn' from expenses";
            expenses = conn.Query<Expense>(sql).ToList();
            return expenses;
        }

        public void Create(Expense e)
        {
            var options = new GenerationOptions(length: 9, useSpecialCharacters: false);
            string sql = @"insert into expenses (expense_id, title, amount, location, comment, category, card_type) 
            values (@ExpenseId, @Title, @Amount, @Location, @Comment, @Category, @CardType)";
            string id = ShortId.Generate(options);
            var result = conn.Execute(sql, new
            {
                ExpenseId = id,
                Title = e.Title,
                Amount = e.Amount,
                Location = e.Location,
                Comment = e.Comment,
                CardType = e.CardType,
                Category = e.Category
            });
            Console.WriteLine(result);
        }
    }
}