using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UISkillTimers : MonoBehaviour
{
    public PlayerInventory inventory;
    public List<float> availableWeaponsTimer;
    public List<float> weaponsTimer;
    public List<string> weaponsName;
    public List<Image> slots;
    // Start is called before the first frame update
    void Start()
    {
        inventory = FindObjectOfType<PlayerInventory>();
        for (int j = 0; j < inventory.availableWeapons.Count; j++)
        {
            availableWeaponsTimer[j] = inventory.availableWeapons[j].baseStats.cooldown;
            weaponsTimer[j] = availableWeaponsTimer[j];
        }
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < slots.Count; i++)
        {
            Image iconBG = slots[i].GetComponent<Image>();
            if (inventory.weaponSlots[i].item)
            {
                iconBG.enabled = true;
                string nm = inventory.weaponSlots[i].item.name.Substring(0, inventory.weaponSlots[i].item.name.Length - 11);
                for (int j = 0; j < inventory.availableWeapons.Count; j++)
                {
                    string aw = inventory.availableWeapons[j].baseStats.name;
                    if (nm == aw)
                    {
                        weaponsTimer[j] -= Time.deltaTime;
                        if (weaponsTimer[j] <= 0)
                        {
                            weaponsTimer[j] = availableWeaponsTimer[j];
                        }
                        Debug.Log("slot " + i + " /// " + aw + " /// " + availableWeaponsTimer[j]);
                        slots[i].fillAmount = weaponsTimer[j] / availableWeaponsTimer[j];
                    }
                }
            }
            else
            {
                iconBG.enabled = false;
            }

            /*weaponsTimer[i] -= Time.deltaTime;
            if (weaponsTimer[i] <= 0)
            {
                weaponsTimer[i] = inventory.availableWeapons[i].baseStats.cooldown;
            }

            slots[i].fillAmount = weaponsTimer[i] / inventory.availableWeapons[i].baseStats.cooldown;*/
        }
    }
}
