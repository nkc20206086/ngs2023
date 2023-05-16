using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace AyahaShader.VertToon
{
    public class VertToonInfo : MonoBehaviour
    {
        private static string version = "0.1.1";
        private static string repositoryLink = "https://github.com/ayaha401/VertToonShader";

        /// <summary>
        /// ���݂̃o�[�W����
        /// </summary>
        public static string GetVersion()
        {
            return version;
        }

        /// <summary>
        /// ���|�W�g���ւ̃����N
        /// </summary>
        public static string GetRepositoryLink()
        {
            return repositoryLink;
        }
    }
}
