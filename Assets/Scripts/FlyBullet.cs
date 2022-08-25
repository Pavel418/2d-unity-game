using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : MonoBehaviour
{
    public GameObject Shooter;
    public Attack Attack = new() { Damage = 20};
    public float Speed;
    public float SecondsTillDestroying = 10;

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

        var events = collision.gameObject.GetComponents<IAttackable>(); 
        
        foreach (var e in events)
        {
            e.OnAttack(gameObject, Attack);
        }
        Destroy(gameObject);
    }

    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(SecondsTillDestroying);
        Destroy(gameObject);
    }
}
