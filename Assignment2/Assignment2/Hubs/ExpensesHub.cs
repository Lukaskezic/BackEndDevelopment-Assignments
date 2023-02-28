using System;
using Microsoft.AspNetCore.SignalR;
using Assignment2.Models;

namespace Assignment2.Hubs
{
    public class ExpensesHub : Hub<IExpenses>
    {
        public async Task SendMessage(Expense expenses)
        {
            await Clients.All.SendMessage(expenses);
        }
    }
}
