using System.Collections.Generic;

namespace Robo
{
    public class StageSelectModelArgs
    {
        //ステージの長さ
        public readonly IReadOnlyList<StageSelectElementInfo> Infos;

        public StageSelectModelArgs(IReadOnlyList<StageSelectElementInfo> elements)
        {
            this.Infos = elements;
        }
    }
}