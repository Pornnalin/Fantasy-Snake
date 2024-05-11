using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMananger : MonoBehaviour
{
    public static GameMananger instance;
    public StatInfo statInfo;
    [Header("GamePad")]
    public bool isGamePadDetect;
    [Space]
    [Header("Switch")]
    public bool isSwtichF_To_S = false;
    public bool isSwtichL_To_F = false;
    public bool canPress = true;
    [Space]
    [Header("Spawn")]
    public bool isPlayerSpwan;
    // Start is called before the first frame update
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        isGamePadDetect = Gamepad.current == null ? false : true;

    }
    [ContextMenu("testChance")]
    public void ChanceSpawn()
    {
        int a = Random.Range(0, 101);
        if (a <= statInfo.chancePlayer)
        {
            isPlayerSpwan = true;
        }
        else if (a >= statInfo.chancePlayer && a <= statInfo.chanceMonster)
        {
            isPlayerSpwan = false;

        }
        Debug.Log(a);
    }
    public int AmountSpawn()
    {
        int amount = new int();
        if (PlayerManager.instance.heroList.Count > 3)
        {
            amount = statInfo.anmountPlayerSpawanMin;
        }
        if (PlayerManager.instance.heroList.Count < 3)
        {
            amount = statInfo.anmountPlayerSpawanMin;
        }

        return amount;
    }
}
[System.Serializable]
public class StatInfo
{
    [Space]
    public int startNumberplayer = 1;
    [Space]
    [Range(1, 5)]
    public int startNumbermonster;
    [Space]
    [Range(1, 5)]
    public int startNumberobstacle;
    [Space]
    [Range(1, 25), Tooltip("min =1,max=25")]
    public int minPlayerAttack, maxPlayerAttack, minMonsterAttack, maxMonsterrAttack;
    [Space]
    [Range(1, 10), Tooltip("min =1,max=10")]
    public int minPlayerHeart, maxPlayerHeart, minMonsterHeart, maxMonsterHeart;
    [Space]
    [Range(0.1f, 2), Tooltip("min =0.1f,max=2")]
    public float growing;
    [Space]
    [Tooltip("chance/100%")]
    public float chancePlayer, chanceMonster;
    [Space]
    [Range(1, 5)]
    public int anmountPlayerSpawanMin, anmountPlayerSpawanMax, anmountMonsterSpawanMin, anmountMonsterSpawanMax;
}
