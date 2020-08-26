using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : Enemy
{
    public Animator animEnemy;
    public Collider colliderEnemy;
    public float speedWalking;
    public float speedRunning;

    void Start()
    {
        AddLife(maxLife);
        waitTime = startWaitTime;
    }
    void Update()
    {
        AnimationsEnemy();
        if (stateCharacter != CharacterState.DEAD)
        {
            MoveToPatrolPoint();
            EnemyFollowPlayer();
        }
    }

    public void Attack()
    {
        PlayerController.instancePlayer.TakeDamage(damageEnemy);
    }

    public void EndAttack()
    {
        stateEnemy = EnemyState.IDLE;
    }

    void AnimationsEnemy()
    {
        switch (stateEnemy)
        {
            case EnemyState.IDLE:
                animEnemy.SetBool("attack", false);
                animEnemy.SetBool("running", false);
                animEnemy.SetBool("surprise", false);
                animEnemy.SetBool("idle", true);
                animEnemy.SetBool("walking", false);
                break;

            case EnemyState.WALKING:
                enemyAgent.speed = speedWalking;
                animEnemy.SetBool("attack", false);
                animEnemy.SetBool("running", false);
                animEnemy.SetBool("surprise", false);
                animEnemy.SetBool("idle", false);
                animEnemy.SetBool("walking", true);
                break;

            case EnemyState.SURPRISE:
                enemyAgent.speed = 0f;
                animEnemy.SetBool("attack", false);
                animEnemy.SetBool("running", false);
                animEnemy.SetBool("surprise", true);
                animEnemy.SetBool("idle", false);
                animEnemy.SetBool("walking", false);
                break;

            case EnemyState.RUNNING:
                enemyAgent.speed = speedRunning;
                animEnemy.SetBool("attack", false);
                animEnemy.SetBool("running", true);
                animEnemy.SetBool("surprise", false);
                animEnemy.SetBool("idle", false);
                animEnemy.SetBool("walking", false);
                break;

            case EnemyState.ATTACK:
                animEnemy.SetBool("attack", true);
                animEnemy.SetBool("running", false);
                animEnemy.SetBool("surprise", false);
                animEnemy.SetBool("idle", false);
                animEnemy.SetBool("walking", false);
                break;

            case EnemyState.DISABLED:
                animEnemy.SetBool("attack", false);
                animEnemy.SetBool("running", false);
                animEnemy.SetBool("surprise", false);
                animEnemy.SetBool("idle", false);
                animEnemy.SetBool("walking", false);
                colliderEnemy.enabled = false;
                break;
        }
        if (stateCharacter == CharacterState.DEAD && stateEnemy != EnemyState.DISABLED)
        {
            enemyAgent.speed = 0f;
            animEnemy.SetBool("dead", true);
            stateEnemy = EnemyState.DISABLED;
        }
    }
}
