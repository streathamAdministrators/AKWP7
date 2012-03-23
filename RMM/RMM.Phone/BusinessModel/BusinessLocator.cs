/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocatorTemplate xmlns:vm="clr-namespace:RMM.Phone.ViewModel"
                                   x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"
*/

using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using Microsoft.Practices.ServiceLocation;
using RMM.Business.AccountService;
using RMM.Business.CategoryService;
using RMM.Business.ObtionService;
using RMM.Business.TransactionService;
using RMM.Phone.automapper;
using RMM.Business.automapper;

namespace RMM.Phone.BusinessModel
{

    public class BusinessBootStrapper
    {
        static BusinessBootStrapper()
        {
            if(ServiceLocator.Current == null)
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            #region Register Services
            SimpleIoc.Default.Register<IAccountService, AccountService>();
            SimpleIoc.Default.Register<ICategoryService, CategoryService>();
            SimpleIoc.Default.Register<IObtionService, ObtionService>();
            SimpleIoc.Default.Register<ITransactionService, TransactionService>();
            #endregion

            //Register mappings
           // AutoMapperRegistry.Configure();
        }

        /// <summary>
        /// Cleans up all the resources.
        /// </summary>
        public static void Cleanup()
        {
            //que c'est pas tres beau
        }
    }
}