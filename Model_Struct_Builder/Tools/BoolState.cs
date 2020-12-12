using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Model_Struct_Builder
{
    /// <summary>
    /// 用于指示一种bool状态
    /// 该状态会在某个功能或程序段的执行过程中全程为特殊值，并在其余状态全程默认值
    /// 使用 using(BoolScope.SetScope()){}来限定特殊值范围
    /// </summary>
    public class BoolState : IDisposable
    {
        /// <summary>
        /// 默认的状态
        /// </summary>
        private bool commonValue;
        /// <summary>
        /// 当前的状态
        /// </summary>
        public bool Value { get; private set; }

        /// <summary>
        /// 出入范围时引发事件的代理
        /// </summary>
        public delegate void ValueChangedEvent();

        /// <summary>
        /// 进入事件的代理
        /// </summary>
        ValueChangedEvent EnterScopeEvent = null;
        /// <summary>
        /// 离开事件的代理
        /// </summary>
        ValueChangedEvent OutScopeEvent = null;

        public BoolState(bool commonValue)
        {
            this.commonValue = commonValue;
        }

        public BoolState(bool commonValue, ValueChangedEvent enterScopeEvent, ValueChangedEvent outScopeEvent)
        {
            this.commonValue = commonValue;
            OutScopeEvent = outScopeEvent;
            EnterScopeEvent = enterScopeEvent;
        }


        /// <summary>
        /// 使用using声明一个范围，在该范围内的状态值为特殊值
        /// </summary>
        /// <returns></returns>
        public IDisposable SetScope()
        {
            if (EnterScopeEvent != null)
            {
                EnterScopeEvent();
            }
            Value = !commonValue;
            return this;
        }
        /// <summary>
        /// Dispose方法不应被人为调用
        /// </summary>
        public void Dispose()
        {
            if (OutScopeEvent != null)
            {
                OutScopeEvent();
            }
            Value = commonValue;
        }
    }
}
