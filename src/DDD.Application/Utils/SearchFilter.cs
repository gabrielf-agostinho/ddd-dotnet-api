namespace DDD.Application.Utils
{
  public static class SearchFilterExtension
  {
    public static T? Get<T>(this object[] searchFilters, int index)
    {
      return index < searchFilters.Length ? (T)searchFilters[index] : default;
    }
  }
}
