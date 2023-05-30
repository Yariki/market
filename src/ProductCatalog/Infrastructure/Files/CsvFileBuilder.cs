using System.Globalization;
using ProductCatalog.Application.Common.Interfaces;
using ProductCatalog.Application.TodoLists.Queries.ExportTodos;
using ProductCatalog.Infrastructure.Files.Maps;
using CsvHelper;

namespace ProductCatalog.Infrastructure.Files;

public class CsvFileBuilder : ICsvFileBuilder
{
    public byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records)
    {
        using var memoryStream = new MemoryStream();
        using (var streamWriter = new StreamWriter(memoryStream))
        {
            using var csvWriter = new CsvWriter(streamWriter, CultureInfo.InvariantCulture);

            csvWriter.Context.RegisterClassMap<TodoItemRecordMap>();
            csvWriter.WriteRecords(records);
        }

        return memoryStream.ToArray();
    }
}
