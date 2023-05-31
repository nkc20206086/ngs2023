
using UnityEngine;

namespace Robo
{
    public class GoalCameraView : MonoBehaviour
    {
        [SerializeField]
        private Vector2 goalCameraOffset = new Vector2(2, 0.3f);

        private GoalCameraPosition position;

        public void Initalize(GoalCameraPosition position)
        {
            this.position = position;
        }

        private void Update()
        {
            Vector3 goalPos = GameObject.FindObjectOfType<Goal>().transform.position;
            switch (position)
            {
                case GoalCameraPosition.East:
                    transform.position = goalPos + new Vector3(goalCameraOffset.x, goalCameraOffset.y, 0);
                    break;
                case GoalCameraPosition.West:
                    transform.position = goalPos + new Vector3(-goalCameraOffset.x, goalCameraOffset.y, 0);
                    break;
                case GoalCameraPosition.South:
                    transform.position = goalPos + new Vector3(0, goalCameraOffset.y, goalCameraOffset.x);
                    break;
                case GoalCameraPosition.North:
                    transform.position = goalPos + new Vector3(0, goalCameraOffset.y, -goalCameraOffset.x);
                    break;
            }
        }
    }
}
