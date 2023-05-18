using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using Zenject;

namespace Robo
{
    public class MultiSceneLoader : IMultiSceneLoader
    {
        public event Action<SceneID> OnAddScene;
        public event Action<SceneID> OnUnloadScene;
        public event Action<SetActiveSceneArgs> OnActiveScene;

        private ZenjectSceneLoader zenjectSceneLoader;
        private SceneID nowActiveScene;

        [Inject]
        public MultiSceneLoader(ZenjectSceneLoader zenjectSceneLoader)
        {
            this.zenjectSceneLoader = zenjectSceneLoader;
        }

        //シーン追加
        //extraBindingsで、追加するシーンに今のシーンの情報をバインドできる。
        async UniTask IMultiSceneLoader.AddScene(SceneID id, bool autoChangeActiveScene,  Action<DiContainer> extraBindings)
        {
            await zenjectSceneLoader.LoadSceneAsync(id.ToString(), LoadSceneMode.Additive, extraBindings);
            if (autoChangeActiveScene)
            {
                ((IMultiSceneLoader)this).SetActiveScene(id);
            }
            OnAddScene?.Invoke(id);
        }

        //シーン追加
        async UniTask IMultiSceneLoader.AddScene(SceneID id, bool autoChangeActiveScene)
        {
            await ((IMultiSceneLoader)this).AddScene(id, autoChangeActiveScene, null);
        }
        //シーン削除
        async UniTask IMultiSceneLoader.UnloadScene(SceneID id)
        {
            await SceneManager.UnloadSceneAsync(id.ToString());
            await Resources.UnloadUnusedAssets();
            OnUnloadScene?.Invoke(id);
        }

        //アクティブシーンを決定する
        void IMultiSceneLoader.SetActiveScene(SceneID id)
        {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(id.ToString()));
            OnActiveScene?.Invoke(new SetActiveSceneArgs(nowActiveScene, id));
        }
    }
}