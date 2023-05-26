using UnityEngine;
using Zenject;
using ScreenMask;

public class TutorialMaskInstaller : MonoInstaller
{
    [SerializeField]
    private GameObject tutorialMaskObj;

    public override void InstallBindings()
    {
        Container.BindInterfacesAndSelfTo<TutorialMaskController>()
                 .FromComponentOn(tutorialMaskObj)
                 .AsCached();
    }
}