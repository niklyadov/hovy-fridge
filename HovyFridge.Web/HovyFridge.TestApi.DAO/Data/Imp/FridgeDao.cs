using HovyFridge.Data.Entity;
using Microsoft.EntityFrameworkCore;

namespace HovyFridge.TestApi.DAO.Data.Imp;

public class FridgeDao : IFridgeDao
{
    private readonly DbContext _dbContext;
    
    public FridgeDao(DbContext dbContext)
    {
        _dbContext = dbContext;
    }
    
    public Fridge GetFridgeById(long fridgeId)
    {
        throw new NotImplementedException();
    }

    public List<Fridge> GetFridgesList()
    {
        throw new NotImplementedException();
    }

    public Fridge DeleteFridgeById(long fridgeId)
    {
        throw new NotImplementedException();
    }

    public Fridge UpdateFridge(Fridge fridge)
    {
        throw new NotImplementedException();
    }

    public Product PutFridge(Fridge fridge)
    {
        throw new NotImplementedException();
    }
}