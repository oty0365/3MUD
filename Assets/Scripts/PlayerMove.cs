using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    private Rigidbody2D _rb2D;
    private bool _isGrounded;

    void Start()
    {
        _isGrounded = true;
        _rb2D = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        
    }
    public void Jump()
    {
        if (_isGrounded)
        {
            _rb2D.AddForce(new Vector2(0, 5f), ForceMode2D.Impulse);
            _isGrounded = false;
        }

    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag("Ground"))
        {
            _isGrounded = true;
        }
    }
}
