using UnityEngine;
using Zenject;
using Gimmick;
using Gimmick.Interface;

public class StageInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject gimmickDirector;

    public override void InstallBindings()
    {
        Container.
            Bind<IGimmickAccess>().
            To<GimmickDirector>().
            FromComponentOn(gimmickDirector).
            AsTransient();
    }
}