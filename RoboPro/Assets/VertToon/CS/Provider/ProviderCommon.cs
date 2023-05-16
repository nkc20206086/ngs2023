using UnityEngine;
using System;
namespace AyahaShader.Provider
{
    public static class ProviderCommon
    {
        private static Color errorColor = Color.magenta;

        /// <summary>
        /// マテリアルにプロパティがあるか調べる(インスタンスの複製をしないで行う)
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="propName">プロパティ名</param>
        public static bool HasSharedMaterialProperty(Renderer renderer, string propName)
        {
            return renderer.sharedMaterial.HasProperty(propName);
        }

        /// <summary>
        /// materialPropertyBlockがSetされてなければSetする
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static void CheckAndSetPropertyBlock(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            if (!renderer.HasPropertyBlock())
            {
                renderer.SetPropertyBlock(materialPropertyBlock);
            }
        }

        /// <summary>
        /// 色を取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        public static Color GetColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName)
        {
            renderer.GetPropertyBlock(materialPropertyBlock);

            bool mpbHasProp = materialPropertyBlock.HasProperty(propName);

            if (mpbHasProp)
            {
                return materialPropertyBlock.GetColor(propName);
            }
            else
            {
                bool materialHasProp = ProviderCommon.HasSharedMaterialProperty(renderer, propName);
                if (materialHasProp)
                {
                    return renderer.sharedMaterial.GetColor(propName);
                }
                return errorColor;
            }
        }

        /// <summary>
        /// 色を変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        /// <param name="color">変更色</param>
        public static void SetColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, Color color)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor(propName, color);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }

        /// <summary>
        /// boolを取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        public static bool GetBool(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName)
        {
            renderer.GetPropertyBlock(materialPropertyBlock);

            bool mpbHasProp = materialPropertyBlock.HasProperty(propName);

            if (mpbHasProp)
            {
                return Convert.ToBoolean(materialPropertyBlock.GetFloat(propName));
            }
            else
            {
                bool materialHasProp = ProviderCommon.HasSharedMaterialProperty(renderer, propName);
                if (materialHasProp)
                {
                    return Convert.ToBoolean(renderer.sharedMaterial.GetFloat(propName));
                }
                return default;
            }
        }

        /// <summary>
        /// boolを変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        /// <param name="enable">bool</param>
        public static void SetBool(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, bool enable)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat(propName, Convert.ToSingle(enable));
            renderer.SetPropertyBlock(materialPropertyBlock);
        }

        /// <summary>
        /// floatを取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        public static float GetFloat(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName)
        {
            renderer.GetPropertyBlock(materialPropertyBlock);

            bool mpbHasProp = materialPropertyBlock.HasProperty(propName);

            if (mpbHasProp)
            {
                return materialPropertyBlock.GetFloat(propName);
            }
            else
            {
                bool materialHasProp = ProviderCommon.HasSharedMaterialProperty(renderer, propName);
                if (materialHasProp)
                {
                    return renderer.sharedMaterial.GetFloat(propName);
                }
                return default;
            }
        }

        /// <summary>
        /// floatを変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">プロパティ名</param>
        /// <param name="value">値</param>
        public static void SetFloat(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, float value)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat(propName, value);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
