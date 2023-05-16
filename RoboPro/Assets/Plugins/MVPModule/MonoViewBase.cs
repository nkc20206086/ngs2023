using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    /// <summary>ViewのBaseクラス</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoViewBase<T> : MonoBehaviour
    {
        public event Func<T> CurrentValueGetEventHandler;

        protected T currentValue => CurrentValueGetEventHandler();

        protected Subject<T> subject = new Subject<T>();

        public IObservable<T> Observable => subject;


        /// <summary>値変更時呼び出しメソッド</summary>
        /// <param name="value">型指定したViewから送られてくる値</param>
        public virtual void OnModelValueChanged(T value) { }
    }
}