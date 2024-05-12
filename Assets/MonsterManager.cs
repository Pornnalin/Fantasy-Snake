using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterManager : MonoBehaviour
{
    public static MonsterManager instance;
    public List<Vector2> monsPosition = new List<Vector2>();   
    public List<Transform> monsList = new List<Transform>();

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
    public void GetData()
    {
        
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
