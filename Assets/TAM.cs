using UnityEngine;
using System.Collections.Generic;

public class TAM : MonoBehaviour
{
    public float moveSpeed = 5f;
    public AudioClip shootSound;

    private AudioSource audioSource;
    private Vector3 recoilOrigin;

    private List<GameObject> ballsInRange = new List<GameObject>();

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    void Update()
    {
        Move();

        if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
        {
            ShootEffect();
            TryShootBall();
        }
    }

    void Move()
    {
        float moveX = Input.GetAxisRaw("Horizontal");
        float moveY = Input.GetAxisRaw("Vertical");

        Vector2 move = new Vector2(moveX, moveY).normalized;
        transform.Translate(move * moveSpeed * Time.deltaTime);
    }

    void ShootEffect()
    {
        if (audioSource != null && shootSound != null)
        {
            audioSource.PlayOneShot(shootSound);
        }

        recoilOrigin = transform.position;
        Vector3 upOffset = new Vector3(0, 0.1f, 0);
        transform.position += upOffset;
        Invoke("ResetPosition", 0.05f);
    }

    void ResetPosition()
    {
        transform.position = recoilOrigin;
    }

    void TryShootBall()
    {
        if (ballsInRange.Count > 0)
        {
            GameObject target = ballsInRange[0];
            ballsInRange.RemoveAt(0);
            Destroy(target);
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && !ballsInRange.Contains(collision.gameObject))
        {
            ballsInRange.Add(collision.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Ball") && ballsInRange.Contains(collision.gameObject))
        {
            ballsInRange.Remove(collision.gameObject);
        }
    }
}