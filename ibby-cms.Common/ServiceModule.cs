using Ninject.Modules;
using ibby_cms.Common.Interfaces;
using ibby_cms.Common.Repositories;

namespace ibby_cms.Common
{
    public class ServiceModule : NinjectModule
    {
        private string connectionString;

        public ServiceModule(string connection)
        {
            connectionString = connection;
        }

        public override void Load()
        {
            Bind<IUnitOfWork>().To<EFUnitOfWork>().WithConstructorArgument(connectionString);
        }
    }
}