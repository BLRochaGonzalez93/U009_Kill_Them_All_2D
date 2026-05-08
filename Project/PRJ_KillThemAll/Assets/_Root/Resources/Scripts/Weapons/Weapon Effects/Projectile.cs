using UnityEngine;

/// <summary>
/// Component that you attach to all projectile prefabs. All spawned projectiles will fly in the direction
/// they are facing and deal damage when they hit an object.
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class Projectile : WeaponEffect
{
    public bool hasAutoAim = false;
    public bool hasRandomTarget = false;
    public Vector3 rotationSpeed = new(0, 0, 0);

    protected Rigidbody2D rb;
    protected int piercing;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Weapon.Stats stats = weapon.GetStats();
        if (rb.bodyType == RigidbodyType2D.Dynamic)
        {
            rb.angularVelocity = rotationSpeed.z;
            rb.linearVelocity = stats.speed * weapon.Owner.Stats.speed * transform.right;
        }

        // Prevent the area from being 0, as it hides the projectile.
        float area = weapon.GetArea();
        if(area <= 0) area = 1;
        transform.localScale = new Vector3(
            area * Mathf.Sign(transform.localScale.x),
            area * Mathf.Sign(transform.localScale.y), 1
        );

        // Set how much piercing this object has.
        piercing = stats.piercing;

        // Destroy the projectile after its lifespan expires.
        if (stats.lifespan > 0) Destroy(gameObject, stats.lifespan);

        // If the projectile is auto-aiming, automatically find a suitable enemy.
        if (hasAutoAim && hasRandomTarget) AcquireAutoRandomFacing();
        else if (hasAutoAim && !hasRandomTarget) AcquireAutoNearestFacing();
    }

    // If the projectile is homing, it will automatically find a suitable target
    // to move towards.
    public virtual void AcquireAutoRandomFacing()
    {
        float aimAngle; // We need to determine where to aim.

        // Find all enemies on the screen.
        EnemyStats[] targets = FindObjectsOfType<EnemyStats>();

        // Select a random enemy (if there is at least 1).
        // Otherwise, pick a random angle.
        if (targets.Length > 0)
        {
            EnemyStats selectedTarget = targets[Random.Range(0, targets.Length)];
            Vector2 difference = selectedTarget.transform.position - transform.position;
            aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            aimAngle = Random.Range(0f, 360f);
        }

        // Point the projectile towards where we are aiming at.
        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    public virtual void AcquireAutoNearestFacing()
    {
        float aimAngle; // We need to determine where to aim.
        // Find all enemies on the screen.
        EnemyStats[] targets = FindObjectsOfType<EnemyStats>();
        float distanceToClosestEnemy = Mathf.Infinity;
        EnemyStats selectedTarget = null;

        // Select a random enemy (if there is at least 1).
        // Otherwise, pick a random angle.
        if (targets.Length > 0)
        {
            foreach (EnemyStats currentEnemy in targets)
            {
                float distanceToEnemy = (currentEnemy.transform.position - transform.position).sqrMagnitude;
                if (distanceToEnemy < distanceToClosestEnemy)
                {
                    distanceToClosestEnemy = distanceToEnemy;
                    selectedTarget = currentEnemy;
                }
            }
            Vector2 difference = selectedTarget.transform.position - transform.position;
            aimAngle = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        }
        else
        {
            aimAngle = Random.Range(0f, 360f);
        }

        transform.rotation = Quaternion.Euler(0, 0, aimAngle);
    }

    // Update is called once per frame
    protected virtual void FixedUpdate()
    {
        // Only drive movement ourselves if this is a kinematic.
        if (rb.bodyType == RigidbodyType2D.Kinematic)
        {
            Weapon.Stats stats = weapon.GetStats();
            transform.position += stats.speed * Time.fixedDeltaTime * weapon.Owner.Stats.speed * transform.right;
            rb.MovePosition(transform.position);
            transform.Rotate(rotationSpeed * Time.fixedDeltaTime);
        }
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        EnemyStats es = other.GetComponent<EnemyStats>();
        BreakableProps p = other.GetComponent<BreakableProps>();

        // Only collide with enemies or breakable stuff.
        if (es)
        {
            // Deals damage and destroys the projectile.
            es.TakeDamage(GetDamage());
            es.GetComponent<EnemyMovement>().DoKnockBack();

            Weapon.Stats stats = weapon.GetStats();
            piercing--;
            if (stats.hitEffectPrefab)
            {
                Destroy(Instantiate(stats.hitEffectPrefab, transform.position, Quaternion.identity), 5f);
            }
        }
        else if (p)
        {
            p.TakeDamage(GetDamage());
            piercing--;

            Weapon.Stats stats = weapon.GetStats();
            if (stats.hitEffectPrefab)
            {
                Destroy(Instantiate(stats.hitEffectPrefab, transform.position, Quaternion.identity), 5f);
            }
        }

        // Destroy this object if it has run out of health from hitting other stuff.
        if (piercing <= 0) Destroy(gameObject);
    }
}