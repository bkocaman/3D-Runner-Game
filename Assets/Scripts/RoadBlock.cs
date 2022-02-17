using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlock : MonoBehaviour
{
    public GameObject RoadBlocks;

    void OnTriggerEnter(Collider c) {
        Debug.Log("Hit to object.");
        RoadBlocks.GetComponent<RoadBlocks>().CollisionObject();
    }
}
