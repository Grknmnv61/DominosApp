using Dominos.Common.Classes;
using Dominos.Common.Helpers;
using System.Collections.Generic;

namespace Dominos.Business.Product
{
    public interface IProductService
    {
        void DatabaseInitializeFactory(string database);
        ServiceResult<List<ProductTemplate>> GetAllProductByRedis();
        ServiceResult<bool> UpdateProduct(ProductTemplate productTemplate);
    }
}
