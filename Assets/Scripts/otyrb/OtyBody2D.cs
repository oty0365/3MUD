using UnityEngine;

[RequireComponent(typeof(OtyColider2D))]
public class OtyBody2D : MonoBehaviour, ColiderObj
{
    public float gravityScale = 9.8f;
    private OtyColider2D colider2D;
    private bool isGrounded = false;

    void Start()
    {
        colider2D = GetComponent<OtyColider2D>();
    }

    void FixedUpdate()
    {
        if (!isGrounded)
        {
            transform.position -= new Vector3(0, Time.fixedDeltaTime * gravityScale, 0);
        }

        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {
            gravityScale = -9.8f;
            isGrounded = false;
        }
    }

    public void Enter()
    {
        isGrounded = true;
        gravityScale = 0;
    }

    public void Stay()
    {
        Debug.Log("Stay");
    }

    public void Exit()
    {
        Debug.Log("Exit");
        isGrounded = false;
        gravityScale = 9.8f;
    }
}

