using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public GameObject Shooter;
    public float SecondsTillDestroying = 10;

    public event Action<GameObject, GameObject> ProjectileCollided;

    [HideInInspector]
    public float Speed;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(nameof(WaitTillDestroy));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed, 0, 0));
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject == Shooter)
            return;

        ProjectileCollided?.Invoke(Shooter, collision.gameObject);
        Destroy(gameObject);
    }

    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(SecondsTillDestroying);
        Destroy(gameObject);
    }
}