using System.Collections;
using NUnit.Framework.Constraints;
using UnityEngine;

public class Movement : MonoBehaviour
{
    [SerializeField] float jumpForce = 5f;
    [SerializeField] float rotateForce = 5f;
    [SerializeField] float cooldown = 0.15f;
    [SerializeField] float megaJumpCD = 3f;
    bool canJump = true;
    Rigidbody2D rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>(); 
    }

    void FixedUpdate()
    {
        float rotation = 0;
        if (Input.GetKey(KeyCode.D)) rotation--;
        if (Input.GetKey(KeyCode.A)) rotation++;
        if (Input.GetKey(KeyCode.Space) && canJump)
        {
            canJump = false;
            StartCoroutine(megaJumpCooldown());
            rb.AddForce(transform.up * jumpForce * 1.5f);
        }
        if(Input.GetKey(KeyCode.R)) UnityEngine.SceneManagement.SceneManager.LoadScene(UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
        

        rb.AddTorque(rotation * rotateForce);
    }

    IEnumerator jumpCooldown()
    {
        yield return new WaitForSeconds(cooldown);
        canJump = true;
    }

    IEnumerator megaJumpCooldown()
    {
        yield return new WaitForSeconds(megaJumpCD);
        canJump = true;
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.CompareTag("Environment") && canJump)
        {
            canJump = false;
            StartCoroutine(jumpCooldown());
            float angle = Vector2.Angle(collision.contacts[0].normal, transform.up);

            if (0f <= angle && angle < 90f)
            {
                float jumpMult = (180f - angle) / 120f;
                jumpMult = Mathf.Clamp(jumpMult, 0f, 1f);
                //float jumpMult = 1f;
                rb.AddForce(transform.up * jumpForce * jumpMult);
            }
            else
            {
                Debug.Log("DIE DIE DIE");
                //gameObject.GetComponent<SpriteRenderer>().color = Color.red;
            }
        }
        else if (collision.transform.CompareTag("finish"))
        {
            // UnityEngine.SceneManagement.SceneManager.LoadScene(
            // UnityEngine.SceneManagement.SceneManager.GetActiveScene().buildIndex);
            Debug.Log("WINNER WINNER CHICKEN DINNER");
        }
    }
}
