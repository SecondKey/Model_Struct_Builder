using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Model_Struct_Builder
{
    struct FormStruct
    {
        public string name;
        public FormItemType type;
        public Size size;
        public object parameters;
    }

    enum FormItemType
    {
        InputLine,
        InputText,
        DropDown,
        InputDropDown,
    }

    class DialogueWindowViewModel : AppViewModelBase
    {
        AllAppMsg callbackMsg;

        public DialogueWindowViewModel(List<FormStruct> formInfo, AllAppMsg callbackMsg)
        {
            formStructs = formInfo;
            this.callbackMsg = callbackMsg;
        }

        private List<FormStruct> formStructs;
        public List<FormStruct> FormStructs
        {
            get { return formStructs; }
        }

        private Dictionary<string, object> callBackValues = new Dictionary<string, object>();
        public Dictionary<string, object> CallBackValues
        {
            get { return callBackValues; }
            set { callBackValues = value; }
        }

        public RelayCommand CallbackCommand
        {
            get
            {
                return new RelayCommand(() =>
                {
                    if (callBackValues.Count == 1)
                    {
                        MsgCenter.SendMsg(new MsgVar<string>(callbackMsg, callBackValues["Value"] as string));//发送-加载框架--Test
                    }
                    else
                    {
                        MsgCenter.SendMsg(new MsgVar<Dictionary<string, object>>(callbackMsg, callBackValues));//发送-加载框架--Test
                    }
                    Console.WriteLine(CallBackValues["Value"]);
                });
            }
        }
    }
}
