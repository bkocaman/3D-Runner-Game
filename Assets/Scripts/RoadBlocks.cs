using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoadBlocks : MonoBehaviour
{
    public GameObject player;
    public GameObject roadBlock;
    public GameObject roadBlockSplit;

    public float minForce = 125f;
    public float maxForce = 125f;
    public float radius = 2f;

    public void CollisionObject() {
       
        player.GetComponent<PlayerController>().HitObstacle();
        roadBlock.SetActive(false);
        roadBlockSplit.SetActive(true);
        StartCoroutine(Explode());

    }

    public IEnumerator Explode() {
        foreach (Transform t in roadBlockSplit.transform) {
            var rb = t.GetComponent<Rigidbody>();

            if(rb != null)
                rb.AddExplosionForce(Random.Range(minForce, maxForce), roadBlockSplit.transform.position, radius);
        }
        yield return new WaitForSeconds(1);
        Destroy(roadBlockSplit);
        Destroy(roadBlock);
    }

}
