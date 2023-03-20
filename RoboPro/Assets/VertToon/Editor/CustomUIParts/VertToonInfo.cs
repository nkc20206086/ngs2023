using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.VertToon
{
    public class VertToonInfo : MonoBehaviour
    {
        private static string version = "0.0.5";
        private static string repositoryLink = "https://github.com/ayaha401/VertToonShader";

        /// <summary>
        /// 現在のバージョン
        /// </summary>
        public static string GetVersion()
        {
            return version;
        }

        /// <summary>
        /// リポジトリへのリンク
        /// </summary>
        public static string GetRepositoryLink()
        {
            return repositoryLink;
        }
    }
}
