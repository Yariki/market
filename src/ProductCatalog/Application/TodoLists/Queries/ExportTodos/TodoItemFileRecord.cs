using ProductCatalog.Application.Common.Mappings;
using ProductCatalog.Domain.Entities;

namespace ProductCatalog.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; init; }

    public bool Done { get; init; }
}
