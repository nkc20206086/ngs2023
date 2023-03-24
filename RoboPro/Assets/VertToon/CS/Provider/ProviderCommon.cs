using UnityEngine;
using System;
namespace AyahaShader.Provider
{
    public static class ProviderCommon
    {
        private static Color errorColor = Color.magenta;

        /// <summary>
        /// �}�e���A���Ƀv���p�e�B�����邩���ׂ�(�C���X�^���X�̕��������Ȃ��ōs��)
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="propName">�v���p�e�B��</param>
        public static bool HasSharedMaterialProperty(Renderer renderer, string propName)
        {
            return renderer.sharedMaterial.HasProperty(propName);
        }

        /// <summary>
        /// materialPropertyBlock��Set����ĂȂ����Set����
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
        /// �F���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
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
        /// �F��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
        /// <param name="color">�ύX�F</param>
        public static void SetColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, Color color)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetColor(propName, color);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }

        /// <summary>
        /// bool���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
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
        /// bool��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
        /// <param name="enable">bool</param>
        public static void SetBool(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, bool enable)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat(propName, Convert.ToSingle(enable));
            renderer.SetPropertyBlock(materialPropertyBlock);
        }

        /// <summary>
        /// float���擾����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
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
        /// float��ύX����
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="propName">�v���p�e�B��</param>
        /// <param name="value">�l</param>
        public static void SetFloat(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, string propName, float value)
        {
            ProviderCommon.CheckAndSetPropertyBlock(renderer, materialPropertyBlock);

            renderer.GetPropertyBlock(materialPropertyBlock);
            materialPropertyBlock.SetFloat(propName, value);
            renderer.SetPropertyBlock(materialPropertyBlock);
        }
    }
}
