using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerHealth2 : MonoBehaviour
{
    public int startingHealth = 100;
    public int currentHealth;
    public Slider healthSlider;
    public Image damageImage;
    public AudioClip deathClip;
    public float flashSpeed = 5f;
    public Color flashColour = new Color(1f, 0f, 0f, 0.1f);
	public Camera mainCamera;
	public Camera PlayerCamera;
	public PlayerHealth playerHealth;
	public PlayerHealth2 player2Health;


    Animator anim;
    AudioSource playerAudio;
    PlayerMovement2 playerMovement;
    PlayerShooting2 playerShooting;
    bool isDead;
    bool damaged;

    void Awake ()
    {
        anim = GetComponent <Animator> ();
        playerAudio = GetComponent <AudioSource> ();
        playerMovement = GetComponent <PlayerMovement2> ();
        playerShooting = GetComponentInChildren <PlayerShooting2> ();
        currentHealth = startingHealth;
		mainCamera = GameObject.Find ("Main Camera 2").GetComponent<Camera>();
		PlayerCamera = GameObject.Find ("Main Camera").GetComponent<Camera>();
	}


    void Update ()
    {
        if(damaged)
        {
            damageImage.color = flashColour;
        }
        else
        {
            damageImage.color = Color.Lerp (damageImage.color, Color.clear, flashSpeed * Time.deltaTime);
        }
        damaged = false;
	}


    public void TakeDamage (int amount)
    {
        damaged = true;

        currentHealth -= amount;

        healthSlider.value = currentHealth;

        playerAudio.Play ();

        if(currentHealth <= 0 && !isDead)
        {
            Death ();
        }
    }


    void Death ()
    {
        isDead = true;

        playerShooting.DisableEffects ();

        anim.SetTrigger ("Die");

        playerAudio.clip = deathClip;
        playerAudio.Play ();

        playerMovement.enabled = false;
        playerShooting.enabled = false;

		StartCoroutine (CameraTurnOff ()); 
	}

	IEnumerator CameraTurnOff () 
	{
		yield return new WaitForSeconds (5);
		PlayerCamera.rect = Rect.MinMaxRect(0, 0, 1, 1);
		mainCamera.enabled = false;
		transform.Translate (-Vector3.up);
	}


	public void RestartLevel ()
	{
		if (playerHealth.currentHealth <= 0 && player2Health.currentHealth <= 0) {
			Application.LoadLevel (Application.loadedLevel);
		}
	}
}
