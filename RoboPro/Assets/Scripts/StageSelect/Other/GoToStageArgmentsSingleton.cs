namespace Robo
{
    public static class GoToStageArgmentsSingleton
    {
        private static StageSelectElementInfo instance;

        public static void SetStage(StageSelectElementInfo info)
        {
            instance = info;
        }

        public static StageSelectElementInfo Get()
        {
            return instance;
        }
    }
}