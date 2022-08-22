using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IsGrounded : MonoBehaviour
{
    public bool isGrounded;
    public const string JumpAbleLayerName = "Solid Object";

    private BoxCollider2D _boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        _boxCollider = GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        CheckGround();
    }

    public void CheckGround()
    {
        isGrounded = false;

        List<Collider2D> results = new();
        ContactFilter2D filter2D = new();
        Physics2D.OverlapCollider(_boxCollider, filter2D.NoFilter(), results);

        foreach (var collider in results)
        {
            if (collider.gameObject.layer == LayerMask.NameToLayer(JumpAbleLayerName))
            {
                Debug.Log(collider.gameObject.name);
                isGrounded = true;
                break;
            }
        }
    }
    /*
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("enter" + collision.gameObject.name);
        isGrounded = collision.collider.gameObject.layer == LayerMask.NameToLayer(JumpAbleLayerName);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        Debug.Log("exit");
        isGrounded = false;
    }*/
}
