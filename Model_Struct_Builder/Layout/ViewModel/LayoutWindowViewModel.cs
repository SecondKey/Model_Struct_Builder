using Model_Struct_Builder.ViewModel;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 页面/窗口 的VM
    /// </summary>
    public class LayoutWindowViewModel : LayoutPanelViewModelBase
    {
        public LayoutWindowViewModel(PanelInfo targetStruct) : base(targetStruct)
        {
            this.RegistSelf(AllAppMsg.SaveUserVisible, SaveUserVisible<MsgBase>);
            this.RegistSelf(AllAppMsg.LoadUserVisible, LoadUserVisible<MsgBase>);

            ViewModelLocator.instence.Main.ActiveDocumentChanged += (sender, e) =>
            {
                ActiveDocumentChangedCallback((e as VarEventArgs<PanelInfo>).parameter.name);
            };
            ViewModelLocator.instence.Main.WindowActionList.CollectionChanged += ChangeVisible;
        }

        #region AutoVisible
        /// <summary>
        /// 用户设定当前页面为显示或隐藏
        /// </summary>
        bool userVisible = true;
        /// <summary>
        /// 表示上一次的显示或隐藏操作是否是自动进行的
        /// 该值会在自动进行显示隐藏操作时设为true并在该次对IsVisible属性操作时设为false
        /// 用于指示IsVisible的在set过程中不修改userVisible，因为该次操作不是用户发起的
        /// </summary>
        BoolState autoVisible = new BoolState(false);

        /// <summary>
        /// 当切换了选定页面，自动对窗口进行一次显示隐藏操作
        /// </summary>
        /// <param name="avticePageName"></param>
        void ActiveDocumentChangedCallback(string avticePageName)
        {
            using (autoVisible.SetScope())
            {
                if (PanelInfo.link.Count != 0)
                {
                    if (PanelInfo.link.Contains(avticePageName) && userVisible)
                    {
                        IsVisible = true;
                    }
                    else
                    {
                        IsVisible = false;
                    }
                }
            }
        }
        #endregion

        #region IsVisible

        public bool IsVisible
        {
            get
            {
                return ViewModelLocator.instence.Main.WindowActionList[Name];
            }
            set
            {
                if (ViewModelLocator.instence.Main.WindowActionList[Name] != value)
                {
                    if (AppController.GetInstence().LoadLayoutState.Value)
                    {
                        using (autoVisible.SetScope())
                        {
                            ViewModelLocator.instence.Main.WindowActionList[Name] = value;
                        }
                    }
                    else
                    {
                        ViewModelLocator.instence.Main.WindowActionList[Name] = value;

                    }
                }
            }
        }

        void ChangeVisible(object sender, NotifyCollectionChangedEventArgs e)
        {
            KeyValuePair<string, bool> kv = (KeyValuePair<string, bool>)e.NewItems[0];
            if (kv.Key == Name)
            {
                if (kv.Value == true && PanelInfo.link.Count != 0 && !PanelInfo.link.Contains(ViewModelLocator.instence.Main.ActiveDocument.Name))
                {
                    using (autoVisible.SetScope())
                    {
                        ViewModelLocator.instence.Main.WindowActionList[Name] = false;
                    }
                }
                else
                {
                    if (!autoVisible.Value) { userVisible = kv.Value; }//如果是用户操作，则修改userVisible
                    RaisePropertyChanged(() => IsVisible);
                }
            }
        }
        #endregion

        #region LoadSave
        public void SaveUserVisible<T>(MsgBase msg)
        {
            MsgVar<string> tmpMSg = msg as MsgVar<string>;
            RWXml.TemporaryAddPropertySetContent(
                PanelInfo.name,
                userVisible.ToString(),
                FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + tmpMSg.parameter + ".xml",
                "UserVisible"
                );
        }

        public void LoadUserVisible<T>(MsgBase msg)
        {
            MsgVar<string> tmpMSg = msg as MsgVar<string>;
            userVisible = bool.Parse(RWXml.TemporaryReadContent(
                PanelInfo.name,
                FileFolder.LinkPath(AppController.GetInstence().appPath, "Frame", FrameController.GetInstence().frameName, "Layout") + tmpMSg.parameter + ".xml",
                "UserVisible"
                ));
        }

        #endregion

        #region Old

        //MsgCenter.RegistSelf(this, AllAppMsg.ShowHideWindow, ChangeVisible<MsgBase>);

        //void ChangeVisible<T>(MsgBase msg)
        //{
        //    MsgVar<KeyValuePair<string, bool>> tmpMsg = (MsgVar<KeyValuePair<string, bool>>)msg;
        //    if (tmpMsg.parameter.Key == Name)
        //    {
        //        if (PanelInfo.link != null && !PanelInfo.link.Contains(ViewModelLocator.instence.Main.ActiveDocument.Name))
        //        {
        //            ViewModelLocator.instence.Main.WindowActionList[Name].P2Property = false;
        //        }
        //        else
        //        {
        //            IsVisible = tmpMsg.parameter.Value;
        //        }
        //    }
        //}


        //public bool IsVisible
        //{
        //    get
        //    {
        //        return ViewModelLocator.instence.Main.WindowActionList[Name].P2Property;
        //    }
        //    set
        //    {
        //        if (!autoVisible) { userVisible = value; }//如果是用户操作，则修改userVisible
        //        else { autoVisible = false; }//如果是自动操作，不修改userVisible

        //        if (ViewModelLocator.instence.Main.WindowActionList[Name].P2Property != value)
        //        {
        //            ViewModelLocator.instence.Main.WindowActionList[Name].P2Property = value;
        //        }
        //        RaisePropertyChanged(() => IsVisible);
        //    }
        //}
        #endregion
    }
}
