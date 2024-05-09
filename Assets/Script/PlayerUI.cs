using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayerUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI healthText, attackText;
    [SerializeField] private PlayerController playerController;
    // Start is called before the first frame update
    void Start()
    {
        playerController = GetComponent<PlayerController>();
    }

    // Update is called once per frame
    void Update()
    {
        DisplayText();
    }

    private void DisplayText()
    {
        healthText.text = playerController.health.ToString();
        attackText.text = playerController.attack.ToString();
    }
}
