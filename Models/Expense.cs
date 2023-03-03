namespace Spendr.Models
{
    public class Expense
    {
        public int Id {get;set;}
        public string? ExpenseId {get;set;}
        public string? Title {get;set;}
        public decimal Amount {get;set;}
        public string? Location {get;set;}
        public string? Comment {get;set;}
        public string? Category {get;set;}
        public string? CardType{get;set;}

        public DateTime CreatedOn {get;set;}
    }

    enum CardType{
        Credit,
        Debit
    }
}