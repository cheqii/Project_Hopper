using UnityEngine;

public class Player : MonoBehaviour
{
    public float jumpForce;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            print("jump");
            rb.velocity = (Vector2.up * jumpForce);
        }

        if (Input.GetKeyDown(KeyCode.O))
        {
            print("input here");
        }
    }
}
