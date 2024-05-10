using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText, attackText;
    [SerializeField] private PlayerController playerController;
    public GameObject statGroup;
     public GameObject arrowGroup;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
        statGroup.SetActive(false);
        arrowGroup.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();
    }

    private void DisplayText()
    {
        healthText.text = playerController.playerProfile.health.ToString();
        attackText.text = playerController.playerProfile.attack.ToString();
    }
}
