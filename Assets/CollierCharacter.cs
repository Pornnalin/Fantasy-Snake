using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollierCharacter : MonoBehaviour
{
    // [SerializeField] private Collider2D[] _collider2D;
    // Start is called before the first frame update
    [SerializeField] Rigidbody2D rb2D;
    [SerializeField] private Transform up;
    [SerializeField] private Transform down;
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;
    public bool isHit = false;
    public bool isCanRepalce = false;
    public Spawn spawn;
    void Start()
    {
        rb2D = GetComponent<Rigidbody2D>();
        spawn = FindAnyObjectByType<Spawn>();
    }

    // Update is called once per frame
    void Update()
    {
        if (!isHit)
        {
            StartCoroutine(wait());
            isHit = true;
        }
        else
        {
            Destroy(this.gameObject);
        }
    }
    void FixedUpdate()
    {

        RaycastHit2D hitU = Physics2D.Raycast(up.position, Vector2.up, .5f);
        RaycastHit2D hitD = Physics2D.Raycast(down.position, Vector2.down, .5f);
        RaycastHit2D hitL = Physics2D.Raycast(left.position, Vector2.left, .5f);
        RaycastHit2D hitR = Physics2D.Raycast(right.position, Vector2.right, .5f);


        if (hitU.collider == null)
        {
            Debug.Log("notHit");
        }
        else
        {
            isHit = true;
          //  Destroy(this.gameObject);
            Debug.Log("hit" + hitU.collider.name);
        }

        if (hitD.collider == null)
        {
            Debug.Log("notHit");

        }
        else
        {
            isHit = true;
          //  Destroy(this.gameObject);
            Debug.Log("hit" + hitD.collider.name);

        }
        if (hitL.collider == null)
        {
            Debug.Log("notHit");

        }
        else
        {
            isHit = true;
           // Destroy(this.gameObject);
            Debug.Log("hit" + hitL.collider.name);

        }
        if (hitR.collider == null)
        {
            Debug.Log("notHit");

        }
        else
        {

            isHit = true;
           // Destroy(this.gameObject);
            Debug.Log("hit" + hitR.collider.name);

        }

        //Method to draw the ray in scene for debug purpose
        Debug.DrawRay(up.position, Vector2.up, Color.red);
        Debug.DrawRay(down.position, Vector2.down, Color.red);
        Debug.DrawRay(right.position, Vector2.right, Color.red);
        Debug.DrawRay(left.position, Vector2.left, Color.red);

    }

    IEnumerator wait()
    {       
       // spawn.ReplaceCharacter();
       // isHit = true;
        yield return new WaitForSeconds(0.5f);
      //  Destroy(this.gameObject);

    }
}

