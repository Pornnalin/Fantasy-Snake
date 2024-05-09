using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameMananger : MonoBehaviour
{
    [Range(1, 15)]
    public int minAttack, maxAttack;
    [Range(1, 10)]
    public int minHeart, maxHeart;
    public float growing;
    public float chancePlayer, chanceMonster;
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
