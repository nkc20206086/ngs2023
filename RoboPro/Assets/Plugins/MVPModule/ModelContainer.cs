using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UniRx;
using System;

namespace MVPModule
{
    /// <summary>MVPのModelとロジックを繋げるコンテナクラス</summary>
    /// <typeparam name="T"></typeparam>
    public static class ModelContainer<T>
    {
        static readonly int listCapacity = 64;

        static Dictionary<string, List<MonoModelBase<T>>> modelDic = new Dictionary<string, List<MonoModelBase<T>>>();
        static Dictionary<string, List<IDisposable>> disposableDic = new Dictionary<string, List<IDisposable>>();


        /// <summary>モデル登録メソッド</summary>
        /// <param name="modelInstance">モデルのインスタンス</param>
        public static void Register(MonoModelBase<T> modelInstance)
        {
            // モデルの名前がない場合
            if (!modelDic.ContainsKey(modelInstance.ContentsName))
            {
                // モデル名Dictionaryをnewする
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


        /// <summary>モデル名削除メソッド</summary>
        /// <param name="name">削除したいモデル名</param>
        public static void Clear(string name)
        {
            foreach (IDisposable disposable in disposableDic[name])
            {
                disposable.Dispose();
            }
            modelDic[name].Clear();
            disposableDic[name].Clear();
        }


        /// <summary>モデルの値取得メソッド</summary>
        /// <param name="contentsName">モデル名</param>
        /// <returns></returns>
        public static IReadOnlyReactiveProperty<T> GetModelValueChangeObservable(string contentsName)
        {
            if (!modelDic.ContainsKey(contentsName)) { return null; }

            return modelDic[contentsName][0].ReadOnlyProperty;
        }


        /// <summary>モデルの値設定メソッド</summary>
        /// <param name="contentsName">モデル名</param>
        /// <param name="value">送られてくる値</param>
        public static void SetModelPropertyValue(string contentsName, T value)
        {
            if (!modelDic.ContainsKey(contentsName)) { return; }

            modelDic[contentsName][0].PropertyValueChange(value);
        }
    }
}
