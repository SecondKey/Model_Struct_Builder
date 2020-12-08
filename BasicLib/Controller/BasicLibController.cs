using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Model_Struct_Builder;

namespace BasicLib
{
    public enum DiagramElementType
    {
        Node,
        Link,
    }

    class BasicLibController 
    {
        //#region Data
        //string name;
        //int pageNum;

        //List<string[]> linkList = new List<string[]>();
        //#endregion
        //Dictionary<string, Control> allPanel = new Dictionary<string, Control>();
        //public Dictionary<string, Control> AllPanel
        //{
        //    get { return allPanel; }
        //}

        //Dictionary<string, iFrameElement> allElement = new Dictionary<string, iFrameElement>();
        //public Dictionary<string, iFrameElement> AllElement
        //{
        //    get
        //    {
        //        return allElement;
        //    }
        //    set
        //    {
        //        allElement = value;
        //    }
        //}

        //List<ItemsControlDragHelper> allDragHelper = new List<ItemsControlDragHelper>();

        //public BasicLibController(string parameter)
        //{
        //    name = parameter;
        //    pageNum = int.Parse(FrameController.GetInstence().MainFrameData.GetContent("ControlInfo", name, "PageNum"));
        //    foreach (string link in FrameController.GetInstence().MainFrameData.GetContent("ControlInfo", name, "Link").Split(','))
        //    {
        //        linkList.Add(link.Split('+'));
        //    }
        //}


        //public void RegistPackagePanel(string elementName, Control viewElement)
        //{
        //    allPanel.Add(elementName, viewElement);
        //    foreach (var l in linkList)
        //    {
        //        if (l.Contains(elementName) && allPanel.ContainsKey(l[0]) && allPanel.ContainsKey(l[1]))
        //        {
        //            ItemsControl c = allPanel[l[1]].FindName("DragElement") as ItemsControl;
        //            UIElement u = allPanel[l[0]].FindName("DropElement") as UIElement;
        //            allDragHelper.Add(new ItemsControlDragHelper(c, u));
        //        }
        //    }
        //}

        //public Control GetPanel(string panelName)
        //{
        //    if (!allPanel.ContainsKey(panelName))
        //    {

        //    }
        //    return allPanel[panelName];
        //}
    }
}
