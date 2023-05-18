using Cysharp.Threading.Tasks;
using System;
using Zenject;

namespace Robo
{
    public interface IMultiSceneLoader
    {
        /// <summary>シーンを追加したときに呼ばれるコールバック</summary>
        event Action<SceneID> OnAddScene;

        /// <summary>シーンを削除したときに呼ばれるコールバック</summary>
        event Action<SceneID> OnUnloadScene;

        /// <summary>シーンをアクティブにしたときに呼ばれるコールバック</summary>
        event Action<SetActiveSceneArgs> OnActiveScene;    
        
        /// <summary>シーン追加</summary>
        UniTask AddScene(SceneID id, bool autoChangeActiveScene);

        /// <summary>シーン追加(extraBindingsで、追加するシーンに今のシーンの情報をバインドできる。)</summary>
        UniTask AddScene(SceneID id, bool autoChangeActiveScene, Action<DiContainer> extraBindings);

        /// <summary>シーン削除</summary>
        UniTask UnloadScene(SceneID id);

        /// <summary>
        /// アクティブシーンを決定する。
        /// 生成されるオブジェクトはアクティブシーンに生成される。(親を指定しない場合)
        /// アクティブシーンはシーンに一つしか存在しない。
        /// </summary>
        void SetActiveScene(SceneID id);
    }
}