using UnityEngine;
using Utility;
using Zenject;

public class PostEffectTest : MonoBehaviour
{
    [Inject]
    private IPostEffector postEffector;
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.P))
        {
            postEffector.SetMaterial(PostEffectMaterialKey.Compression);
            postEffector.Fade(FadeType.Out, 1, DG.Tweening.Ease.Linear);
        }
        else
        if(Input.GetKeyDown(KeyCode.O))
        {
            postEffector.SetMaterial(PostEffectMaterialKey.Compression);
            postEffector.Fade(FadeType.In, 1, DG.Tweening.Ease.Linear);
        }

        if (Input.GetKeyDown(KeyCode.I))
        {
            postEffector.SetMaterial(PostEffectMaterialKey.SimpleFade);
            postEffector.Fade(FadeType.Out, 1, DG.Tweening.Ease.Linear);
        }
        else
        if (Input.GetKeyDown(KeyCode.U))
        {
            postEffector.SetMaterial(PostEffectMaterialKey.SimpleFade);
            postEffector.Fade(FadeType.In, 1, DG.Tweening.Ease.Linear);
        }
    }
}
