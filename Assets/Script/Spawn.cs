using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Spawn : MonoBehaviour
{
    [SerializeField] private GameObject playerPrefab;
    //  [SerializeField] private GameObject floorPrefab;
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private GameObject test;
    [SerializeField] private GameObject obstaclePrefab;
    [SerializeField] private List<Vector2> area = new List<Vector2>();
    [SerializeField] int x, y;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform parentFloor;
    public bool isSpwan = false;
    private GameObject gg;


    private void Start()
    {
        //SpawnFloor();
    }
    public void SpawnPlayer()
    {
        for (int i = 0; i < GameMananger.instance.AmountSpawn(); i++)
        {
            float x = Random.Range(-7, 9);
            float y = Random.Range(-8, 8);
            var game = Instantiate(playerPrefab, new Vector3(x, y, 0), Quaternion.identity);
            gg = game;
        }
        isSpwan = false;
    }

    //public void ReplaceCharacter()
    //{
    //    Instantiate(playerPrefab, gg.transform.position, Quaternion.identity);
    //}

    // Update is called once per frame
    void Update()
    {
        //won
        //if (Input.GetKeyDown(KeyCode.H))
        //{
        //    SpawnPlayer();

        //}


    }
}
