using HovyFridge.Data.Entity;

namespace HovyFridge.TestApi.DAO.Data;

public interface IFridgeDao
{
    public Fridge GetFridgeById(long fridgeId);
    public List<Fridge> GetFridgesList();
    public Fridge DeleteFridgeById(long fridgeId);
    public Fridge UpdateFridge(Fridge fridge);
    public Product PutFridge(Fridge fridge);
}