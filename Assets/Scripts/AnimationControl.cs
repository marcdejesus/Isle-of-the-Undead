using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationControl : MonoBehaviour
{
    Animator anim;
    Enemy enemy;

    // Start is called before the first frame update
    void Start()
    {
        // Get reference to the animator
        anim = GetComponent<Animator>();
        // Get reference to the enemy script
        enemy = GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        // Set animation parameters
        anim.SetFloat("speed", enemy.GetSpeed());
        anim.SetFloat("direction", enemy.GetDirection());
        anim.SetFloat("angularspeed", enemy.GetAngularSpeed());
        anim.SetBool("attack", enemy.IsAttacking());
        anim.SetBool("damage", enemy.IsTakingDamage());
        anim.SetBool("death", enemy.IsDead());
    }
}
