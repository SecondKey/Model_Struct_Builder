using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 程序总控制器负责：
    /// 加载程序
    /// 文档信息
    /// 用户设置
    /// 程序状态
    /// </summary>
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
        public RWXml mainAppData;


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
                { AllAppMsg.AppLoadComplete,new Action<MsgBase>[]{ GetLanguage<MsgBase> } },
                { AllAppMsg.ChangeLanguage,new Action<MsgBase>[]{ GetDataText<MsgBase> } },
            });
        }
        public static AppController GetInstence()
        {
            if (instence == null)
            {
                instence = new AppController();
                instence.LoadAppData();
                instence.CreateInstence();
            }
            return instence;
        }

        /// <summary>
        /// 用于创建所有控制器的单例
        /// </summary>
        void CreateInstence()
        {
            FrameController.GetInstence();
        }

        /// <summary>
        /// 加载应用程序数据
        /// </summary>
        void LoadAppData()
        {
            mainAppData = new RWXml(appPath, "AppData.xml");//加载AppData
            foreach (var path in mainAppData.GetDoubleLayerElements("Load"))//遍历加载所有AppData中的Load信息
            {
                foreach (var file in path.Value)
                {
                    AllAppData.Add(file.Value, new RXml(appPath + path.Key, file.Key + ".xml"));
                }
            }
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.AppLoadComplete));//程序加载完成
        }
        #endregion

        #region Data

        #region DataText
        /// <summary>
        /// 应用程序的所有文本（不包含框架）
        /// </summary>
        public Dictionary<string, string> AppDataText
        {
            get { return appDataText; }
            set
            {
                appDataText = value;
                RaisePropertyChanged(() => AppDataText);
            }
        }
        private Dictionary<string, string> appDataText;
        void GetDataText<T>(MsgBase msg)
        {
            AppDataText = AllAppData["AppText_" + Language].GetAllElementContent();
        }
        #endregion

        #region Settgins

        #region Language
        /// <summary>
        /// 当前选择的语言
        /// </summary>
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
        private string language;
        void GetLanguage<T>(MsgBase msg)
        {
            Language = mainAppData.GetContent("Settings", "Language");
        }
        #endregion


        #endregion
        #endregion

        #region State
        /// <summary>
        /// 程序是否正在加载布局
        /// </summary>
        public BoolScope LoadLayoutState = new BoolScope(false);
        #endregion
    }
}
