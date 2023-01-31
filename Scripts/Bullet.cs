using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    // Start is called before the first frame update
    private int damage;
    private bool slow = false;
    public GameObject playerManager;
    public bool active = false;
    private bool generateCurrency = true;

    public void Start()
    {
        playerManager = GameObject.Find("PlayerManager");
    }
    public void SetUpBullet(Vector3 aim, int damage, float speed, bool slow)
    {
        generateCurrency = true;
        this.slow = slow;
        this.damage = damage;
        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(aim.x * speed, aim.y * speed);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            // playerManager.GetComponent<PlayerResources>().GainCoin(1);
            //Debug.Log("Destroy Bullet");
            active = false;
            this.transform.position = new Vector3(1000,0,0);
            gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(0,0);
            if (slow)
            {
                Debug.Log("slowed");
                other.GetComponent<Enemy>().Slow();
            }
            if (generateCurrency)
            {
                other.GetComponent<Enemy>().TakeDamage(damage);
            }
            generateCurrency = false;
        }
    }
}