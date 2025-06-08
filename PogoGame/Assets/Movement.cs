using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Movement : MonoBehaviour
{
    public static Movement Instance;
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float rotateForce = 5f;
    [SerializeField] float cooldown = 0.15f;
    [SerializeField] float megaJumpCD = 3f;
    float megaJumpTimer;
    bool canJump = true;
    bool canMegaJump = true;
    Rigidbody2D rb;

    public bool active = true;

    void Awake()
    {
        Instance = this;
        rb = GetComponent<Rigidbody2D>(); 
    }

    void Update()
    {
        if (!active) return;
        if (megaJumpTimer > 0f)
        {
            megaJumpTimer -= Time.deltaTime;
            if (megaJumpTimer <= 0f) canMegaJump = true;
        }
        GameUI.Instance.megaJumpSlider.value = (megaJumpCD - megaJumpTimer) / megaJumpCD;
    }

    void FixedUpdate()
    {
        if (!active) return;
        float rotation = 0;
        if (Input.GetKey(KeyCode.D)) rotation--;
        if (Input.GetKey(KeyCode.A)) rotation++;
        if(Input.GetKey(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        

        rb.AddTorque(rotation * rotateForce);
    }

    IEnumerator jumpCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canJump = true;
    }

    void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.transform.CompareTag("finish"))
        {
            active = false;
            GameUI.Instance.Win();
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (!active) return;
        if (collision.transform.CompareTag("Environment") && canJump)
        {
            canJump = false;
            StartCoroutine(jumpCooldown());
            float angle = Vector2.Angle(collision.contacts[0].normal, transform.up);

            if (0f <= angle && angle < 90f)
            {
                float jumpMult = (180f - angle) / 120f;
                jumpMult = Mathf.Clamp(jumpMult, 0f, 1f);
                if (Input.GetKey(KeyCode.Space) && canMegaJump)
                {
                    jumpMult = 1.625f;
                    canMegaJump = false;
                    megaJumpTimer = megaJumpCD;
                }
                rb.AddForce(transform.up * jumpForce * jumpMult);
            }
        }
    }
}
