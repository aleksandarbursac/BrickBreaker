using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class Block : MonoBehaviour
{
    // config params
    [SerializeField] AudioClip[] breakAudio;
    [SerializeField] GameObject blockSparklesVFX;
    //[SerializeField] int maxHits;
    [SerializeField] Sprite[] hitSprites;

    // cached reference
    Level level;
    GameStatus gameStatus;

    // states variables
    [SerializeField] int timesHit; // only serialized for debugging
 

    private void Start()
    {
        gameStatus = FindObjectOfType<GameStatus>();
        CountBreakableBlocks();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (gameObject.tag.Equals("Breakable"))
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        timesHit++;
        int maxHits = hitSprites.Length + 1;
        if (timesHit >= maxHits)
        {
            PlayBlockDestroySFXs();
            DestroyBlock();
        }
        else
        {
            PlayBlockDestroySFXs();
            ShowNextHitSprite();
        }
    }

    private void ShowNextHitSprite()
    {
        int spriteIndex = timesHit - 1;
        if(hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Block sprite is missing from array for block: " + name);
        }
    }

    private void DestroyBlock()
    {
        gameStatus.IncreaseScore();
        Destroy(gameObject, 0f);
        level.BlockDestroyed();
        TriggerSparklesVFX();
    }

    private void PlayBlockDestroySFXs() {

        if (breakAudio.Length > 0)
        {
            AudioSource.PlayClipAtPoint(breakAudio[Random.Range(0, breakAudio.Length)], Camera.main.transform.position);
        }
        //AudioSource.PlayClipAtPoint(breakAudio, GetComponent<Transform>().transform.position);
    }

    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(blockSparklesVFX, transform.position, transform.rotation);
        Destroy(sparkles, 2f);
       // blockSparklesVFX = GameObject.Instantiate()
    }

    private void CountBreakableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (gameObject.tag.Equals("Breakable"))
        {
            level.CountBreakableBlocks();
        }
    }

    public Vector3 ReturnPosition()
    {
        return gameObject.transform.position;
    }
}
