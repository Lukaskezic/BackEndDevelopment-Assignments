using Assignment2.Models;

namespace Assignment2.Hubs
{
    public interface IExpenses
    {
        Task SendMessage(Expense expenses);
    }
}
