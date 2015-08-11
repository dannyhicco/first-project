using UnityEngine;

public class GameOverManager : MonoBehaviour
{
    public PlayerHealth playerHealth;
	public PlayerHealth2 player2Health;
	public float restartDelay = 5f;


    Animator anim;
	float restartTimer;


    void Awake()
    {
        anim = GetComponent<Animator>();
    }


    void Update()
    {
		if (playerHealth.currentHealth <= 0 || player2Health.currentHealth <= 0)
        {
            anim.SetTrigger("GameOver");
        
			restartTimer += Time.deltaTime;
			
			if (restartTimer >= restartDelay)
			{
				Application.LoadLevel (Application.loadedLevel);
			}
		}
    }
}
