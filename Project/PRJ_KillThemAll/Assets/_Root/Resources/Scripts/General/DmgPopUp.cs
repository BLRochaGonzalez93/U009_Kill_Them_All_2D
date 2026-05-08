using TMPro;
using UnityEngine;

public class DmgPopUp : MonoBehaviour
{
    #region Damage PopUp Variables
    private const float DISAPPEAR_MAX_TIMER = 1f;

    [SerializeField] private Transform prefabDmgPopUp;
    private TextMeshPro textMesh;
    private float disappearTimer;
    private Color textColor;
    private Vector3 moveVector;
    #endregion

    private void Awake()
    {
        textMesh = transform.GetComponent<TextMeshPro>();
    }

    public void Update()
    {
        transform.position += moveVector * Time.deltaTime;
        moveVector -= 8 * Time.deltaTime * moveVector;

        if (disappearTimer > DISAPPEAR_MAX_TIMER * 0.8f)
        {
            float increaseScaleAmount = 2f;
            transform.localScale += increaseScaleAmount * Time.deltaTime * Vector3.one;
        }
        else
        {
            float decreaseScaleAmount = .7f;
            transform.localScale -= decreaseScaleAmount * Time.deltaTime * Vector3.one;
        }

        disappearTimer -= Time.deltaTime;

        if (disappearTimer < 0)
        {
            float disappearDelay = 3f;
            textColor.a -= disappearDelay * Time.deltaTime;
            textMesh.color = textColor;
            if (textColor.a < 0)
            {
                Destroy(gameObject);
            }
        }
    }

    #region Create & Modify Methods
    public static DmgPopUp Create(Vector3 position, int amount, bool isCriticalHit)
    {
        Transform damagePopupTransform = Instantiate(GameAssets.I.prefabDamagePopUp, position, Quaternion.identity);
        DmgPopUp damagePopup = damagePopupTransform.GetComponent<DmgPopUp>();
        damagePopup.Setup(amount, isCriticalHit);

        return damagePopup;
    }

    public void Setup(int damageAmount, bool isCriticalHit)
    {
        textMesh.SetText(damageAmount.ToString());
        if (!isCriticalHit)
        {
            textMesh.fontSize = 2;
            textMesh.color = new Color(255f, 114f, 0f);
        }
        else
        {
            textMesh.fontSize = 4;
            textMesh.color = new Color(255f, 0f, 0f);
        }
        textColor = textMesh.color;
        disappearTimer = DISAPPEAR_MAX_TIMER;

        moveVector = new Vector3(.7f, 1) * 5f;
    }
    #endregion
}