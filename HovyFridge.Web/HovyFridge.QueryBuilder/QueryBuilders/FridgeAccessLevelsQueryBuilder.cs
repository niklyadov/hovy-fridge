using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders
{
    public class FridgeAccessLevelsQueryBuilder : QueryBuilder<FridgeAccessLevel, ApplicationContext>
    {
        public FridgeAccessLevelsQueryBuilder(ApplicationContext context) : base(context)
        {
        }

        public FridgeAccessLevelsQueryBuilder WithUserId(long userId)
        {
            Query = Query.Where(fal => fal.UserId == userId);

            return this;
        }

        public FridgeAccessLevelsQueryBuilder WithFridgeId(long fridgeId)
        {
            Query = Query.Where(fal => fal.FridgeId == fridgeId);

            return this;
        }
    }
}
