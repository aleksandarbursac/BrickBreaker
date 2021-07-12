using UnityEngine;

public class BallPowerup : MonoBehaviour
{
    [SerializeField] float powerupDropSpeed = 0.5f;
    [SerializeField] int dropTimeLowerEnd = 5;
    [SerializeField] int dropTimeHigherEnd = 15;
    [SerializeField] int startXCoord = 0;
    [SerializeField] int endXCoord = 15;
    bool isGameStarted = false;
    bool hasDropped = false;

    //variables
    Rigidbody2D myRigidbody2D;
    Transform myTransform;
    Ball[] allBalls;

    void Start()
    {
        myRigidbody2D = GetComponent<Rigidbody2D>();
        myTransform = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasDropped)
        {
            allBalls = FindObjectsOfType<Ball>();
            foreach (Ball b in allBalls)
            {
                if (b.hasStarted)
                {
                    isGameStarted = true;
                }
                else
                {
                    isGameStarted = false;
                    break;
                }
            }
            if (isGameStarted)
            {
                Invoke("StartDropping", Random.Range(dropTimeLowerEnd, dropTimeHigherEnd));
                hasDropped = true;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Paddle")
        {
            HandlePowerup();
            Destroy(gameObject, 0f);
        } else if (collision.tag == "LoseCollider")
        {
            Destroy(gameObject, 0f);
        }
    }

    private void HandlePowerup()
    {
        allBalls = FindObjectsOfType<Ball>();
        foreach (Ball b in allBalls)
        {
            Ball ballClone1 = Instantiate(b);
            ballClone1.myRigidbody2D.velocity = new Vector2(-b.myRigidbody2D.velocity.x, -b.myRigidbody2D.velocity.y);
        }
    }

    private void StartDropping()
    {
        myTransform.position = new Vector3(Random.Range(startXCoord, endXCoord), myTransform.position.y, myTransform.position.z);
        myRigidbody2D.velocity = new Vector2(0f, -powerupDropSpeed);
        Debug.Log("Started dropping");
    }
}
