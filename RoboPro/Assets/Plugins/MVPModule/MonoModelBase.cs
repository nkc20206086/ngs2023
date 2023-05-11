using UnityEngine;
using UniRx;

namespace MVPModule
{
    /// <summary>Model��Base�N���X</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoModelBase<T> : MonoBehaviour
    {
        [SerializeField, Tooltip("Model�̖��O")] 
        private string contentsName = null;

        protected ReactiveProperty<T> property = new ReactiveProperty<T>(default);

        public IReadOnlyReactiveProperty<T> ReadOnlyProperty => property;

        public string ContentsName => contentsName;


        /// <summary>���݂̒l��Ԃ����\�b�h</summary>
        /// <returns>�w�肵���^�̒l</returns>
        public virtual T GetCurrentValue()
        {
            return ReadOnlyProperty.Value;
        }

        /// <summary>�l�ύX���Ăяo�����\�b�h</summary>
        /// <param name="value">�^�w�肵��Model���瑗���Ă���l</param>
        public virtual void PropertyValueChange(T value) { }


        private void Awake()
        {
            ModelContainer<T>.Register(this);
        }

        /// <summary>���̃I�u�W�F�N�g���f�X�g���C�������ɌĂ΂�郁�\�b�h</summary>
        private void OnDestroy()
        {
            ModelContainer<T>.Clear(contentsName);
        }
    }
}
