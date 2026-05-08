using System.Collections;
using UnityEngine;

public class SpawnSkill : MonoBehaviour
{
    public  GameObject prefab;
    float gTimer = 0;

    private void Update()
    {
        gTimer += Time.deltaTime;
        if (gTimer >= 3.5f+ Random.Range(1f,3f))
        {
            StartCoroutine(Fire());
            gTimer = 0;
        }
    }

    IEnumerator Fire()
    {
        Instantiate(prefab, transform.position, Quaternion.identity);
        yield return new WaitForSeconds(.5f);
      

    }
}