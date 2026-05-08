using System.Collections.Generic;
using UnityEngine;

public class GameAssets : MonoBehaviour
{
    private static GameAssets _i;

    /// <summary>
    /// 
    /// </summary>
    public static GameAssets I
    {
        get
        {
            if (_i == null)
            {
                _i = Instantiate(Resources.Load<GameAssets>("Prefabs/GameAssets"));
            }
            return _i;
        }
    }

    public Transform prefabDamagePopUp;
    public Sprite playerIcon;
}