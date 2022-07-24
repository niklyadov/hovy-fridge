using HovyFridge.Data;
using HovyFridge.Data.Entity;
namespace HovyFridge.TestApi.DAO.Data;

public class FridgeDao : DaoBase
{
    private readonly ApplicationContext _db;

    public FridgeDao(ApplicationContext db)
    {
        _db = db;
    }

    public Fridge? GetFridgeById(long fridgeId)
    {
        return _db.Fridges.SingleOrDefault(f => f.Id == fridgeId);
    }

    public List<Fridge> GetFridgesList()
    {
        throw new NotImplementedException();
    }

    public Fridge DeleteFridgeById(long fridgeId)
    {
        var fridge = GetFridgeById(fridgeId);

        if (fridge == null)
            throw new InvalidOperationException($"Product with id {fridgeId} not found");

        var result = _db.Fridges.Remove(fridge);
        _db.SaveChanges();

        return result.Entity;
    }

    public Fridge UpdateFridge(Fridge fridge)
    {
        var result = _db.Fridges.Remove(fridge);
        _db.SaveChanges();

        return result.Entity;
    }

    public Fridge PutFridge(Fridge fridge)
    {
        var result = _db.Fridges.Add(fridge);
        _db.SaveChanges();

        return result.Entity;
    }
}