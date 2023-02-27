using System.Collections.Generic;

namespace Robo
{
    public class StageSelectModelArgs
    {
        //ステージの長さ
        public readonly IReadOnlyList<StageSelectElementInfo> Infos;

        public StageSelectModelArgs(List<StageSelectElementInfo> elements)
        {
            this.Infos = elements;
        }
    }
}