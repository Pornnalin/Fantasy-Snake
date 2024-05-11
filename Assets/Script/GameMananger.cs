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
    public float chancePlayer, chanceMonster;
}
