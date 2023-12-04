using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class BirdController : MonoBehaviour
{
    [SerializeField] private GameManager gameManager;
    [SerializeField] private GameObject[] normalFrames;
    [SerializeField] private GameObject[] hungerFrames;
    private GameObject frame1;
    private GameObject frame2;

    private SpriteRenderer spriteRenderer;
    public float jumpForce = 5f;

    private bool isDead = false;
    private bool isEat = false;
    private Rigidbody2D rb2d;
    private int score = 0;

    public bool IsDead { get => isDead; set => isDead = value; }
    public bool IsEat { get => isEat; set => isEat = value; }
    public int Score { get => score; set => score = value; }

    private void Start()
    {
        Score = 0;
        frame1 = normalFrames[0];
        frame2 = normalFrames[1];
        rb2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponentInChildren <SpriteRenderer>();

        StartCoroutine(MenuAnimation(0.3f));
    }

    private void Update()
    {
        if (gameManager.StartGame)
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                if (!IsDead)
                {
                    StartCoroutine(JumpAnimation(0.3f));
                    Jump();
                }
            }

            float targetAngle = 3f * rb2d.velocity.y;
            transform.eulerAngles = new Vector3(0f, 0f, Mathf.LerpAngle(transform.eulerAngles.z, targetAngle, Time.deltaTime * 5f));

            

            GameObject[] currentFrames;
            GameObject[] otherFrames;

            if (gameManager.HungryImage.fillAmount < 0.5)
            {
                currentFrames = hungerFrames;
                otherFrames = normalFrames;
            }
            else
            {
                currentFrames = normalFrames;
                otherFrames = hungerFrames;
            }

            SwitchFrames(currentFrames[0], currentFrames[1]);
            DisableFrames(otherFrames[0], otherFrames[1]);
        }
    }

    private void SwitchFrames(GameObject newFrame1, GameObject newFrame2)
    {
        if (frame1 != newFrame1 || frame2 != newFrame2)
        {
            frame1.SetActive(false);
            frame2.SetActive(false);

            frame1 = newFrame1;
            frame2 = newFrame2;

            frame1.SetActive(true);
            frame2.SetActive(true);
        }
    }

    private void DisableFrames(GameObject frameToDisable1, GameObject frameToDisable2)
    {
        frameToDisable1.SetActive(false);
        frameToDisable2.SetActive(false);
    }

    private IEnumerator FrameTransition(GameObject newFrame1, GameObject newFrame2, float seconds)
    {
        frame1.SetActive(false);
        frame2.SetActive(true);
        yield return new WaitForSeconds(seconds);
        frame1.SetActive(true);
        frame2.SetActive(false);

        frame1 = newFrame1;
        frame2 = newFrame2;
    }

  

    private void Jump()
    {
        rb2d.velocity = Vector2.up * jumpForce;
    }

    private System.Collections.IEnumerator JumpAnimation(float seconds)
    {
        frame1.SetActive(false);
        frame2.SetActive(true);
        yield return new WaitForSeconds(seconds);
        frame1.SetActive(true);
        frame2.SetActive(false);
    }

    private System.Collections.IEnumerator MenuAnimation(float seconds)
    {
        while (!gameManager.StartGame)
        {
            frame1.SetActive(true);
            frame2.SetActive(false);
            yield return new WaitForSeconds(seconds);
            frame1.SetActive(false);
            frame2.SetActive(true);
            yield return new WaitForSeconds(seconds);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Pipe") || collision.gameObject.CompareTag("Ground"))
        {
            isDead = true;
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Trigger"))
        {
            score++;
        }
        else if (collider.gameObject.CompareTag("Bug"))
        {
            collider.gameObject.SetActive(false);
            isEat = true;
            Debug.Log(isEat);
        }
    }

    public void Die()
    {
        if (isDead)
        {
            rb2d.velocity = Vector2.zero;
            transform.position = new Vector2(transform.position.x - 0.1f, transform.position.y);
            rb2d.GetComponent<CircleCollider2D>().enabled = false;
            spriteRenderer.flipY = true;
        }
    }
}
