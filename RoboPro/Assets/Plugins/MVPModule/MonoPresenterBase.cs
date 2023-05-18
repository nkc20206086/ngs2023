using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;

namespace MVPModule
{
    /// <summary>Presenter��Base�N���X</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoPresenterBase<T> : MonoBehaviour
    {
        [SerializeField,Tooltip("View�N���X")]
        protected MonoViewBase<T> view = null;

        [SerializeField, Tooltip("Model�N���X")]
        protected MonoModelBase<T> model = null;

        void Awake()
        {
            ObservableConnect();
            EventHandlerConnect();
        }

        /// <summary>View - Model�R�l�N�g���\�b�h</summary>
        protected virtual void ObservableConnect()
        {
            model.ReadOnlyProperty.Subscribe(view.OnModelValueChanged).AddTo(this);
            view.Observable.Subscribe(model.PropertyValueChange).AddTo(this);
        }

        /// <summary>�C�x���g��o�^���郁�\�b�h</summary>
        private void EventHandlerConnect()
        {
            view.CurrentValueGetEventHandler += model.GetCurrentValue;
        }

        /// <summary>�C�x���g���폜���郁�\�b�h</summary>
        private void OnDestroy()
        {
            view.CurrentValueGetEventHandler -= model.GetCurrentValue;
        }
    }
}
