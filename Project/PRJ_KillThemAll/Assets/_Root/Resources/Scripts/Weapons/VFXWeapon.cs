using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Damage does not scale with Might stat currently.
public class VFXWeapon : Weapon
{
    protected float currentAttackInterval;
    protected int currentAttackCount; // Number of times this attack will happen.

    List<EnemyStats> allSelectedEnemies = new();

    protected override void Update()
    {
        base.Update();

        // Otherwise, if the attack interval goes from above 0 to below, we also call attack.
        if (currentAttackInterval > 0)
        {
            currentAttackInterval -= Time.deltaTime;
            if (currentAttackInterval <= 0) Attack(currentAttackCount);
        }
    }

    public override bool CanAttack()
    {
        if (currentAttackCount > 0) return true;
        return base.CanAttack();
    }

    protected override bool Attack(int attackCount = 1)
    {
        // If no hitEffectPrefab prefab is assigned, leave a warning message.
        if (!currentStats.hitEffectPrefab)
        {
            Debug.LogWarning(string.Format("Hit effect prefab has not been set for {0}", name));
            ActivateCooldown(true);
            return false;
        }

        // If there is no projectile assigned, set the weapon on cooldown.
        if (!CanAttack()) return false;

        string cSName = currentStats.name;
        if (cSName.StartsWith("Twising Slash"))
        {
            VFXPrefab go = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go.weapon = this;
            go.owner = owner;
            StartCoroutine(Knight_TwisingSlash(GetArea(), GetDamage()));
            Destroy(go.transform.gameObject, currentStats.lifespan);
        }
        else if (cSName.StartsWith("Multi Cut"))
        {
            VFXPrefab go2 = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go2.weapon = this;
            go2.owner = owner;
            StartCoroutine(Assassin_MultiCut(GetArea(), GetDamage()));
            Destroy(go2.transform.gameObject, currentStats.lifespan);
        }
        else if (cSName.StartsWith("Poison Bottle"))
        {
            for (int i = 0; i < currentStats.number; i++)
            {
                EnemyStats target = PickEnemy();
                VFXPrefab go3 = Instantiate(currentStats.hitEffectPrefab, target.transform);
                go3.weapon = this;
                go3.owner = owner;
                go3.transform.SetParent(null);
                Destroy(go3.transform.gameObject, currentStats.lifespan);
                StartCoroutine(Assassin_PoisonBottle(target.transform.position, GetArea(), GetDamage()));
            }
        }
        else if (cSName.StartsWith("Divine Sword"))
        {
            for (int i = 0; i < currentStats.number; i++)
            {
                EnemyStats target = PickEnemy();
                VFXPrefab go4 = Instantiate(currentStats.hitEffectPrefab, target.transform);

                go4.weapon = this;
                go4.owner = owner;
                go4.transform.SetParent(null);
                Destroy(go4.transform.gameObject, currentStats.lifespan);
                StartCoroutine(Cleric_DivineSword(target.transform.position, GetArea(), GetDamage()));
            }
        }
        else if (cSName.StartsWith("Sacred Floor"))
        {
            VFXPrefab go5 = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go5.transform.parent = GameObject.Find("Sacred Floor Controller").transform;
            go5.weapon = this;
            go5.owner = owner;
            StartCoroutine(Cleric_SacredFloor(GetArea(), GetDamage()));
            Destroy(go5.transform.gameObject, currentStats.lifespan);
        }
        else if (cSName.StartsWith("Solar Beam"))
        {
            for (int i = 0; i < currentStats.number; i++)
            {
                EnemyStats target = PickEnemy();
                VFXPrefab go3 = Instantiate(currentStats.hitEffectPrefab, target.transform);
                go3.weapon = this;
                go3.owner = owner;
                go3.transform.SetParent(null);
                Destroy(go3.transform.gameObject, currentStats.lifespan);
                StartCoroutine(Cleric_SolarBeam(target.transform.position, GetArea(), GetDamage()));
            }
        }
        else if (cSName.StartsWith("Earth Shake"))
        {
            StartCoroutine(DragonLord_EarthShake(owner.transform.position, GetArea(), GetDamage()));
        }
        else if (cSName.StartsWith("Star Fall"))
        {
            StartCoroutine(Hunter_StarFall(owner.transform.position, GetArea(), GetDamage()));
        }
        else if (cSName.StartsWith("Inferno"))
        {
            VFXPrefab go6 = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go6.weapon = this;
            go6.owner = owner;
            StartCoroutine(Sorcerer_Inferno(GetArea(), GetDamage()));
            Destroy(go6.transform.gameObject, currentStats.lifespan);
        }
        else if (cSName.StartsWith("Thunder"))
        {
            for (int i = 0; i < currentStats.number; i++)
            {
                EnemyStats target = PickEnemy();
                VFXPrefab go7 = Instantiate(currentStats.hitEffectPrefab, target.transform);
                go7.weapon = this;
                go7.owner = owner;
                go7.transform.SetParent(null);
                Destroy(go7.transform.gameObject, currentStats.lifespan);
                StartCoroutine(Sorcerer_Thunder(target.transform.position, GetArea(), GetDamage()));
            }
        }
        else if (cSName.StartsWith("Traps"))
        {
            StartCoroutine(Hunter_Traps(owner.transform.position, GetArea(), GetDamage()));
        }
        else if (cSName.StartsWith("Hand Of Justice"))
        {
            VFXPrefab go9 = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go9.weapon = this;
            go9.owner = owner;
            StartCoroutine(Cleric_HandOfJustice(GetArea()));
            Destroy(go9.transform.gameObject, currentStats.lifespan);
        }
        else if (cSName.StartsWith("Shadow Step"))
        {
            if (!GameObject.Find("SmokeTrailAssasin(Clone)"))
            {
                StartCoroutine(Assassin_ShadowStep());
            }
        }

        ActivateCooldown(true);

        attackCount--;

        // If we have more than 1 attack count.
        if (attackCount > 0)
        {
            currentAttackCount = attackCount - 1;
            currentAttackInterval = currentStats.projectileInterval;
        }

        return true;
    }

    // Randomly picks an enemy on screen.
    EnemyStats PickEnemy()
    {
        EnemyStats[] targets = FindObjectsOfType<EnemyStats>();
        foreach (EnemyStats currentEnemy in targets)
        {
            allSelectedEnemies.Add(currentEnemy);
        }

        EnemyStats target = null;
        while (!target)
        {
            int idx = Random.Range(0, allSelectedEnemies.Count);
            target = allSelectedEnemies[idx];
        }
        return target;
    }

    private IEnumerator Knight_TwisingSlash(float radius, float damage)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < (3 * currentStats.number); i++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds((currentStats.lifespan - 1) / (3 * currentStats.number));
        }
    }
    private IEnumerator Assassin_MultiCut(float radius, float damage)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < (5 * currentStats.number); i++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds((currentStats.lifespan - 1) / (5 * currentStats.number));
        }
    }
    private IEnumerator Assassin_PoisonBottle(Vector2 position, float radius, float damage)
    {
        yield return new WaitForSeconds(1f);
        for (int j = 0; j < 5; j++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(currentStats.lifespan / 5);
        }
    }
    private IEnumerator Cleric_DivineSword(Vector2 position, float radius, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D t in targets)
        {
            EnemyStats es = t.GetComponent<EnemyStats>();
            if (es)
            {
                es.TakeDamage(damage);
                if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator Cleric_SacredFloor(float radius, float damage)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < (2 * currentStats.number); i++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds((currentStats.lifespan - 1) / (2 * currentStats.number));
        }
    }
    private IEnumerator Cleric_SolarBeam(Vector2 position, float radius, float damage)
    {
        for (int j = 0; j < 5; j++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(currentStats.lifespan / 5);
        }
    }
    private IEnumerator DragonLord_EarthShake(Vector2 position, float radius, float damage)
    {

        for (int i = 0; i < currentStats.number; i++)
        {
            VFXPrefab go = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go.weapon = this;
            go.owner = owner;
            go.transform.SetParent(null);
            Destroy(go.transform.gameObject, currentStats.lifespan);
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator Hunter_StarFall(Vector2 position, float radius, float damage)
    {

        for (int i = 0; i < currentStats.number; i++)
        {
            VFXPrefab go = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go.weapon = this;
            go.owner = owner;
            //go.transform.SetParent(null);
            Destroy(go.transform.gameObject, currentStats.lifespan);
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(0.5f);
        }
    }
    private IEnumerator Sorcerer_Inferno(float radius, float damage)
    {
        yield return new WaitForSeconds(1);
        for (int i = 0; i < (8 * currentStats.number); i++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds((currentStats.lifespan - 1) / (8 * currentStats.number));
        }
    }
    private IEnumerator Sorcerer_Thunder(Vector2 position, float radius, float damage)
    {
        Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
        foreach (Collider2D t in targets)
        {
            EnemyStats es = t.GetComponent<EnemyStats>();
            if (es)
            {
                es.TakeDamage(damage);
                if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
            }
        }
        yield return new WaitForSeconds(0.5f);
    }
    private IEnumerator Cleric_HandOfJustice(float radius)
    {
        for (int i = 0; i < 10; i++)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(owner.transform.position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(currentStats.lifespan / 10);
        }
    }
    private IEnumerator Hunter_Traps(Vector2 position, float radius, float damage)
    {
        VFXPrefab go = Instantiate(currentStats.hitEffectPrefab, owner.transform);
        go.weapon = this;
        go.owner = owner;
        go.transform.SetParent(null);
        Destroy(go.transform.gameObject, currentStats.lifespan);
        if(go != null) yield return new WaitForSeconds(1);
        while (go != null)
        {
            Collider2D[] targets = Physics2D.OverlapCircleAll(position, radius);
            foreach (Collider2D t in targets)
            {
                EnemyStats es = t.GetComponent<EnemyStats>();
                if (es)
                {
                    es.TakeDamage(damage);
                    Destroy(go.transform.gameObject);
                    if (currentStats.knockback == 1) es.GetComponent<EnemyMovement>().DoKnockBack();
                }
            }
            yield return new WaitForSeconds(currentStats.projectileInterval);
        }
        yield return null;
    }
    private IEnumerator Assassin_ShadowStep()
    {
        do
        {
            yield return new WaitForSeconds(0.1f);
        } while (!owner.GetComponent<PlayerMovement>().isDashing);
        if (owner.GetComponent<PlayerMovement>().isDashing)
        {
            VFXPrefab go10 = Instantiate(currentStats.hitEffectPrefab, owner.transform);
            go10.weapon = this;
            go10.owner = owner;
            owner.transform.GetComponent<Collider2D>().enabled = false;
            owner.GetComponent<PlayerMovement>().dashTime += (currentStats.speed * 0.1f);
            currentCooldown = currentStats.cooldown;
            yield return new WaitForSeconds(.5f);
            owner.transform.GetComponent<Collider2D>().enabled = true;
            owner.GetComponent<PlayerMovement>().dashTime -= (currentStats.speed * 0.1f);
            Destroy(go10.gameObject);
        }
    }

    protected virtual Vector2 GetSpawnOffset(float spawnAngle = 0)
    {
        return Quaternion.Euler(0, 0, spawnAngle) * new Vector2(
            Random.Range(currentStats.spawnVariance.xMin, currentStats.spawnVariance.xMax),
            Random.Range(currentStats.spawnVariance.yMin, currentStats.spawnVariance.yMax)
        );
    }
}