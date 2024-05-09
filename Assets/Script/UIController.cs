using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class UIController : MonoBehaviour
{
    [Header("Odd_OR_even")]
    public GameObject oddOrEvenHead;
    public GameObject uiOddEven;
    public GameObject result;
    public TextMeshProUGUI tellPlayer;
    public TextMeshProUGUI numberText;
    // Start is called before the first frame update
    void Start()
    {
        oddOrEvenHead.SetActive(false);
        result.SetActive(false);
    }

    // Update is called once per frame
 
}