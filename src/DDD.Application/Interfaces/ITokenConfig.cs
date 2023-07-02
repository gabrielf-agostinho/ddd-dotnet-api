namespace DDD.Application.Interfaces
{
  public interface ITokenConfig
  {
    public string? Audience { get; set; }
    public string? Issuer { get; set; }
    public string? Secret { get; set; }
    public int MinutesToExpire { get; set; }
    public int DaysToRefresh { get; set; }
  }
}