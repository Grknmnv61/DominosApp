using Dominos.Common.Enums;
using Dominos.Web.UI.Business.Helper.Order.Provider;
using Dominos.Web.UI.Business.Helper.Product.Provider;
using Dominos.Web.UI.Models;

namespace Dominos.Web.UI.Business.Helper
{
    public class Instance
    {
        public Instance(SubmitEnum submit)
        {
            switch (submit)
            {
                case SubmitEnum.List:
                    Provider = new GetProductListProvider();
                    break;
                case SubmitEnum.Update:
                    Provider = new UpdateProductProvider();
                    break;

                case SubmitEnum.Insert:
                    Provider = new InsertOrderListProvider();
                    break;
                case SubmitEnum.Remove:
                    Provider = new RemoveOrderListProvider();
                    break;
                case SubmitEnum.Create:
                    Provider = new CreateOrderProvider();
                    break;
                
            }
        }
        public IProvider<ViewModel> Provider { get; set; }
    }
}