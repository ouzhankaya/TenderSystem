using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using MongoDB.Driver;
using Tender.Sourcing.Data.Interfacess;
using Tender.Sourcing.Repositories.Interfaces;

namespace Tender.Sourcing.Repositories
{
  public class TenderRepository: ITenderRepository
  {
    private readonly ITenderContext _context;
    public TenderRepository(ITenderContext context)
    {
      _context = context;
    }

    public async Task Create(Entities.Tender tender)
    {
       await _context.Tenders.InsertOneAsync(tender);
    }

    public async Task<IEnumerable<Entities.Tender>> GetAll()
    {
      return await _context.Tenders.Find(x => true).ToListAsync();
    }

    public async Task<Entities.Tender> GetById(string id)
    {
      return await _context.Tenders.Find(x => x.Id == id).FirstOrDefaultAsync();
    }

    public async Task<Entities.Tender> GetByName(string name)
    {
      return await _context.Tenders.Find(x => x.Name == name).FirstOrDefaultAsync();
    }

    public async Task<bool> Update(Entities.Tender tender)
    {
      FilterDefinition<Entities.Tender> filter = Builders<Entities.Tender>.Filter.Eq(x=>x.Id, tender.Id);
      var updatedResult = await _context.Tenders.ReplaceOneAsync(filter, tender);
      return updatedResult.IsAcknowledged && updatedResult.ModifiedCount > 0;
    }

    public async Task<bool> Delete(string id)
    {
      FilterDefinition<Entities.Tender> filter = Builders<Entities.Tender>.Filter.Eq(x => x.Id, id);
      DeleteResult deletedResult = await _context.Tenders.DeleteOneAsync(filter);
      return deletedResult.IsAcknowledged && deletedResult.DeletedCount > 0;
    }
  }
}
