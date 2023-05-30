using UnityEngine;

namespace Stage
{
    public class StageObject : MonoBehaviour
    {
        public Vector3Int Position { get; private set; }

        public void Initalize(Vector3Int pos)
        {
            Position = pos;
        }
    }
}