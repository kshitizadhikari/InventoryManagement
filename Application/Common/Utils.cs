namespace Application.Common;
public static class Utils
{
    public static Guid ParseOrNewGuid(string guidString)
    {
        return Guid.TryParse(guidString, out var guid) ? guid : Guid.NewGuid();
    }

}
