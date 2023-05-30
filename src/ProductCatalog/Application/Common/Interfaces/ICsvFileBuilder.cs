using ProductCatalog.Application.TodoLists.Queries.ExportTodos;

namespace ProductCatalog.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
