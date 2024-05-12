using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.TextCore.Text;

public class Spawn : MonoBehaviour
{
    public GameObject playerMainPrefab;
    public GameObject playerChildPrefab;
    public GameObject monsterPrefab;
    // [SerializeField] private GameObject test;
    public GameObject obstaclePrefab;
    //  [SerializeField] private GameObject[] objectSpawn;
    [Space]
    [Header("Debug")]
    [SerializeField] int row;
    [SerializeField] int colum;
    [Header("SetPosition")]
    [SerializeField] int minX;
    [SerializeField] int minY;
    [SerializeField] int maxX;
    [SerializeField] int maxY;
    [SerializeField] private Transform startPos;
    [SerializeField] private Transform parentPlayer;
    [SerializeField] private Transform parentMonster;
    public bool isSpwan = false;
    // private GameObject gg;
    [SerializeField] private List<Vector2> old;
    [SerializeField] private List<Vector2> setPostionSpawn;

    [ContextMenu("DebugPositionArea")]
    public void Area()
    {
        Vector2 vec = new Vector2();
        setPostionSpawn = new List<Vector2>();
        for (int i = 0; i < row; i++)
        {
            var xPod = startPos.position.x + i;
            for (int j = 0; j < colum; j++)
            {
                var YPod = startPos.position.y - j;
                vec = new Vector2(xPod, YPod);
                //GameObject game = Instantiate(Resources.Load<GameObject>("Prefab/Floor"), vec, Quaternion.identity);
                setPostionSpawn.Add(vec);

                Debug.Log(vec);
            }
        }
        ShuffleList(setPostionSpawn);
    }
    private void Start()
    {
        //SpawnFloor();
        //  Area();
        StartSpawn();

    }
    private void Awake()
    {
    }
    private Vector2 RandomXY()
    {
        float x = Random.Range(minX, maxX);
        float y = Random.Range(minY, maxY);

        return new Vector2(x, y);
    }
    private void StartSpawn()
    {
        //ShuffleList(setPostionSpawn);
        GameObject main = Instantiate(playerMainPrefab, Vector2.zero, Quaternion.identity);
        main.transform.SetParent(parentPlayer);

        //StartCoroutine(SpawnObjectChildPlayer(GameMananger.instance.statInfo.startNumberPlayerChild, 1.5f));
        //StartCoroutine(FirstMonster(2f, .5f));
        //StartCoroutine(WaitToSpawnObstacle(2.5f, GameMananger.instance.statInfo.startNumberObstacle, obstaclePrefab, false));

        for (int i = 0; i < GameMananger.instance.statInfo.startNumberPlayerChild; i++)
        {
            GameObject playerAnother = Instantiate(playerChildPrefab, setPostionSpawn[i], Quaternion.identity);
            playerAnother.name = playerChildPrefab.name + "_" + i;
            PlayerManager.instance.heroNotInTeam.Add(playerAnother.transform);
        }
        int plusPlayer = GameMananger.instance.statInfo.startNumberPlayerChild;
        for (int j = 0; j < GameMananger.instance.statInfo.startNumberMonster; j++)
        {
            //Instantiate(monsterPrefab, setPostionSpawn[j + plusPlayer], Quaternion.identity);
            GameObject monsAnother = Instantiate(monsterPrefab, setPostionSpawn[j + plusPlayer], Quaternion.identity);
            monsAnother.name = monsterPrefab.name + "_" + j;
            MonsterManager.instance.monsList.Add(monsAnother.transform);
            MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);
            Debug.Log("Spwan_New_" + monsAnother.name);

        }
        int plusPlayerPM = GameMananger.instance.statInfo.startNumberPlayerChild + GameMananger.instance.statInfo.startNumberMonster;
        for (int i = 0; i < GameMananger.instance.statInfo.startNumberObstacle; i++)
        {
            GameObject monsAnother = Instantiate(obstaclePrefab, setPostionSpawn[i + plusPlayerPM], Quaternion.identity);
            monsAnother.name = obstaclePrefab.name + "_" + i;
            MonsterManager.instance.monsList.Add(monsAnother.transform);
            MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);
            Debug.Log("Spwan_New_" + monsAnother.name);
        }
        //  can move
        PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.MOVE;
        Debug.Log("All_Character_Spawn");
    }
    public void SpwanNewCharacterOrObject(int whichNumberCharOrOb, GameObject prefab, bool isSpwanPlayer)
    {
        //int mix = MonsterManager.instance
        //    .monsList.Count+PlayerManager.instance.heroList.c;
        for (int i = 0; i < whichNumberCharOrOb; i++)
        {
            for (int j = 0; j < MonsterManager.instance.monsPosition.Count; j++)
            {
                for (int k = 0; k < PlayerManager.instance.heroPosition.Count; k++)
                {
                    for (int m = 0; m < PlayerManager.instance.heroNotInTeam.Count; m++)
                    {
                        do
                        {
                            RandomXY();
                        }
                        while (old.Contains(RandomXY()) && old.Contains(PlayerManager.instance.heroPosition[k]) && old.Contains(MonsterManager.instance.monsPosition[j]) && old.Contains(PlayerManager.instance.heroNotInTeam[m].position));
                        old.Add(RandomXY());
                    }
                }
            }
            if (isSpwanPlayer)
            {
                GameObject playerAnother = Instantiate(prefab, RandomXY(), Quaternion.identity);
                playerAnother.name = prefab.name + "_" + i;
                PlayerManager.instance.heroNotInTeam.Add(playerAnother.transform);
                Debug.Log("Spwan_New_" + playerAnother.name);

            }
            else
            {
                GameObject monsAnother = Instantiate(prefab, RandomXY(), Quaternion.identity);
                monsAnother.name = prefab.name + "_" + i;
                MonsterManager.instance.monsList.Add(monsAnother.transform);
                MonsterManager.instance.monsPosition.Add(monsAnother.transform.position);
                Debug.Log("Spwan_New_" + monsAnother.name);
            }
        }

        old = new List<Vector2>();

    }
    //IEnumerator SpawnObjectChildPlayer(int amount, float time)
    //{
    //    yield return new WaitForSeconds(time);
    //    int _amount = amount;
    //    Debug.Log(_amount);
    //    int indexPlayerPosition = PlayerManager.instance.heroPosition.Count();
    //    for (int i = 0; i < _amount; i++)
    //    {
    //        for (int j = 0; j < indexPlayerPosition; j++)
    //        {
    //            do
    //            {
    //                RandomXY();
    //            }
    //            while (old.Contains(RandomXY()) && old.Contains(PlayerManager.instance.heroPosition[j]));
    //            old.Add(RandomXY());
    //        }
    //        Instantiate(playerChildPrefab, RandomXY(), Quaternion.identity);

    //    }

    //    // isSpwan = false;
    //    old = new List<Vector2>();
    //    Debug.Log("Spwan_New_Friend");
    //}

    //IEnumerator FirstMonster(float time, float waitSpwanAgin)
    //{
    //    yield return new WaitForSeconds(time);
    //    for (int i = 0; i < PlayerManager.instance.heroPosition.Count; i++)
    //    {
    //        do
    //        {
    //            RandomXY();
    //        }
    //        while (old.Contains(RandomXY()) && old.Contains(PlayerManager.instance.heroPosition[i]));
    //        old.Add(RandomXY());
    //    }

    //    GameObject mons = Instantiate(monsterPrefab, RandomXY(), Quaternion.identity);
    //    MonsterManager.instance.monsList.Add(mons.transform);
    //    MonsterManager.instance.monsPosition.Add(mons.transform.position);
    //    Debug.Log("Spawn_First_Monster");
    //    // yield return new WaitForSeconds(waitSpwanAgin);
    //    if (GameMananger.instance.statInfo.startNumberMonster > 1)
    //    {
    //        SpwanNewCharacterOrObject(GameMananger.instance.statInfo.startNumberMonster - 1, monsterPrefab, false);
    //    }
    //    old = new List<Vector2>();



    //}

   
    //IEnumerator WaitToSpawnObstacle(float time, int whichNumberMonsterOrOb, GameObject prefab, bool isSpwanPlayer)
    //{
    //    yield return new WaitForSeconds(time);
    //    SpwanNewCharacterOrObject(whichNumberMonsterOrOb, prefab, isSpwanPlayer);
    //    yield return new WaitForSeconds(0.1f);
    //    //can move
    //    PlayerManager.instance.currentPlayerStage = PlayerManager.playerStage.MOVE;
    //    Debug.Log("All_Character_Spawn");

    //}

    private void ShuffleList(List<Vector2> t)
    {
        for (int i = 0; i < t.Count - 1; i++)
        {
            var temp = t[i];
            int rand = Random.Range(i, t.Count);
            t[i] = t[rand];
            t[rand] = temp;
        }

       // Debug.Log("<color=yellow>ShuffleList</color>");
    }


    // Update is called once per frame
    void Update()
    {
        //won
        if (Input.GetKeyDown(KeyCode.H))
        {
            //Instantiate(objectSpawn[SpawnRandom()], transform.position, Quaternion.identity);
            //Debug.Log(SpawnRandom()); 
            //   SpawnObject(5);
        }


    }

}
