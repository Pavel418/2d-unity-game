using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    public static readonly string IDLE = "Idle";
    public static readonly string RUN = "Run";
    public static readonly string SHOOT = "Shoot";
    public static readonly string RUN_SHOOT = "RunNgun";

    Animator _animator;

    // Start is called before the first frame update
    void Start()
    {
        _animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public float PlayAnimation(string animation)
    {
        _animator.Play(animation);
        return _animator.GetCurrentAnimatorClipInfo(0)[0].clip.length;
    }
}
