namespace DDD.Application.Utils
{
  public class Pagination<TGetDTO>
  {
    public int Total { get; set; }
    public IEnumerable<TGetDTO> List { get; set; } = new List<TGetDTO>();

    public Pagination(int total, IEnumerable<TGetDTO> lista)
    {
      Total = total;
      List = lista;
    }
  }
}