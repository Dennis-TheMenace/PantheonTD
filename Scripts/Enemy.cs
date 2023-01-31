using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public int health;
    public float movementSpeed;
    public bool active = false;
    public Sprite newSprite;
    public SpriteRenderer spriteRenderer;
    public Sprite bossSprite;
    [SerializeField]
    private Spline spline;
    private bool once = true;
    private int startingHealth = 1;
    public GameObject playerManager;

    public bool slowed = false;

    // Start is called before the first frame update
    void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
        //health = 10;
        //movementSpeed = .1f; //This might not go past 2 definatley not 5
    }

    // Update is called once per frame
    void Update()
    {
        if(active == true)
        {
            if(once)
            {
                this.GetComponent<EnemyMovement>().pointNumber = 0;
                this.GetComponent<EnemyMovement>().PassParameters(spline.points, spline.pointCount);
                once = false;
            }
        }
        else
        {
            this.transform.position = new Vector3(1000,0,0);
            health = startingHealth;
            once = true;
        }

        if (health <= 0)
        {
            active = false;
        } 
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        if (health <= 0)
        {
            Debug.Log("Enemy destroyed");
            playerManager.GetComponent<PlayerResources>().GainCoin(1);
            active = false;
        }
    }

    public void PassParameters(int health, float movementSpeed, int getSprite)
    {
        this.health = health;
        this.startingHealth = health;
        this.movementSpeed = movementSpeed;
        if (getSprite == 1)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = newSprite;
        } else if (getSprite == 2)
        {
            spriteRenderer = gameObject.GetComponent<SpriteRenderer>();
            spriteRenderer.sprite = bossSprite;
        }
    }
    public void Slow()
    {
        slowed = true;
        movementSpeed = movementSpeed / 2;
    }
}
