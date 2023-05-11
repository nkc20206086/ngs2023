using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MVPModule
{
    /// <summary>PresenterのBaseクラス</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoPresenterBase<T> : MonoBehaviour
    {
        [SerializeField,Tooltip("Viewクラス")]
        protected MonoViewBase<T> view = null;

        [SerializeField, Tooltip("Modelクラス")]
        protected MonoModelBase<T> model = null;

        void Awake()
        {
            ObservableConnect();
            EventHandlerConnect();
        }

        /// <summary>View - Modelコネクトメソッド</summary>
        protected virtual void ObservableConnect()
        {
            model.ReadOnlyProperty.Subscribe(view.OnModelValueChanged).AddTo(this);
            view.Observable.Subscribe(model.PropertyValueChange).AddTo(this);
        }

        /// <summary>イベントを登録するメソッド</summary>
        private void EventHandlerConnect()
        {
            view.CurrentValueGetEventHandler += model.GetCurrentValue;
        }

        /// <summary>イベントを削除するメソッド</summary>
        private void OnDestroy()
        {
            view.CurrentValueGetEventHandler -= model.GetCurrentValue;
        }
    }
}
