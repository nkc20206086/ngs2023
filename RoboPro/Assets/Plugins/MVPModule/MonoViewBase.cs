using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    /// <summary>View��Base�N���X</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoViewBase<T> : MonoBehaviour
    {
        public event Func<T> CurrentValueGetEventHandler;

        protected T currentValue => CurrentValueGetEventHandler();

        protected Subject<T> subject = new Subject<T>();

        public IObservable<T> Observable => subject;


        /// <summary>�l�ύX���Ăяo�����\�b�h</summary>
        /// <param name="value">�^�w�肵��View���瑗���Ă���l</param>
        public virtual void OnModelValueChanged(T value) { }
    }
}