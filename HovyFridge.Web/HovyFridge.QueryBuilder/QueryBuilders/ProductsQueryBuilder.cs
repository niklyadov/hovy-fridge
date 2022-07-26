using HovyFridge.Entity;

namespace HovyFridge.QueryBuilder.QueryBuilders;

public class ProductsQueryBuilder : QueryBuilder<Product, ApplicationContext>
{
    public ProductsQueryBuilder(ApplicationContext context) : base(context)
    {
    }

    public ProductsQueryBuilder NotDeleted()
    {
        Query = Query.Where(p => !p.IsDeleted);

        return this;
    }

    public ProductsQueryBuilder Deleted()
    {
        Query = Query.Where(p => p.IsDeleted);

        return this;
    }

    public ProductsQueryBuilder WithBarcode(string? barcode)
    {
        Query = Query.Where(p => !string.IsNullOrEmpty(p.BarCode));

        if (string.IsNullOrEmpty(barcode))
            Query = Query.Where(p => p.BarCode.Equals(barcode));

        return this;
    }

    public ProductsQueryBuilder WithOwnerId(long ownerUserId)
    {
        Query = Query.Where(p => p.OwnerId == ownerUserId);

        return this;
    }

    public ProductsQueryBuilder WithOwner(User ownerUser)
    {
        if (ownerUser.Id == 0)
            throw new InvalidOperationException("Valid user owner id is not provided");

        return WithOwnerId(ownerUser.Id);
    }
}