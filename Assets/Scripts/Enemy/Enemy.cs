using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Enemy : Character
{
    public EnemyState stateEnemy;
    public float waitTime;
    public float startWaitTime;
    public float rangeSpot;
    public bool seeRangeSpot;
    public int damageEnemy;
    public NavMeshAgent enemyAgent;

    public Transform[] patrolSpots;    
    public int randomSpot = 0;
    private float distance;

    public void MoveToPatrolPoint()
    {
        enemyAgent.destination = patrolSpots[randomSpot].position;
        
        if(Vector3.Distance(transform.position, patrolSpots[randomSpot].position) < 1.8f)
        {            
            if(waitTime <= 0)
            {        
                stateEnemy = EnemyState.WALKING;
                randomSpot++;  
                waitTime = startWaitTime;
            }
            else
            {
                stateEnemy = EnemyState.IDLE;
                waitTime -= Time.deltaTime;                
            }
            if(randomSpot >= patrolSpots.Length)
            {
                randomSpot = 0;
            }
        }
    }
    
    public void EnemyFollowPlayer()
    {
        distance = Vector3.Distance(PlayerController.instancePlayer.transform.position, transform.position);

        if(distance <= rangeSpot)
        {
            StartCoroutine(findPlayer());
            stateEnemy = EnemyState.RUNNING;
            enemyAgent.SetDestination(PlayerController.instancePlayer.transform.position);
        }
        if(distance <= enemyAgent.stoppingDistance)
        {
            FaceTarget();
            if(PlayerController.instancePlayer.stateCharacter == CharacterState.DISABLED)
            {
                stateEnemy = EnemyState.IDLE;
            }
            else
            {
                stateEnemy = EnemyState.ATTACK;
            }
        }
    }
    public void FaceTarget()
    {
        Vector3 direction = (PlayerController.instancePlayer.transform.position - transform.position).normalized;
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * 5f);
    }

    public IEnumerator findPlayer()
    {      
        stateEnemy = EnemyState.SURPRISE;
        yield return new WaitForSeconds(4f);               
    }

    void OnDrawGizmos()
    {
        if(seeRangeSpot)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, rangeSpot);
        }
    }
}
