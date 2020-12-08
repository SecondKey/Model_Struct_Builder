using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Model_Struct_Builder
{
    #region FrameStruct
    /// <summary>
    /// 面板的类型（页面，窗口）
    /// </summary>
    public enum PanelType
    {
        /// <summary>
        /// 页面
        /// </summary>
        Page,
        /// <summary>
        /// 窗口
        /// </summary>
        Window,
    }

    /// <summary>
    /// 面板的信息
    /// </summary>
    public struct PanelInfo
    {
        public string name;//面板名
        public PanelType panelType;
        public string package;//面板内容所在的包（包名）
        public string type;//面板的类型（类名）
        public List<string> link;//当前面板链接到的其他面板（其他面板名）
        public List<string> feature;//当前面板的所有特性
    }

    public struct DragHelperStruct
    {
        public string source;
        public string target;
        public string package;
        public string helper;
    }
    #endregion

    /// <summary>
    /// 框架的控制器负责：
    /// 加载框架
    /// 框架文档
    /// 框架所需的包
    /// 页面结构
    /// </summary>
    public class FrameController : ObservableObject
    {
        #region Parameters
        /// <summary>
        /// 模板的核心数据
        /// </summary>
        RXml mainFrameData;
        public RXml MainFrameData
        {
            get { return mainFrameData; }
        }
        /// <summary>
        /// 当前选择的模板
        /// </summary>
        public string frameName;
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
            //加载框架信息
            mainFrameData = new RXml(AppController.GetInstence().appPath + "Frame/" + frameName, "FrameData.xml");
            foreach (var path in mainFrameData.GetDoubleLayerElements("Load", "Xml"))
            {
                foreach (var file in path.Value)
                {
                    allFrameData.Add(file.Value, new RXml(AppController.GetInstence().appPath + "Frame/" + frameName + "/" + path.Key, file.Key + ".xml"));
                }
            }
            //加载框架所需的包
            foreach (var package in mainFrameData.GetAllElementContent("Load", "Package"))
            {
                FramePackage p = new FramePackage(package.Value);
                allPackage.Add(package.Key, p);
            }
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.FrameLoadComplete));
        }
        #endregion

        #region Data
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
        /// <summary>
        /// 框架的文本
        /// </summary>
        public Dictionary<string, string> FrameDataText
        {
            get { return frameDataText; }
            set
            {
                frameDataText = value;
                RaisePropertyChanged(() => FrameDataText);

            }
        }
        private Dictionary<string, string> frameDataText;
        void GetTempleteText<T>(MsgBase msg)
        {
            FrameDataText = allFrameData["FrameText_" + FrameLanguage].GetAllElementContent();
        }
        #endregion

        #region Panel

        /// <summary>
        /// 框架中全部的信息
        /// </summary>
        public Dictionary<string, PanelInfo> AllPanelInfo { get { return allPanelInfo; } }
        private Dictionary<string, PanelInfo> allPanelInfo = new Dictionary<string, PanelInfo>();
        /// <summary>
        ///  已经加载的全部面板
        /// </summary>
        public Dictionary<string, Control> AllPanel { get { return allPanel; } }
        Dictionary<string, Control> allPanel = new Dictionary<string, Control>();

        /// <summary>
        /// 加载所有的页面结构
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="msg"></param>
        void StartLoadPanelStruct<T>(MsgBase msg)
        {
            //读取所有的页面属性
            foreach (string panel in mainFrameData.GetOneElementAllContent("Panel"))
            {
                PanelInfo tmpPanel = new PanelInfo()
                {
                    name = panel,
                    panelType = (PanelType)Enum.Parse(typeof(PanelType), mainFrameData.GetContent("Panel", panel, "PanelType")),
                    package = mainFrameData.GetContent("Panel", panel, "Package"),
                    type = mainFrameData.GetContent("Panel", panel, "Type"),
                    link = mainFrameData.GetOneElementAllContent("Panel", panel, "Link"),
                    feature = mainFrameData.GetOneElementAllContent("Panel", panel, "Feature"),
                };
                allPanelInfo.Add(panel, tmpPanel);
                CreatePanel(tmpPanel);
            }
            MsgCenter.SendMsg(new MsgBase(AllAppMsg.PanelCreateComplete));//框架加载结束
        }

        /// <summary>
        /// 创建一个页面
        /// </summary>
        /// <param name="name"></param>
        Control CreatePanel(PanelInfo info)
        {
            Control panel = allPackage[info.package].GetElement(info.type);
            panel.DataContext = allPackage[info.package].GetElement(info.type + "ViewModel", panel, info.name);
            allPanel.Add(info.name, panel);
            return panel;
        }
        #endregion

        #region Document

        #endregion
        #endregion 

        #region DebugTool
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
        ///// <summary>
        ///// 获取一个面板的面板信息，无论它是页面还是窗口
        ///// </summary>
        ///// <param name="name">面板的名称</param>
        ///// <returns></returns>
        //PanelInfo GetPanelInfo(string name)
        //{
        //    if (allPageInfo.ContainsKey(name))
        //    {
        //        return allPageInfo[name];
        //    }
        //    else
        //    {
        //        return AllWindowInfo[name];
        //    }
        //}

        ///// <summary>
        ///// 获取一个页面
        ///// 如果页面已经加载完成，则直接返回该页面
        ///// 如果该页面没有加载，则创建一个页面
        ///// </summary>
        ///// <param name="layoutPanel"></param>
        ///// <param name="package"></param>
        ///// <param name="name"></param>
        ///// <param name="property"></param>
        //public Control GetPanel(string name)
        //{
        //    if (allPanel.ContainsKey(name))
        //    {
        //        return allPanel[name];
        //    }
        //    else
        //    {
        //        return CreatePanel(name);
        //    }
        //}

        ///// <summary>
        ///// 获取一个页面的类型
        ///// </summary>
        ///// <param name="name"></param>
        ///// <returns></returns>
        //PanelType GetPanelType(string name)
        //{
        //    if (allPage.ContainsKey(name))
        //    {
        //        return PanelType.Page;
        //    }
        //    else
        //    {
        //        return PanelType.Window;
        //    }
        //}

        ///// <summary>
        ///// 所有拖拽工具的信息
        ///// </summary>
        //public Dictionary<string, DragHelperStruct> AllDragHelperStruct { get { return allDragHelperStruct; } }
        //Dictionary<string, DragHelperStruct> allDragHelperStruct = new Dictionary<string, DragHelperStruct>();
        ///// <summary>
        ///// 所有拖拽工具
        ///// </summary>
        //public Dictionary<string, iDragHelper> AllDragHelper { get { return allDragHelper; } }
        //Dictionary<string, iDragHelper> allDragHelper = new Dictionary<string, iDragHelper>();

        /// <summary>
        /// 开始加载页面结构
        /// </summary>


        ////读取所有的窗口属性
        //foreach (string window in mainFrameData.GetOneElementAllContent("Panel", "Window"))
        //{
        //    PanelInfo tmpWindow = new PanelInfo()
        //    {
        //        name = window,
        //        package = mainFrameData.GetContent("Panel", "Window", window, "Package"),
        //        type = mainFrameData.GetContent("Panel", "Window", window, "Type"),
        //        link = mainFrameData.GetOneElementAllContent("Panel", "Window", window, "Link"),
        //        feature = mainFrameData.GetAllElementContent("Panel", "Window", window, "Feature"),
        //    };
        //}



        //if (p.link != null)
        //{
        //    switch (GetPanelType(name))
        //    {
        //        case PanelType.Page:
        //            foreach (string t in p.link)
        //            {
        //                if (allPanel.ContainsKey(t))
        //                {
        //                    if (allDragHelper.ContainsKey(t + "To" + name))
        //                    {
        //                        allDragHelper[t + "To" + name] = allPackage[allDragHelperStruct[t + "To" + name].package].GetElement(allDragHelperStruct[t + "To" + name].helper, allPanel[t], panel) as iDragHelper;
        //                    }
        //                    else
        //                    {
        //                        allDragHelper.Add(t + "To" + name, allPackage[allDragHelperStruct[t + "To" + name].package].GetElement(allDragHelperStruct[t + "To" + name].helper, allPanel[t], panel) as iDragHelper);
        //                    }
        //                }
        //            }
        //            break;
        //        case PanelType.Window:
        //            foreach (string t in p.link)
        //            {
        //                if (allPanel.ContainsKey(t))
        //                {
        //                    if (allDragHelper.ContainsKey(name + "To" + t))
        //                    {
        //                        allDragHelper[name + "To" + t] = allPackage[allDragHelperStruct[name + "To" + t].package].GetElement(allDragHelperStruct[name + "To" + t].helper, panel, allPanel[t]) as iDragHelper;
        //                    }
        //                    else
        //                    {
        //                        allDragHelper.Add(name + "To" + t, allPackage[allDragHelperStruct[name + "To" + t].package].GetElement(allDragHelperStruct[name + "To" + t].helper, panel, allPanel[t]) as iDragHelper);
        //                    }
        //                }
        //            }
        //            break;
        //    }
        //}

        //    //读取所有的拖拽属性
        //    foreach (string helper in MainFrameData.GetOneElementAllContent("Panel", "DragDropHelper"))
        //    {
        //        DragHelperStruct tmpHelper = new DragHelperStruct()
        //        {
        //            source = mainFrameData.GetContent("Panel", "DragDropHelper", helper, "Source"),
        //            target = mainFrameData.GetContent("Panel", "DragDropHelper", helper, "Target"),
        //            package = mainFrameData.GetContent("Panel", "DragDropHelper", helper, "Package"),
        //            helper = mainFrameData.GetContent("Panel", "DragDropHelper", helper, "Helper"),
        //        };
        //allDragHelperStruct.Add(helper, tmpHelper);
        //    }

        //public void PackageElementRegistSelf(string name, Control element)
        //{
        //    if (allPage.ContainsKey(name))
        //    {
        //        allPackage[mainFrameData.GetContent("Panel", "Page", name, "Package")].Controller.GetPanel(name);
        //    }
        //    else
        //    {
        //        allPackage[mainFrameData.GetContent("Panel", "Window", name, "Package")].Controller.GetPanel(name);
        //    }
        //}

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
