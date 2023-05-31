namespace Robo
{
    public static class GoToStageArgmentsSingleton
    {
        private static StageSelectElementInfo instance;
        private static bool isClear = false;

        public static void SetStage(StageSelectElementInfo info)
        {
            instance = info;
            isClear = false;
        }

        public static StageSelectElementInfo Get()
        {
            return instance;
        }

        public static void Clear()
        {
            isClear = true;
        }

        public static bool IsClear()
        {
            return isClear;
        }
    }
}