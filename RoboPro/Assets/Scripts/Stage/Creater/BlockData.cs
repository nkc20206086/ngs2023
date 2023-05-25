using UnityEngine;

[System.Serializable]
public class BlockData
{
    [SerializeField] private BlockID id;
    [SerializeField] private GameObject obj_Odd;
    [SerializeField] private GameObject obj_Even;

    public BlockID ID => id;
    public GameObject Obj_Odd => obj_Odd;
    public GameObject Obj_Even => obj_Even;
}
