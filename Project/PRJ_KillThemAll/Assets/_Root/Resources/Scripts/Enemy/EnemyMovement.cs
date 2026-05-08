using System.Collections;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    EnemyStats enemy;
    Transform player;

    float distTimer=0;

    Vector2 knockbackVelocity;
    float knockbackDuration;
    bool facingRight = true;

    void Start()
    {
        enemy = GetComponent<EnemyStats>();
        player = FindObjectOfType<PlayerMovement>().transform;
    }

    void Update()
    {
        distTimer += Time.deltaTime;
        if (distTimer > 1f )
        {
            if (Vector2.Distance(enemy.transform.position,player.transform.position) >= 30f)
            {
                Vector3 sm = SpawnManager.GeneratePosition();
                transform.position = sm;
            }
        }


        CheckDirectionSprite();
        // If we are currently being knocked back, then process the knockback.
        if (knockbackDuration > 0)
        {
            transform.position += (Vector3)knockbackVelocity * Time.deltaTime;
            knockbackDuration -= Time.deltaTime;
        }
        else
        {
            // Otherwise, constantly move the enemy towards the player
            transform.position = Vector2.MoveTowards(transform.position, player.transform.position, (enemy.currentMoveSpeed * Random.Range(.5f, 1.5f)) * Time.deltaTime);
        }
    }

    private void CheckDirectionSprite()
    {
        Vector3 delta = player.position - transform.position;

        if (delta.x >= 0 && !facingRight)
        { // Enemy is on right side of _player
            transform.localScale = new Vector3(1, 1, 1); // or activate look right some other way
            facingRight = true;
        }
        else if (delta.x < 0 && facingRight)
        { // Enemy is on left side
            transform.localScale = new Vector3(-1, 1, 1); // activate looking left
            facingRight = false;
        }
    }

    public void DoKnockBack()
    {
        StartCoroutine(KnockBack());
    }

    private IEnumerator KnockBack()
    {
        for (int i = 0; i < 10; i++)
        {
            if (player != null)
            {
                transform.position = Vector2.MoveTowards(transform.position, player.position, -enemy.currentMoveSpeed * 10f * Time.deltaTime);
                yield return new WaitForSeconds(0.01f);
            }
        }
    }
    
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraViewBox"))
        {
            gameObject.GetComponent<Animator>().enabled = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("CameraViewBox"))
        {
            gameObject.GetComponent<Animator>().enabled = false;
        }
    }
}
