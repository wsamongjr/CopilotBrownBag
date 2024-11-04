using CopilotAPI.Contracts;
using CopilotAPI.Models;

namespace CopilotAPI.Interfaces
{
    public interface ITodoServices
    {
        Task<Todo> GetByIdAsync(Guid id);
        Task CreateTodoAsync(CreateTodoRequest request);
        Task UpdateTodoAsync(Guid id, UpdateTodoRequest request);
        Task DeleteTodoAsync(Guid id);
    }
}