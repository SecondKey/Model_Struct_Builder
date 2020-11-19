using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Model_Struct_Builder
{
    public class RWXml : RXml, iWData
    {
        public RWXml(string path, string name) : base(path, name)
        {
        }

        public void AddContent(params string[] parameters)
        {

        }

        public void AddProperty(params string[] parameters)
        {

        }

        public void SetContent(params string[] paramters)
        {

        }

        public void SetProperty(params string[] parameters)
        {

        }

        #region Temporary
        public static void TemporaryAddPropertySetContent(string property, string value, params string[] parameters)
        {
            XDocument targetXml = XDocument.Load(parameters[0]);
            XElement e = targetXml.Root;
            for (int i = 1; i < parameters.Length; i++)
            {
                XElement tmp = e.Element(parameters[i]);
                if (tmp == null)
                {
                    tmp = new XElement(parameters[i]);
                    e.Add(tmp);
                }
                e = tmp;
            }
            e.SetElementValue(property, value);
            targetXml.Save(parameters[0]);
        }

        public static string TemporaryReadContent(string property, params string[] parameters)
        {
            XDocument targetXml = XDocument.Load(parameters[0]);
            XElement e = targetXml.Root;
            for (int i = 1; i < parameters.Length; i++)
            {
                e = e.Element(parameters[i]);
            }
            return e.Element(property).Value;
        }

        public static void CreateXml(string path)
        {
            if (!FileFolder.HasFile(path))
            {
                XDocument doc = new XDocument();
                doc.Add(new XElement("UserVisible"));
                doc.Save(path);
            }
        }
        #endregion
    }
}
