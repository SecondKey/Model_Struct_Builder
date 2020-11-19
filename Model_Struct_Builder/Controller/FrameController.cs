using GalaSoft.MvvmLight;
using Model_Struct_Builder.RAD;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    public struct PanelInfo
    {
        public string name;
        public string package;
        public string type;
        public List<string> link;
    }


    public class FrameController : ObservableObject
    {
        #region Parameters
        /// <summary>
        /// 模板的核心数据
        /// </summary>
        RXml mainFrameData;
        /// <summary>
        /// 模板的全部数据
        /// </summary>
        Dictionary<string, iRData> allFrameData = new Dictionary<string, iRData>();
        /// <summary>
        /// 模板要加载的dll
        /// </summary>
        Dictionary<string, FramePackage> allPackage = new Dictionary<string, FramePackage>();
        /// <summary>
        /// 模板要加载的系统
        /// </summary>
        Dictionary<string, iFrameSystem> allSystem = new Dictionary<string, iFrameSystem>();

        /// <summary>
        /// 当前选择的模板
        /// </summary>
        public string frameName;

        #endregion 

        #region 单例
        private static FrameController instence;
        private FrameController()
        {
            MsgCenter.RegistSelf(this, new Dictionary<AllAppMsg, Action<MsgBase>[]>
            {
                { AllAppMsg.LoadFrame,new Action<MsgBase>[]{ LoadTemplate<MsgBase> } },
                { AllAppMsg.FrameLoadComplete,new Action<MsgBase>[]{ GetTempleteText<MsgBase>, StartLoadPanelStruct<MsgBase>} },
            });
        }
        public static FrameController GetInstence()
        {
            if (instence == null)
            {
                instence = new FrameController();
            }
            return instence;
        }

        /// <summary>
        /// 加载一个模板
        /// </summary>
        void LoadTemplate<T>(MsgBase msg)
        {
            MsgVar<string> tmpMsg = (MsgVar<string>)msg;
            frameName = tmpMsg.parameter;

            mainFrameData = new RXml(AppController.GetInstence().appPath + "Frame/" + frameName, "FrameData.xml");
            foreach (var path in mainFrameData.GetDoubleLayerElements("Load", "Xml"))
            {
                foreach (var file in path.Value)
                {
                    allFrameData.Add(file.Value, new RXml(AppController.GetInstence().appPath + "Frame/" + frameName + "/" + path.Key, file.Key + ".xml"));
                }
            }

            foreach (var package in mainFrameData.GetAllElementContent("Load", "Package"))
            {
                allPackage.Add(package.Value, new FramePackage(package.Key));
            }

            MsgCenter.SendMsg(new MsgBase(AllAppMsg.FrameLoadComplete));
        }
        #endregion

        #region Data
        #region ControlObject
        public object GetFrameObject(string package, string elementName)
        {
            return allPackage[package].GetElement(elementName);
        }

        public object GetFrameObject(string package, string elementName, string property)
        {
            return allPackage[package].GetElement(elementName, property);
        }
        #endregion

        #region FrameLanguage
        /// <summary>
        /// 框架的语言
        /// 和应用程序选择的语言相同
        /// 如果框架不包含应用程序的语言，使用框架默认语言
        /// </summary>
        public string FrameLanguage
        {
            get
            {
                if (mainFrameData.HasElement("FrameInfo", "Language", AppController.GetInstence().Language))
                { return AppController.GetInstence().Language; }
                else { return mainFrameData.GetContent("FrameInfo", "Language"); }
            }
        }
        #endregion

        #region DataText
        private Dictionary<string, string> frameDataText;
        public Dictionary<string, string> FrameDataText
        {
            get { return frameDataText; }
            set
            {
                frameDataText = value;
                RaisePropertyChanged(() => FrameDataText);

            }
        }
        void GetTempleteText<T>(MsgBase msg)
        {
            FrameDataText = allFrameData["FrameText_" + FrameLanguage].GetAllElementContent();
        }
        #endregion

        #region PanelStruct
        private Dictionary<string, PanelInfo> allPage = new Dictionary<string, PanelInfo>();
        public Dictionary<string, PanelInfo> AllPage { get { return allPage; } }

        private Dictionary<string, PanelInfo> allWindow = new Dictionary<string, PanelInfo>();
        public Dictionary<string, PanelInfo> AllWindow { get { return allWindow; } }

        /// <summary>
        /// 开始加载页面结构
        /// </summary>
        void StartLoadPanelStruct<T>(MsgBase msg)
        {
            foreach (string page in mainFrameData.GetOneElementsAllContent("Panel", "Page"))
            {
                PanelInfo tmpPage = new PanelInfo()
                {
                    name = page,
                    package = mainFrameData.GetContent("Panel", "Page", page, "Package"),
                    type = mainFrameData.GetContent("Panel", "Page", page, "Type"),
                };
                if (mainFrameData.HasElement("Panel", "Page", page, "Link"))
                {
                    tmpPage.link = new List<string>();
                    foreach (string l in mainFrameData.GetOneElementsAllContent("Panel", "Page", page, "Link"))
                    {
                        tmpPage.link.Add(l);
                    }
                }
                else
                {
                    tmpPage.link = null;
                }
                allPage.Add(page, tmpPage);
            }
            foreach (string window in mainFrameData.GetOneElementsAllContent("Panel", "Window"))
            {
                PanelInfo tmpWindow = new PanelInfo()
                {
                    name = window,
                    package = mainFrameData.GetContent("Panel", "Window", window, "Package"),
                    type = mainFrameData.GetContent("Panel", "Window", window, "Type"),
                };
                if (mainFrameData.HasElement("Panel", "Window", window, "Link"))
                {
                    tmpWindow.link = new List<string>();
                    foreach (string l in mainFrameData.GetOneElementsAllContent("Panel", "Window", window, "Link"))
                    {
                        tmpWindow.link.Add(l);
                    }
                }
                else
                {
                    tmpWindow.link = null;
                }
                allWindow.Add(window, tmpWindow);
            }
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.AllPanelStructLoadComplete));
        }

        #endregion
        #endregion

        #region DebugTools
        //void StartShowStruct<T>(MsgBase msg)
        //{
        //    //ShowStruct(PanelStruct, "");
        //}

        //void ShowStruct(List<FramePanelStruct> target, string t)
        //{
        //    foreach (FramePanelStruct panel in target)
        //    {
        //        Console.WriteLine(t + panel.name + "+" + panel.package + "+" + panel.type + "+" + panel.dockType);
        //        if (panel.content != null)
        //        {
        //            ShowStruct(panel.content, t + panel.name + "_");
        //        }
        //    }
        //}

        #endregion

        #region Old
        //public struct FramePanelStruct
        //{
        //    public string name;
        //    public string package;
        //    public string type;
        //    public string dockType;
        //    public List<FramePanelStruct> content;
        //}

        ///// <summary>
        ///// 递归加载页面结构
        ///// </summary>
        //void LoadPanelStruct(List<FramePanelStruct> targetList, params string[] path)
        //{
        //    foreach (string page in mainFrameData.GetOneElementsAllContent(path))
        //    {
        //        FramePanelStruct t = new FramePanelStruct()
        //        {
        //            name = page,
        //            package = mainFrameData.GetContent(ToolsCenter.ConnectArray(path, page, "Package")),
        //            type = mainFrameData.GetContent(ToolsCenter.ConnectArray(path, page, "Type")),
        //            dockType = mainFrameData.GetContent(ToolsCenter.ConnectArray(path, page, "DockType")),
        //        };
        //        if (mainFrameData.HasElement(ToolsCenter.ConnectArray(path, page, "Content")))
        //        {
        //            t.content = new List<FramePanelStruct>();
        //            LoadPanelStruct(t.content, ToolsCenter.ConnectArray(path, page, "Content"));
        //        }
        //        else
        //        {
        //            t.content = null;
        //        }
        //        targetList.Add(t);
        //    }
        //}
        #endregion
    }
}
