using UnityEngine;
using Zenject;
using Ladder;
public class LadderInstaller : MonoInstaller
{
    [SerializeField] 
    private GameObject ladderClimbable;
    public override void InstallBindings()
    {
        Container.
            BindInterfacesTo<LadderDirector>().
            FromComponentOn(ladderClimbable).
            AsCached();
    }
}