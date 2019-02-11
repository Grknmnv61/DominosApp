using Dominos.Common.Classes;
using Dominos.Common.Helpers;
using Dominos.DataLayer;
using Dominos.Repository.Repository;
using ServiceStack.Redis;
using ServiceStack.Redis.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominos.Business.Product
{
    public class ProductService : BaseService<DominosDBEntities>, IProductService
    {
        public ProductService(DBRepository repository) : base(repository)
        {

        }

        private List<ProductTemplate> GetAllProduct()
        {
            return (from p in Repository.Query<PRODUCT>()
                    join pt in Repository.Query<PRODUCT_TYPE>() on p.ProductTypeId equals pt.Id
                    select new ProductTemplate
                    {
                        Id = p.Id,
                        Name = p.Name,
                        Description = p.Description,
                        Price = p.Price.Value,
                        DiscountPrice = p.DiscountPrice.Value,
                        ProductTypeName = pt.Name
                    }).ToList();
        }

        public void DatabaseInitializeFactory(string database)
        {
            if (database.Equals("Redis"))
            {
                ExecuteRedis();
            }
        }

        public void ExecuteRedis()
        {
            var productList = GetAllProduct();

            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ProductTemplate> temp = client.As<ProductTemplate>();
                temp.StoreAll(productList);
            }
        }

        public ServiceResult<List<ProductTemplate>> GetAllProductByRedis()
        {
            var serviceResult = new ServiceResult<List<ProductTemplate>>();

            try
            {
                using (RedisClient client = new RedisClient("localhost", 6379))
                {
                    IRedisTypedClient<ProductTemplate> temp = client.As<ProductTemplate>();
                    serviceResult.Result = temp.GetAll().ToList();
                }
            }
            catch (Exception ex)
            {
                serviceResult.Exception = ex;
                serviceResult.HasError = true;
            }
            return serviceResult;
        }

        public ServiceResult<bool> UpdateProduct(ProductTemplate productTemplate)
        {
            var serviceResult = new ServiceResult<bool>();

            try
            {
                var product = Repository.Context.PRODUCT.Where(x => x.Id == productTemplate.Id).FirstOrDefault();
                var productTypeName = Repository.Context.PRODUCT_TYPE.Where(x => x.Id == product.ProductTypeId).Select(x => x.Name).FirstOrDefault();

                product.Name = productTemplate.Name;
                product.Description = productTemplate.Description;
                product.Price = productTemplate.Price;

                var state = Repository.Commit();

                if (state > decimal.Zero)
                {
                    UpdateProductRedis(product, productTypeName);
                    serviceResult.Result = true;
                }
            }
            catch (Exception ex)
            {
                serviceResult.Exception = ex;
                serviceResult.HasError = true;
            }
            return serviceResult;
        }

        private static void UpdateProductRedis(PRODUCT product, string productTypeName)
        {
            using (RedisClient client = new RedisClient("localhost", 6379))
            {
                IRedisTypedClient<ProductTemplate> temp = client.As<ProductTemplate>();

                var template = new ProductTemplate
                {
                    Id = product.Id,
                    Name = product.Name,
                    Description = product.Description,
                    Price = product.Price.Value,
                    DiscountPrice = product.DiscountPrice.Value,
                    ProductTypeName = productTypeName
                };

                var key = "urn:producttemplate:" + product.Id;
                temp.GetAndSetValue(key, template);
            }
        }
    }
}
