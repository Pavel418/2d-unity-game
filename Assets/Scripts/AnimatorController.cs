using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class AnimatorController : MonoBehaviour
{
    public static readonly string IDLE = "Idle";
    public static readonly string RUN = "Run";
    public static readonly string SHOOT = "Shoot";

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

    public void PlayAnimation(string animation)
    {
        _animator.Play(animation);
    }
}
