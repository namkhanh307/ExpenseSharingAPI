using Repositories.Entities;
using Repositories.ResponseModel.PersonExpenseModel;

namespace Services.IServices
{
    public interface IPersonExpenseService
    {
        void DeletePersonExpense(string id);
        List<PersonExpense> GetPersonExpenses();
        void PostPersonExpense(PostPersonExpenseModel model);
        void PutPersonExpense(string id, PutPersonExpenseModel model);
    }
}
