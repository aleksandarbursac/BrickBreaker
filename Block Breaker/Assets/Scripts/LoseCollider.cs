using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoseCollider : MonoBehaviour
{

    //cached reference
    Level level;

    private void Start()
    {
        level = FindObjectOfType<Level>();
    }

    // Start is called before the first frame update
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Debug.Log("Ball collided with lose collider");
            level.DestroyBall(collision);
        }
    }
}
