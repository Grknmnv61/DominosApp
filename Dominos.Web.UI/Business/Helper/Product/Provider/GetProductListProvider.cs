using Dominos.Web.UI.Models;

namespace Dominos.Web.UI.Business.Helper.Product.Provider
{
    public class GetProductListProvider :BaseProvider,IProvider<ViewModel>
    {
        public void Execute(ViewModel model)
        {
            GetProductList(model);
        }
    }
}