using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Tender.Sourcing.Repositories.Interfaces
{
  public interface ITenderRepository
  {
    Task<IEnumerable<Entities.Tender>> GetAll();
    Task<Entities.Tender> GetById(string id);
    Task<Entities.Tender> GetByName(string name);
    Task Create(Entities.Tender tender);
    Task<bool> Update(Entities.Tender tender);
    Task<bool> Delete(string id);
  }
}
