namespace Robo
{
    //キューシートは、各シーンごとに使われるオーディオファイルを分別するために使用される
    //タイトル画面で使われる効果音はTitleというジャンルに分けられる
    //BGMは例外で、BGM名ごとに管理されている
    public enum CueSheetType
    {
        //SEが使用されるシーン名
        Command = 0,
        Player = 1,
        Staging = 2,
        System = 3,

        //BGM名
        ClearBGM = 1000,
        StageBGM = 1001,
        StageSelectBGM = 1002,
        TitleBGM = 1003,
    }
}