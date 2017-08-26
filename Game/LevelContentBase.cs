using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;

namespace MGame
{
    public abstract class LevelContentBase : INotifyPropertyChanged
    {
        SynchronizationContext context = SynchronizationContext.Current;
        protected SynchronizationContext Context
        {
            get
            {
                if (context == null)
                {
                    context = SynchronizationContext.Current;
                }
                return context;
            }
            set
            {
                context = value;
            }
        }

        protected LevelContentBase()
        {
        }
        event PropertyChangedEventHandler propertyChanged;

        /// <summary>
        /// Occurs when a property value changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged
        {
            add
            {
                propertyChanged += value;
            }
            remove
            {
                propertyChanged -= value;
            }
        }

        /// <summary>
        /// Raises the <see cref="E:PropertyChanged"/> event.
        /// </summary>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> 
        /// instance containing the event data.</param>
        void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (propertyChanged != null)
            {
                propertyChanged(this, e);
            }
        }

        /// <summary>
        /// 触发属性变更事件
        /// </summary>
        /// <param name="property">被更改的属性的名称</param>
        protected void OnPropertyChanged(string property)
        {
            //因为不是每个线程都具有SynchronizationContext对象，
            //因此在使用前要进行安全性检查
            if (context == null)
            {
                OnPropertyChanged(new PropertyChangedEventArgs(property));
            }
            else
            {   //如果从UI线程中获取到了SynchronizationContext对象
                //使用Send方法向UI线程发送执行OnPropertyChanged代码的委托
                context.Send(delegate
                {
                    OnPropertyChanged(new PropertyChangedEventArgs(property));
                }, null);
            }
        }
    }
}