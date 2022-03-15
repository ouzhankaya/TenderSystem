using System;
namespace Tender.Sourcing.Settings
{
  public interface ITenderDatabaseSettings
  {
    public string ConnectionStrings { get; set; }
    public string DatabaseName { get; set; }
  }
}
