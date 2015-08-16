using UnityEngine;
using System.Collections;

public class EnemyAttack : MonoBehaviour
{
    public float timeBetweenAttacks = 0.5f;
    public int attackDamage = 10;


    Animator anim;
    GameObject player;
	GameObject player2;
    PlayerHealth playerHealth;
	PlayerHealth2 player2Health;
    EnemyHealth enemyHealth;
    bool playerInRange;
	bool player2InRange;
    float timer;


    void Awake ()
    {
        player = GameObject.FindGameObjectWithTag ("Player");
		player2 = GameObject.FindGameObjectWithTag ("Player2");
        playerHealth = player.GetComponent <PlayerHealth> ();
		player2Health = player2.GetComponent <PlayerHealth2> ();
        enemyHealth = GetComponent <EnemyHealth>();
        anim = GetComponent <Animator> ();
    }


    void OnTriggerEnter (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = true;
        }
		if(other.gameObject == player2)
		{
			player2InRange = true;
		}
    }


    void OnTriggerExit (Collider other)
    {
        if(other.gameObject == player)
        {
            playerInRange = false;
        }
		if(other.gameObject == player2)
		{
			player2InRange = false;
		}
    }


    void Update ()
    {
        timer += Time.deltaTime;

        if(timer >= timeBetweenAttacks && (playerInRange || player2InRange) && enemyHealth.currentHealth > 0)
        {
            Attack ();
        }

		if(playerHealth.currentHealth <= 0 && player2Health.currentHealth <= 0)
        {
            anim.SetTrigger ("PlayerDead");
        }

    }


    void Attack ()
    {
        timer = 0f;

		if (playerInRange) {
			if (playerHealth.currentHealth > 0) {
				playerHealth.TakeDamage (attackDamage);
			}
		}

		if (player2InRange) {
			if (player2Health.currentHealth > 0) {
				player2Health.TakeDamage (attackDamage);
			}
		}
    }
}
