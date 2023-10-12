using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace DDD.Web.Helpers.Filters
{
  public class SearchFilter
  {
    private string? _Interval;
    private bool? _Paginate;
    private int? _Page;
    private int? _ItemsPerPage;

    public int Skip => ItemsPerPage * Page;
    public int Take => ItemsPerPage;

    [FromHeader(Name = "x-search-filter")]
    public string Interval
    {
      get => HttpUtility.UrlDecode(_Interval) ?? "";
      set => _Interval = value != null ? value.ToLower() : "";
    }

    [FromHeader(Name = "x-page")]
    public int Page
    {
      get => _Page ?? 0;
      set => _Page = value;
    }

    [FromHeader(Name = "x-items-per-page")]
    public int ItemsPerPage
    {
      get => _ItemsPerPage ?? 10;
      set => _ItemsPerPage = value;
    }

    [FromHeader(Name = "x-paginate")]
    public bool Paginate
    {
      get => _Paginate ?? true;
      set => _Paginate = value;
    }
  }
}