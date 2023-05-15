using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

namespace AyahaShader.Provider
{
    public static class VertToonProvider
    {
        private static string colorProp = "_Color";
        private static string useVertColorProp = "_UseVertColor";
        private static string hideColorProp = "_HideColor";

        /// <summary>
        /// ���C���J���[��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">�ύX�F</param>
        public static void SetMainColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, colorProp, color);
        }

        /// <summary>
        /// ���C���J���[���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetMainColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, colorProp);
        }

        /// <summary>
        /// ���_�J���[���g�p���邩�ݒ肷��
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="enable">�g�����H</param>
        public static void SetUseVertColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, bool enable)
        {
            ProviderCommon.SetBool(renderer, materialPropertyBlock, useVertColorProp, enable);
        }

        /// <summary>
        /// ���_�J���[���g�p���Ă��邩�擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static bool isUseVertColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetBool(renderer, materialPropertyBlock, useVertColorProp);
        }

        /// <summary>
        /// �B��Ă��鎞�̐F���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetHideColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, hideColorProp);
        }

        /// <summary>
        /// �B��Ă��鎞�̐F��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">�ύX�F</param>
        public static void SetHideColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, hideColorProp, color);
        }
    }
}
