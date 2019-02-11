using Autofac;
using Dominos.Business.Order;
using Dominos.Business.Product;
using Dominos.DataLayer;
using Dominos.Repository.Repository;

namespace Dominos.Business.Infrastructure
{
    public static class IoC
    {
        public static ContainerBuilder Builder;
        private static Autofac.IContainer Container;

        static IoC()
        {
            if (Builder == null)
            {
                Builder = new ContainerBuilder();
                Builder.RegisterType<DominosDBEntities>();
                Builder.RegisterType<DBRepository>().InstancePerDependency();
                Builder.RegisterType<ProductService>().As<IProductService>();
                Builder.RegisterType<OrderService>().As<IOrderService>();
            }
        }

        public static Autofac.IContainer CreateContainer()
        {
            Container = Builder.Build();
            return Container;
        }

        public static T Resolve<T>()
        {
            return Container.Resolve<T>();
        }
    }

}
