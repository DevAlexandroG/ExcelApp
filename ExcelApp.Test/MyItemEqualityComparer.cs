using ExcelApp.Data;

namespace ExcelApp.Test;

public class MyItemEqualityComparer : IEqualityComparer<ProductInfo>
{
    public bool Equals(ProductInfo x, ProductInfo y)
    {
        if (ReferenceEquals(x, y))
            return true;

        if (ReferenceEquals(x, null) || ReferenceEquals(y, null))
            return false;

        return x.Id == y.Id &&
               x.Name == y.Name &&
               x.Cipher == y.Cipher &&
               x.EffectiveDateFrom == y.EffectiveDateFrom &&
               x.EffectiveDateBy == y.EffectiveDateBy;
    }

    public int GetHashCode(ProductInfo obj)
    {
        return obj.Id.GetHashCode();
    }
}