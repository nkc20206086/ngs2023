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
        /// ���̐F���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetWaterColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, waterColorProp);
        }

        /// <summary>
        /// ���̐F��ݒ肷��
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">���̐F</param>
        public static void SetWaterColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, waterColorProp, color);
        }

        /// <summary>
        /// �A�̐F���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetFoamColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, foamColorProp);
        }

        /// <summary>
        /// �A�̐F��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">�A�̐F</param>
        public static void SetFoamColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, foamColorProp, color);
        }

        /// <summary>
        /// �������擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static float GetHeight(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetFloat(renderer, materialPropertyBlock, heightProp);
        }

        /// <summary>
        /// ������ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="value">����</param>
        public static void SetHeight(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, float value)
        {
            ProviderCommon.SetFloat(renderer, materialPropertyBlock, heightProp, value);
        }
    }
}
