using UnityEngine;

namespace Utility
{
    public interface IPostEffectMaterialDatabase
    {
        Shader GetMaterial(PostEffectMaterialKey key);
    }
}