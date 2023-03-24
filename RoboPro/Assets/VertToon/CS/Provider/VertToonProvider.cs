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
        /// メインカラーを変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">変更色</param>
        public static void SetMainColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, colorProp, color);
        }

        /// <summary>
        /// メインカラーを取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetMainColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, colorProp);
        }

        /// <summary>
        /// 頂点カラーを使用するか設定する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="enable">使うか？</param>
        public static void SetUseVertColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, bool enable)
        {
            ProviderCommon.SetBool(renderer, materialPropertyBlock, useVertColorProp, enable);
        }

        /// <summary>
        /// 頂点カラーを使用しているか取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static bool isUseVertColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetBool(renderer, materialPropertyBlock, useVertColorProp);
        }

        /// <summary>
        /// 隠れている時の色を取得する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        public static Color GetHideColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock)
        {
            return ProviderCommon.GetColor(renderer, materialPropertyBlock, hideColorProp);
        }

        /// <summary>
        /// 隠れている時の色を変更する
        /// </summary>
        /// <param name="renderer">renderer</param>
        /// <param name="materialPropertyBlock">materialPropertyBlock</param>
        /// <param name="color">変更色</param>
        public static void SetHideColor(Renderer renderer, MaterialPropertyBlock materialPropertyBlock, Color color)
        {
            ProviderCommon.SetColor(renderer, materialPropertyBlock, hideColorProp, color);
        }
    }
}
