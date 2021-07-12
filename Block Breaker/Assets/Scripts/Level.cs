using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Level : MonoBehaviour
{
    [SerializeField] int breakableBlocks;
    [SerializeField] int activeBallCount;
    SceneLoader sceneLoader;

    private void Start()
    {
        sceneLoader = FindObjectOfType<SceneLoader>();
    }

    public void CountBreakableBlocks() {
        breakableBlocks++;
    }

    public void BlockDestroyed()
    {
        breakableBlocks--;
        if (breakableBlocks <= 0)
        {
            sceneLoader.LoadNextScene();
        }
    }

    public void CountActiveBalls()
    {
        activeBallCount++;
        Debug.Log("Ball number currently = " + activeBallCount.ToString());
    }

    public void DestroyBall(Collision2D collision)
    {
        activeBallCount--;
        if (activeBallCount <= 0)
        {
            SceneManager.LoadScene("Game Over");
        }
        Debug.Log("Ball number after destroying = " + activeBallCount.ToString());
    }
}
