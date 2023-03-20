namespace Robo
{
    public class SetActiveSceneArgs
    {
        /// <summary>以前アクティブであったシーンのID</summary>
        public readonly SceneID BeforeScene;

        /// <summary>現在アクティブであるシーンのID</summary>
        public readonly SceneID NowScene;

        public SetActiveSceneArgs(SceneID beforeScene, SceneID nowScene)
        {
            BeforeScene = beforeScene;
            NowScene = nowScene;
        }
    }
}
