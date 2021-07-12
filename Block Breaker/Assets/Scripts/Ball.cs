using UnityEngine;
using Random = UnityEngine.Random;

public class Ball : MonoBehaviour
{
    //configuration parameters
    [SerializeField] Paddle paddle1;
    [SerializeField] float xPush = 12f;
    [SerializeField] float yPush = 15f;
    [SerializeField] AudioClip[] ballSounds;
    [SerializeField] float xRandomFactor = 0.4f;
    [SerializeField] float yRandomFactor = 0.2f;
    [SerializeField] float xMaxSpeed = 10f;
    [SerializeField] float yMaxSpeed = 10f;
    public bool hasStarted = false;

    //state
    Vector2 paddleToBallDistance;

    //state variables
    Level level;

    //component gets
    AudioSource ballAudioSource;
    public Rigidbody2D myRigidbody2D;

    // Start is called before the first frame update
    void Start()
    {
        paddleToBallDistance = transform.position - paddle1.transform.position;
        ballAudioSource = GetComponent<AudioSource>();
        myRigidbody2D = GetComponent<Rigidbody2D>();
        level = FindObjectOfType<Level>();
        CountActiveBalls();
    }

    // Update is called once per frame
    void Update()
    {
        if (!hasStarted)
        {
            LockBallToPaddle();
            LaunchOnClick();
        }
    }

    private void LaunchOnClick()
    {
        if (Input.GetMouseButton(0))
        {
            myRigidbody2D.velocity = new Vector2(xPush, yPush);
            hasStarted = true;
        }
    }

    private void LockBallToPaddle()
    {
        Vector2 paddlePosition = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddleToBallDistance + paddlePosition;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.tag == "Ball")
        {
            Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
        }
        else if (collision.collider.tag == "LoseCollider")
        {
            Destroy(gameObject, 0f);
        }
        else
        {
            if (hasStarted)
            {
                float x = myRigidbody2D.velocity.x;
                float y = myRigidbody2D.velocity.y;
                if (x < 0f)
                {
                    yRandomFactor = -Mathf.Abs(yRandomFactor);
                } else
                {
                    yRandomFactor = Mathf.Abs(yRandomFactor);
                }
                if(y < 0f)
                {
                    xRandomFactor = -Mathf.Abs(xRandomFactor);
                }
                else
                {
                    xRandomFactor = Mathf.Abs(xRandomFactor);
                }
                if (x > xMaxSpeed)
                {
                    yRandomFactor = -Mathf.Abs(yRandomFactor);
                }
                else if (x < -xMaxSpeed) {
                    yRandomFactor = Mathf.Abs(yRandomFactor);
                }
                if (y > yMaxSpeed)
                {
                    xRandomFactor = -Mathf.Abs(xRandomFactor);
                }
                else if (y < -yMaxSpeed)
                {
                    xRandomFactor = Mathf.Abs(xRandomFactor);
                }
                Vector2 velocityTweak = new Vector2(Random.Range(0f, xRandomFactor), Random.Range(0f, yRandomFactor));
                myRigidbody2D.velocity += velocityTweak;
                AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
                ballAudioSource.PlayOneShot(clip);
            }
            //Debug.Log("x vel: " + myRigidbody2D.velocity.x);
            //Debug.Log("y vel: " + myRigidbody2D.velocity.x);
        }
    }

    private void CountActiveBalls()
    {
        level.CountActiveBalls();
    }


}
