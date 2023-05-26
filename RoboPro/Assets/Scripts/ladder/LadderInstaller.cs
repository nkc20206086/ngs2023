using UnityEngine;
using Zenject;
using Ladder;
public class LadderInstaller : MonoInstaller
{
    [SerializeField] 
    private GameObject ladderDirector;
    public override void InstallBindings()
    {
        Container.
            BindInterfacesTo<LadderDirector>().
            FromComponentOn(ladderDirector).
            AsCached();
    }
}