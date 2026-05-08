using UnityEngine;

public class EnemySkillProj : MonoBehaviour
{
    Vector3 playerPos;
    public float speed = 10.0f;
    public float dmg = 20f;
    public GameObject dir;

    // Start is called before the first frame update
    void Start()
    {
        transform.SetParent(null);
        playerPos = GameObject.Find("Player").transform.position;
        dir.transform.position = playerPos;
        Destroy(gameObject, 5f);
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector2.MoveTowards(transform.position, dir.transform.position, Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.GetComponent<PlayerStats>().TakeDamage(dmg);
            Destroy(gameObject);
        }
    }
}
