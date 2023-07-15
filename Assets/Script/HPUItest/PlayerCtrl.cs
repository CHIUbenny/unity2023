using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MonoBehaviour
{
    public static PlayerCtrl instance;
    [SerializeField]
    public CPlayerData playerData;
    private void Awake()
    {
        instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Damage(float damage)
    {
        playerData.Hp -= damage;
        if (playerData.Hp < 0)
        {
            playerData.Hp = 0;
        }
        UI.instance.UpdatePlayerUI(playerData);
    }
}
