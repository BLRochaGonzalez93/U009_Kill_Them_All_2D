using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

[RequireComponent(typeof(VerticalLayoutGroup))]
public class UIUpgradeWindow : MonoBehaviour
{

    VerticalLayoutGroup verticalLayout;
    public RectTransform upgradeOptionTemplate;
    public TextMeshProUGUI tooltipTemplate;

    [Header("Settings")]
    public int maxOptions = 4;
    public string iconPath = "Icon/Item Icon", namePath = "Name", descriptionPath = "Description", buttonPath = "Button";

    RectTransform rectTransform; // The RectTransform of this element.
    float optionHeight; // The default height of the upgradeOptionTemplate.
    int activeOptions; // Tracks the number of options that are active currently.
    List<RectTransform> upgradeOptions = new();
    Vector2 lastScreen;

    public void SetUpgrades(PlayerInventory inventory, List<ItemData> possibleUpgrades, int pick = 3, string tooltip = "") 
    {
        pick = Mathf.Min(maxOptions, pick);

        // If we don't have enough upgrade option boxes, create them.
        if (maxOptions > upgradeOptions.Count)
        {
            for (int i = upgradeOptions.Count; i < pick; i++)
            {
                GameObject go = Instantiate(upgradeOptionTemplate.gameObject, transform);
                upgradeOptions.Add((RectTransform)go.transform);
            }
        }

        // If a string is provided, turn on the tooltip.
        if (tooltip != "")
        {
            tooltipTemplate.text = tooltip;
            tooltipTemplate.gameObject.SetActive(true);
        }
        else
        {
            tooltipTemplate.gameObject.SetActive(false);
        }

        // Activate only the number of upgrade options we need, and arm the buttons and the
        // different attributes like descriptions, etc.
        activeOptions = 0;
        foreach(RectTransform r in upgradeOptions)
        {
            if (activeOptions < possibleUpgrades.Count)
            {
                r.gameObject.SetActive(true);

                // Select one of the possible upgrades, then remove it from the list.
                ItemData selected = possibleUpgrades[Random.Range(0, possibleUpgrades.Count)];
                possibleUpgrades.Remove(selected);
                Item item = inventory.Get(selected);

                // Insert the name of the item.
                TextMeshProUGUI name = r.Find(namePath).GetComponent<TextMeshProUGUI>();
                if(name)
                {
                    name.text = selected.name;
                    if(item)
                    {
                        name.text += " - Level " + (item.currentLevel + 1);
                    }
                }

                // Insert the description of the item.
                TextMeshProUGUI desc = r.Find(descriptionPath).GetComponent<TextMeshProUGUI>();
                if (desc)
                {
                    if (item)
                    {
                        desc.text = selected.GetLevelData(item.currentLevel + 1).description;
                    }
                    else
                    {
                        desc.text = selected.GetLevelData(1).description;
                    }
                }

                // Insert the icon of the item.
                Image icon = r.Find(iconPath).GetComponent<Image>();
                if(icon)
                {
                    icon.sprite = selected.icon;
                }

                // Insert the button action binding.
                Button b = r.Find(buttonPath).GetComponent<Button>();
                if (b)
                {
                    b.onClick.RemoveAllListeners();
                    if (item)
                        b.onClick.AddListener(() => inventory.LevelUp(item));
                    else
                        b.onClick.AddListener(() => inventory.Add(selected));
                }
            }
            else r.gameObject.SetActive(false);
        }

        // Sizes all the elements so they do not exceed the size of the box.
        RecalculateLayout();
    }

    // Recalculates the heights of all elements.
    // Called whenever the size of the window changes.
    void RecalculateLayout()
    {
        // Calculates the total available height for all options, then divides it by the number of options.
        optionHeight = (rectTransform.rect.height - verticalLayout.padding.top - verticalLayout.padding.bottom - (maxOptions - 1) * verticalLayout.spacing);
        if (activeOptions == maxOptions && tooltipTemplate.gameObject.activeSelf)
            optionHeight /= maxOptions + 1;
        else
            optionHeight /= maxOptions;

        // Displays the tooltip.
        if (tooltipTemplate.gameObject.activeSelf)
        {
            RectTransform tooltipRect = (RectTransform)tooltipTemplate.transform;
            tooltipTemplate.gameObject.SetActive(true);
            tooltipRect.sizeDelta = new Vector2(tooltipRect.sizeDelta.x, optionHeight);
            tooltipTemplate.transform.SetAsLastSibling();
        }

        // Sets the height of every active Upgrade Option button.
        foreach (RectTransform r in upgradeOptions)
        {
            if (!r.gameObject.activeSelf) continue;
            r.sizeDelta = new Vector2(r.sizeDelta.x, optionHeight);
        }
    }

    void Update()
    {
        RecalculateLayout();
    }

    // Start is called before the first frame update
    void Awake()
    {
        verticalLayout = GetComponentInChildren<VerticalLayoutGroup>();
        if (tooltipTemplate) tooltipTemplate.gameObject.SetActive(false);
        if (upgradeOptionTemplate) upgradeOptions.Add(upgradeOptionTemplate);

        // Get the RectTransform of this object for height calculations.
        rectTransform = (RectTransform)transform;
    }

    void Reset()
    {
        upgradeOptionTemplate = (RectTransform)transform.Find("Upgrade Option");
        tooltipTemplate = transform.Find("Tooltip").GetComponentInChildren<TextMeshProUGUI>();
    }
}
