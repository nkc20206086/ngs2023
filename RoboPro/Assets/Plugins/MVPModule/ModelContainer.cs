using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    /// <summary>MVP��Model�ƃ��W�b�N���q����R���e�i�N���X</summary>
    /// <typeparam name="T"></typeparam>
    public static class ModelContainer<T>
    {
        static readonly int listCapacity = 64;

        static Dictionary<string, List<MonoModelBase<T>>> modelDic = new Dictionary<string, List<MonoModelBase<T>>>();
        static Dictionary<string, List<IDisposable>> disposableDic = new Dictionary<string, List<IDisposable>>();


        /// <summary>���f���o�^���\�b�h</summary>
        /// <param name="modelInstance">���f���̃C���X�^���X</param>
        public static void Register(MonoModelBase<T> modelInstance)
        {
            // ���f���̖��O���Ȃ��ꍇ
            if (!modelDic.ContainsKey(modelInstance.ContentsName))
            {
                // ���f����Dictionary��new����
                disposableDic[modelInstance.ContentsName] = new List<IDisposable>();
                modelDic[modelInstance.ContentsName] = new List<MonoModelBase<T>>(listCapacity);
            }


            foreach (MonoModelBase<T> model in modelDic[modelInstance.ContentsName])
            {
                var disposable1 = model.ReadOnlyProperty.Subscribe(modelInstance.PropertyValueChange);
                disposableDic[modelInstance.ContentsName].Add(disposable1);

                var disposable2 = modelInstance.ReadOnlyProperty.Subscribe(model.PropertyValueChange);
                disposableDic[modelInstance.ContentsName].Add(disposable2);
            }


            modelDic[modelInstance.ContentsName].Add(modelInstance);
        }


        /// <summary>���f�����폜���\�b�h</summary>
        /// <param name="name">�폜���������f����</param>
        public static void Clear(string name)
        {
            foreach (IDisposable disposable in disposableDic[name])
            {
                disposable.Dispose();
            }
            modelDic[name].Clear();
            disposableDic[name].Clear();
        }


        /// <summary>���f���̒l�擾���\�b�h</summary>
        /// <param name="contentsName">���f����</param>
        /// <returns></returns>
        public static IReadOnlyReactiveProperty<T> GetModelValueChangeObservable(string contentsName)
        {
            if (!modelDic.ContainsKey(contentsName)) { return null; }

            return modelDic[contentsName][0].ReadOnlyProperty;
        }


        /// <summary>���f���̒l�ݒ胁�\�b�h</summary>
        /// <param name="contentsName">���f����</param>
        /// <param name="value">�����Ă���l</param>
        public static void SetModelPropertyValue(string contentsName, T value)
        {
            if (!modelDic.ContainsKey(contentsName)) { return; }

            modelDic[contentsName][0].PropertyValueChange(value);
        }
    }
}
