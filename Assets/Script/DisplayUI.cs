using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayUI : MonoBehaviour
{
    [Header("Odd_OR_even")]
    public GameObject oddOrEvenHead;
    public GameObject uiOddEven;
    public GameObject result;
    public TextMeshProUGUI tellPlayer;
    public TextMeshProUGUI numberText;
    [Header("BattleUI")]
    public GameObject playerBattleUI;
    public TextMeshProUGUI playerHealthText;
    public TextMeshProUGUI playerAttackText;
    public GameObject playerArrowGroup;
    [Space]
    public GameObject monsBattleUI;
    public TextMeshProUGUI monsterHealthText;
    public TextMeshProUGUI monsterAttackText;
    public GameObject monsArrowGroup;


    // Start is called before the first frame update
    void Start()
    {
        oddOrEvenHead.SetActive(false);
        result.SetActive(false);
        monsBattleUI.SetActive(false);
        playerBattleUI.SetActive(false);
    }

    // Update is called once per frame
 
}
