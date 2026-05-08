using System.Collections;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class EnemyStats : MonoBehaviour
{
    public EnemyScriptableObject enemyData;

    //Current stats
    public float currentMoveSpeed;
    public float currentHealth;
    public float currentDamage;
    public float dmgTime;
    public float gameProgress;
    public AudioSource deathSfx;

    Transform player;

    [Header("Damage Feedback")]
    public Color damageColor = new(1, 0, 0, 1); // What the color of the damage flash should be.
    public float damageFlashDuration = 0.2f; // How long the flash should last.
    public float deathFadeTime = 0.6f; // How much time it takes for the enemy to fade.
    Color originalColor;
    SpriteRenderer sr;
    EnemyMovement movement;

    public static int count; // Track the number of enemies on the screen.

    void Awake()
    {
        count++;

        //Assign the vaiables
        currentMoveSpeed = enemyData.MoveSpeed;
        currentHealth = enemyData.MaxHealth;
        currentDamage = enemyData.Damage;
    }

    void Start()
    {
        player = FindObjectOfType<PlayerStats>().transform;
        deathSfx = GameObject.Find("Enemy Spawner").GetComponent<AudioSource>();
        sr = GetComponent<SpriteRenderer>();
        originalColor = sr.color;
        if (GameObject.Find("Game Manager").GetComponent<GameManager>().stopwatchTime <= 300)
        {
            gameProgress = 1;
        }
        else
        {
            if (GameObject.Find("Game Manager").GetComponent<GameManager>().stopwatchTime <= 800)
            {
                gameProgress = 0.95f;
            }
            else
            {
                gameProgress = 0.9f;
            }
        }
        movement = GetComponent<EnemyMovement>();
        dmgTime = 0;
    }

    private void Update()
    {
        dmgTime += Time.deltaTime;
    }

    // This function always needs at least 2 values, the amount of damage dealt <dmg>, as well as where the damage is
    // coming from, which is passed as <sourcePosition>. The <sourcePosition> is necessary because it is used to calculate
    // the direction of the knockback.
    public void TakeDamage(float dmg)
    {
        float hit = Random.Range(0f, 1f);
        bool isCriticalHit = hit > .8f;
        if (isCriticalHit) dmg *= 1.5f;
        currentHealth -= dmg * gameProgress;
        StartCoroutine(DamageFlash());

        // Create the text popup when enemy takes damage.
        if (dmg > 0)
            DmgPopUp.Create(transform.position, Mathf.RoundToInt(dmg), isCriticalHit);

        // Kills the enemy if the health drops below zero.
        if (currentHealth <= 0)
        {
            Kill();
        }
    }

    // This is a Coroutine function that makes the enemy flash when taking damage.
    IEnumerator DamageFlash()
    {
        sr.color = damageColor;
        yield return new WaitForSeconds(damageFlashDuration);
        sr.color = originalColor;
    }

    public void Kill()
    {
        deathSfx.Play();
        StartCoroutine(KillFade());
    }

    // This is a Coroutine function that fades the enemy away slowly.
    IEnumerator KillFade()
    {
        // Waits for a single frame.
        WaitForEndOfFrame w = new();
        float t = 0, origAlpha = sr.color.a;

        gameObject.GetComponent<Collider2D>().enabled = false;
        // This is a loop that fires every frame.
        while (t < deathFadeTime)
        {
            yield return w;
            t += Time.deltaTime;

            // Set the colour for this frame.
            sr.color = new Color(sr.color.r, sr.color.g, sr.color.b, (1 - t / deathFadeTime) * origAlpha);
        }

        Destroy(gameObject);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (dmgTime >= 1f)
            {
                PlayerStats player = collision.gameObject.GetComponent<PlayerStats>();
                player.TakeDamage(currentDamage);
                dmgTime = 0;
            }
        }
    }

    private void OnDestroy()
    {
        count--;
    }
}
