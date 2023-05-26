using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Ladder;
public class LadderClimbTest : MonoBehaviour
{
    [Inject] ILadderClimbable ladderClimbable;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        LadderClimbData data = ladderClimbable.GetLadderClimableData(transform);
        if (data.isClimableLadder) Debug.Log($"Pos={data.climbPos} Type={data.climbType}");
    }
}
