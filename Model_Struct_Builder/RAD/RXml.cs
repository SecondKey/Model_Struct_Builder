using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 读Xml类，提供读取xml内容功能
    /// </summary>
    public class RXml : iRData
    {
        public RXml(string path, string name)
        {
            fileFullName = path + "/" + name;
            this.filePath = path;
            this.fileName = name;
            targetXml = XDocument.Load(path + "/" + name);
        }

        #region Parameters
        /// <summary>
        /// 指定要加载的xml
        /// </summary>
        XDocument targetXml;
        /// <summary>
        /// 当前xml的完整路径
        /// </summary>
        string fileFullName;
        /// <summary>
        /// 当前xml的路径
        /// </summary>
        string filePath;
        /// <summary>
        /// xml的名字，后有.xml
        /// </summary>
        string fileName;

        #endregion

        #region iRData Members
        public string GetProperty(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length - 1; i++)
            {
                e = e.Element(parameters[i]);
            }
            return ToolsCenter.FormattingString(e.Attribute(parameters[parameters.Length - 1]).Value);
        }

        public string GetContent(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            return ToolsCenter.FormattingString(e.Value.ToString());
        }

        public Dictionary<string, string> GetOneElementsAllProperty(params string[] parameters)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            foreach (var property in e.Attributes())
            {
                tmp.Add(ToolsCenter.FormattingString(property.Name.ToString()), ToolsCenter.FormattingString(property.Value));
            }
            return tmp;
        }

        public Dictionary<string, string> GetAllElementContent(params string[] parameters)
        {
            Dictionary<string, string> tmp = new Dictionary<string, string>();
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            foreach (var property in e.Elements())
            {
                if (property.Name.ToString() == "Content")
                {
                    continue;
                }
                tmp.Add(ToolsCenter.FormattingString(property.Name.ToString()), ToolsCenter.FormattingString(property.Value));
            }
            return tmp;
        }

        public List<string> GetOneElementsAllContent(params string[] parameters)
        {
            List<string> tmp = new List<string>();
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            foreach (var property in e.Elements())
            {
                tmp.Add(ToolsCenter.FormattingString(property.Name.ToString()));
            }
            return tmp;
        }

        public Dictionary<string, Dictionary<string, string>> GetDoubleLayerElements(params string[] parameters)
        {
            Dictionary<string, Dictionary<string, string>> tmp = new Dictionary<string, Dictionary<string, string>>();
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            foreach (var property in e.Elements())
            {
                Dictionary<string, string> tmp1 = new Dictionary<string, string>();
                foreach (var p1 in property.Elements())
                {
                    tmp1.Add(ToolsCenter.FormattingString(p1.Name.ToString()), ToolsCenter.FormattingString(p1.Value));
                }
                tmp.Add(ToolsCenter.FormattingString(property.Name.ToString()), tmp1);
            }
            return tmp;
        }

        public int GetPropertyNum(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            return e.Attributes().Count();
        }

        public int GetContentNum(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            return e.Elements().Count();
        }

        public bool HasElement(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length - 1; i++)
            {
                e = e.Element(parameters[i]);

                bool hasElement = false;
                foreach (var element in e.Elements())
                {
                    if (element.Name == parameters[i + 1])
                    {
                        hasElement = true;
                        break;
                    }
                }
                if (!hasElement)
                {
                    return false;
                }
            }
            return true;
        }

        public bool HasProperty(params string[] parameters)
        {
            XElement e = targetXml.Root;
            for (int i = 0; i < parameters.Length - 1; i++)
            {
                e = e.Element(parameters[i]);
            }
            foreach (var kv in e.Attributes())
            {
                if (kv.Name == parameters[parameters.Length - 1])
                {
                    return true;
                }
            }
            return false;
        }

        #endregion


        #region Tools

        #endregion
    }
}
