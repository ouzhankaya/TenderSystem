using System;
namespace Tender.Sourcing.Settings
{
  public class TenderDatabaseSettings: ITenderDatabaseSettings
  {
    public string ConnectionStrings { get; set; }
    public string DatabaseName { get; set; }
  }
}
