using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyBullet : MonoBehaviour
{
    public float Speed;
    public float SecondsTillDestroying = 10;

    // Start is called before the first frame update
    void Start()
    {
            
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(new Vector3(Speed, 0, 0));
    }

    IEnumerator WaitTillDestroy()
    {
        yield return new WaitForSeconds(SecondsTillDestroying);
        Destroy(gameObject);
    }
}
