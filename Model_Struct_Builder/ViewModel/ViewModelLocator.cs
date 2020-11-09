using CommonServiceLocator;
using GalaSoft.MvvmLight.Ioc;

namespace Model_Struct_Builder.ViewModel
{
    /// <summary>
    /// MVVMLight IOC注册机
    /// 可以获取VM单例
    /// </summary>
    public class ViewModelLocator
    {
        public static ViewModelLocator instence;

        public ViewModelLocator()
        {
            instence = this;

            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);
            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }
    }
}