namespace ExcelApp.Data;

public class TableColumn
{
    public string DisplayName { get; set; }
    public string FieldName { get; set; }

    public string? GetValue(object item)
    {
        var property = item.GetType().GetProperty(FieldName);
        var value = property?.GetValue(item);
        return value?.ToString();
    }
}