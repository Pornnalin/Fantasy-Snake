using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMananger : MonoBehaviour
{
    public StatInfo statInfo;
    [Header("GamePad")]
    public bool isGamePadDetect;
    [Space]
    [Header("Switch")]
    public bool isSwtichF_To_S = false;
    public bool isSwtichL_To_F = false;
    public bool canPress = true;

    // Start is called before the first frame update
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
    [Range(1, 25), Tooltip("min =1,max=25")]
    public int minPlayerAttack, maxPlayerAttack, minMonsterAttack, maxMonsterrAttack;
    [Space]
    [Range(1, 10), Tooltip("min =1,max=10")]
    public int minPlayerHeart, maxPlayerHeart, minMonsterHeart, maxMonsterHeart;
    [Space]
    public float growing;
    [Space]
    public float chancePlayer, chanceMonster;
}
