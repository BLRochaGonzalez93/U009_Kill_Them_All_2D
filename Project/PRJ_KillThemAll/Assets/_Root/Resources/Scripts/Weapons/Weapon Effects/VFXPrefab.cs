using UnityEngine;

/// <summary>
/// 
/// 
/// </summary>
[RequireComponent(typeof(Rigidbody2D))]
public class VFXPrefab : WeaponEffect
{
    public bool hasAutoAim = false;
    public bool hasRandomTarget = false;
    public Vector3 rotationSpeed = new(0, 0, 0);

    protected Rigidbody2D rb;
    protected int piercing;

    protected virtual void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        Weapon.Stats stats = weapon.GetStats();

        // Prevent the area from being 0, as it hides the projectile.
        float area = weapon.GetArea();
        if (area <= 0) area = 1;
        transform.localScale = new Vector3(
            stats.PreserveX ? Mathf.Abs(area * Mathf.Sign(transform.localScale.x)):(area * Mathf.Sign(transform.localScale.x)),
            area * Mathf.Sign(transform.localScale.y), 
            stats.ZScale? ( area * Mathf.Sign(transform.localScale.z)):1
        );

        // Set how much piercing this object has.
        piercing = stats.piercing;

        // Destroy the projectile after its lifespan expires.
        if (stats.lifespan > 0) Destroy(gameObject, stats.lifespan);
    }
}