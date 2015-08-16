using UnityEngine;
using System.Collections;

public class EnemyMovement : MonoBehaviour
{
    Transform player;
	Transform player2;
    PlayerHealth playerHealth;
	PlayerHealth2 player2Health;
    EnemyHealth enemyHealth;
    NavMeshAgent nav;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player").transform;
		player2 = GameObject.FindGameObjectWithTag ("Player2").transform;
        playerHealth = player.GetComponent <PlayerHealth> ();
		player2Health = player2.GetComponent <PlayerHealth2> ();
        enemyHealth = GetComponent <EnemyHealth> ();
        nav = GetComponent <NavMeshAgent> ();
    }


    void Update ()
    {
		if (enemyHealth.currentHealth > 0 && (playerHealth.currentHealth > 0 || player2Health.currentHealth > 0)) {
			if (playerHealth.currentHealth > 0 && player2Health.currentHealth > 0) {
				if (Vector3.Distance (transform.position, player.position) < Vector3.Distance (transform.position, player2.position)) {
					nav.SetDestination (player.position);
				} 
				else {
					nav.SetDestination (player2.position);
				}
			}
			if(playerHealth.currentHealth <=0){
				nav.SetDestination (player2.position);
			}
			if(player2Health.currentHealth <=0){
				nav.SetDestination (player.position);
			}		
		}
        else
        {
            nav.enabled = false;
        }
    }
}
