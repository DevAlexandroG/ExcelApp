using ExcelApp.Data;

namespace ExcelApp.Services;

/// <summary>
/// The TableService class provides methods for retrieving table column data asynchronously.
/// </summary>
public class TableService
{
    /// <summary>
    /// Retrieves a list of TableColumn objects representing the columns of a table.
    /// </summary>
    /// <returns>A Task representing the asynchronous operation that returns a List of TableColumn objects.</returns>
    public Task<List<TableColumn>> GetTableColumnAsync()
    {
        return Task.FromResult(new List<TableColumn>
        {
            new TableColumn { DisplayName = "ID", FieldName = "Id" },
            new TableColumn { DisplayName = "Наименование", FieldName = "Name" },
            new TableColumn { DisplayName = "Шифр", FieldName = "Cipher" },
            new TableColumn { DisplayName = "Дата действия с", FieldName = "EffectiveDateFrom" },
            new TableColumn { DisplayName = "Дата действия по", FieldName = "EffectiveDateBy" },
            new TableColumn { DisplayName = "IsExt", FieldName = "IsExt" },
            new TableColumn { DisplayName = "ExtID", FieldName = "ExitID" },
        });
    }
}