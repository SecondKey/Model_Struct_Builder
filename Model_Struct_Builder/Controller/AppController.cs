using GalaSoft.MvvmLight;
using Model_Struct_Builder.RAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    #region StaticValue

    #endregion

    public class AppController : ObservableObject
    {
        #region Parameters
        /// <summary>
        /// 程序本身的路径
        /// </summary>
        public string appPath = AppDomain.CurrentDomain.SetupInformation.ApplicationBase + "Data/";
        /// <summary>
        /// 程序中心数据，包括版本等
        /// </summary>
        public RXml mainAppData;
        /// <summary>
        /// 程序的全部数据
        /// </summary>
        public Dictionary<string, iRData> AllAppData = new Dictionary<string, iRData>();
        #endregion

        #region 单例/APP启动/控制器初始化
        private static AppController instence;
        private AppController()
        {
            MsgCenter.RegistSelf(this, new Dictionary<AllAppMsg, Action<MsgBase>[]>
            {
                { AllAppMsg.LoadApp,new Action<MsgBase>[]{ ControllerInitialize<MsgBase>, LoadAppData<MsgBase> } },
                { AllAppMsg.AppLoadComplete,new Action<MsgBase>[]{ GetLanguage<MsgBase> } },
                { AllAppMsg.ChangeLanguage,new Action<MsgBase>[]{ GetDataText<MsgBase> } },
            });
        }
        public static AppController GetInstence()
        {
            if (instence == null)
            {
                instence = new AppController();
                MsgCenter.SendMsg(new MsgBase(AllAppMsg.LoadApp));
            }
            return instence;
        }
        /// <summary>
        /// 初始化所有控制器
        /// </summary>
        void ControllerInitialize<T>(MsgBase msg)
        {
            FrameController.GetInstence();
        }

        /// <summary>
        /// 加载应用程序数据
        /// </summary>
        void LoadAppData<T>(MsgBase msg)
        {
            mainAppData = new RXml(appPath, "AppData.xml");
            foreach (var path in mainAppData.GetDoubleLayerElements("Load"))
            {
                foreach (var file in path.Value)
                {
                    AllAppData.Add(file.Value, new RXml(appPath + path.Key, file.Key + ".xml"));
                }
            }
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.AppLoadComplete));
        }
        #endregion

        #region Data

        #region Language
        private string language;
        public string Language
        {
            get { return language; }
            set
            {
                language = value;
                RaisePropertyChanged(() => Language);
                MsgCenter.SendMsg(new MsgBase(AllAppMsg.ChangeLanguage));
            }
        }
        void GetLanguage<T>(MsgBase msg)
        {
            Language = mainAppData.GetContent("Settings", "Language");
        }
        #endregion

        #region DataText
        private Dictionary<string, string> appDataText;
        public Dictionary<string, string> AppDataText
        {
            get { return appDataText; }
            set
            {
                appDataText = value;
                RaisePropertyChanged(() => AppDataText);
            }
        }
        void GetDataText<T>(MsgBase msg)
        {
            AppDataText = AllAppData["AppText_" + Language].GetAllElementContent();
        }
        #endregion

        #endregion
    }
}
