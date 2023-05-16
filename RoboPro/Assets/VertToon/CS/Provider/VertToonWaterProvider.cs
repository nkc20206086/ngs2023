using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.Provider
{
    public static class VertToonWaterProvider
    {
        private static string waterColorProp = "_WaterColor";
        private static string foamColorProp = "_FoamColor";
        private static string heightProp = "_Height";

        /// <summary>
        /// 水の色を取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetWaterColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, waterColorProp);
        }

        /// <summary>
        /// 水の色を設定する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">水の色</param>
        public static void SetWaterColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, waterColorProp, color);
        }

        /// <summary>
        /// 泡の色を取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetFoamColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, foamColorProp);
        }

        /// <summary>
        /// 泡の色を変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">泡の色</param>
        public static void SetFoamColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, foamColorProp, color);
        }

        /// <summary>
        /// 高さを取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static float GetHeight(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetFloat(renderer, materialPropertyBlock, heightProp);
        }

        /// <summary>
        /// 高さを変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="value">高さ</param>
        public static void SetHeight(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, float value)
        {
            ProviderCommon.SetFloat(renderer, materialPropertyBlock, heightProp, value);
        }
    }
}
