namespace ExcelApp.Data;

public class ProductInfo
{
    public int? Id { get; set; }
    public string Name { get; set; }

    public string Cipher { get; set; }

    public DateTime? EffectiveDateFrom { get; set; }

    public DateTime? EffectiveDateBy { get; set; }

    public int IsExt { get; set; }

    public int? ExitID { get; set; }
}