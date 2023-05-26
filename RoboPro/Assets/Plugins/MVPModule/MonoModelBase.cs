using UnityEngine;
using UniRx;

namespace MVPModule
{
    /// <summary>ModelのBaseクラス</summary>
    /// <typeparam name="T"></typeparam>
    public class MonoModelBase<T> : MonoBehaviour
    {
        [SerializeField, Tooltip("Modelの名前")] 
        private string contentsName = null;

        protected ReactiveProperty<T> property = new ReactiveProperty<T>(default);

        public IReadOnlyReactiveProperty<T> ReadOnlyProperty => property;

        public string ContentsName => contentsName;


        /// <summary>現在の値を返すメソッド</summary>
        /// <returns>指定した型の値</returns>
        public virtual T GetCurrentValue()
        {
            return ReadOnlyProperty.Value;
        }

        /// <summary>値変更時呼び出しメソッド</summary>
        /// <param name="value">型指定したModelから送られてくる値</param>
        public virtual void PropertyValueChange(T value) { }


        private void Awake()
        {
            ModelContainer<T>.Register(this);
        }

        /// <summary>このオブジェクトがデストロイした時に呼ばれるメソッド</summary>
        private void OnDestroy()
        {
            ModelContainer<T>.Clear(contentsName);
        }
    }
}
