using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnAnimation : Singleton<RespawnAnimation>
{
    public Animator Anim { get; private set; }

    private void Awake()
    {
        Anim = GetComponent<Animator>();
    }

    private void Start()
    {
        Anim.SetBool("IsRespawn", true);
    }
}
