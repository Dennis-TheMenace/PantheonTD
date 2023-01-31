using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerController : MonoBehaviour
{
    private float upgradeGain = 0.25f;
    public int towerPrice = 10;
    public float towerRange = 3.0f;
    public float bulletSpeed = 3.0f;
    public float towerCooldown = 1.0f;
    public float coolDownTimer;
    public int towerDamage = 1;
    public int level = 1;
    private bool onCooldown = false;
    private float enemyAngle;
    public GameObject nearestEnemy;
    public GameObject bulletPrefab;
    private float prevDist = float.MaxValue;
    private float curDist;
    private Vector3 targetEnemy;
    GameObject[] enemies;
    private bool targetingEnemy = false;
    private int enemyIndex;
    public bool canAttack = true;
    public bool canPlace = true;
    public bool slowTower = false;
    public bool aoeTower = false;
    public bool curTower = false;
    private bool placed = false;
    public BulletManager bManager;
    public List<GameObject> BulletArray;
    public GameObject playerManager;

    void Start()
    {
        BulletArray = bManager.getBulletList();
        playerManager = GameObject.Find("PlayerManager");
    }

    // Update is called once per frame
    void Update()
    {
        if (curTower && placed)
        {
            // Use attack cooldowns for currency generation
            if (onCooldown == false)
            {
                coolDownTimer = towerCooldown * 10;
                onCooldown = true;
                Debug.Log("Currency generated");
                playerManager.GetComponent<PlayerResources>().GainCoin(towerDamage);
            }
            else if (onCooldown)
            {
                // decrement cooldown
                coolDownTimer -= Time.deltaTime;
                if (coolDownTimer <= 0.0f)
                {
                    onCooldown = false;
                }
            }

            if (canAttack != false)
            {
                canAttack = false;
            }
        }
        if(canAttack)
        {
            //if (Input.GetKeyDown(KeyCode.U))
            //{
            //    slowTower = !slowTower;
            //}
            if (targetingEnemy == false || nearestEnemy == null)
            {
                if (!slowTower)
                {
                    FindEnemy();
                }
                else
                {
                    FindEnemySlow();
                }
            }

            if (targetingEnemy && nearestEnemy != null)
            {
                targetEnemy = nearestEnemy.transform.position - this.transform.position;
                targetEnemy.Normalize();

                float checkDist = Vector3.Distance(nearestEnemy.transform.position, this.transform.position);
                if (checkDist > towerRange)
                {
                    targetingEnemy = false;
                    nearestEnemy = null;
                }
            }

            // If able to, fire at enemy before tower goes through cooldown
            if (onCooldown == false && targetingEnemy && nearestEnemy != null)
            {
                /*for(int i = 0; i < BulletArray.Count; i++)
                {
                    if(BulletArray[i].active = false)
                    {
                        BulletArray[i].active = true;
                        BulletArray[i].GetComponent<GameObject>().transform.position = this.transform.position;
                        BulletArray[i].GetComponent<Bullet>().SetUpBullet(targetEnemy, towerDamage, bulletSpeed, slowTower);
                        if (aoeTower)
                        {
                            for(int j = 0; j < BulletArray.Count; j++)
                            {
                                if(BulletArray[i].active = false)
                                {
                                    BulletArray[i].active = true;
                                    BulletArray[i].GetComponent<GameObject>().transform.position = this.transform.position;
                                    BulletArray[i].GetComponent<Bullet>().SetUpBullet(new Vector3(-1 * targetEnemy.x, targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                                    BulletArray[i+1].active = true;
                                    BulletArray[i+1].GetComponent<GameObject>().transform.position = this.transform.position;
                                    BulletArray[i+1].GetComponent<Bullet>().SetUpBullet(new Vector3(targetEnemy.x, -1 * targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                                    BulletArray[i+2].active = true;
                                    BulletArray[i+2].GetComponent<GameObject>().transform.position = this.transform.position;
                                    BulletArray[i+2].GetComponent<Bullet>().SetUpBullet(new Vector3(-1 * targetEnemy.x, -1 * targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                                }
                            }
                        }
                        targetingEnemy = false;
                        nearestEnemy = null;
                        coolDownTimer = towerCooldown;
                        onCooldown = true;
                        return;
                    }
                }*/
                // Fire bullet at enemy
                GameObject temp = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                temp.GetComponent<Bullet>().SetUpBullet(targetEnemy, towerDamage, bulletSpeed, slowTower);
                if (aoeTower)
                {
                    GameObject temp2 = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    temp2.GetComponent<Bullet>().SetUpBullet(new Vector3(-1 * targetEnemy.x, targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                    GameObject temp3 = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    temp3.GetComponent<Bullet>().SetUpBullet(new Vector3(targetEnemy.x, -1 * targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                    GameObject temp4 = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
                    temp4.GetComponent<Bullet>().SetUpBullet(new Vector3(-1 * targetEnemy.x, -1 * targetEnemy.y, 0), towerDamage, bulletSpeed, slowTower);
                }
                // Will need to be adjusted as enemies have more than 1 health
                targetingEnemy = false;
                nearestEnemy = null;
                coolDownTimer = towerCooldown;
                onCooldown = true;
            }
        }

        // If able to, fire at enemy before tower goes through cooldown

        if (onCooldown == false && targetingEnemy && nearestEnemy != null)
        {
            // Fire bullet at enemy
            for(int i = 0; i < BulletArray.Count; i++)
            {
                if(BulletArray[i].active = false)
                {
                    BulletArray[i].active = true;
                    BulletArray[i].GetComponent<GameObject>().transform.position = this.transform.position;
                    BulletArray[i].GetComponent<Bullet>().SetUpBullet(targetEnemy, towerDamage, bulletSpeed, slowTower);
                    targetingEnemy = false;
                    nearestEnemy = null;
                    coolDownTimer = towerCooldown;
                    onCooldown = true;
                    return;
                }
            }

            /*GameObject temp = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            temp.GetComponent<Bullet>().SetUpBullet(targetEnemy, towerDamage, bulletSpeed, slowTower);
            // Will need to be adjusted as enemies have more than 1 health
            targetingEnemy = false;
            nearestEnemy = null;
            coolDownTimer = towerCooldown;
            onCooldown = true;*/
        }
        else if (onCooldown)
        {
            // decrement cooldown
            coolDownTimer -= Time.deltaTime;
            if (coolDownTimer <= 0.0f)
            {
                onCooldown = false;
            }
        }
    }

    public void FindEnemy()
    {
        // Grab all enemies in scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        prevDist = float.MaxValue;
        enemyIndex = -1;
        // Loop through enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            // Calculate distance between tower and current enemy
            curDist = Vector3.Distance(enemies[i].transform.position, this.transform.position);
            // If the distance between the enemy and player is in range and less than the previous distance
            if (curDist < towerRange && curDist < prevDist)
            {
                // The enemy we want to target is at the current index
                enemyIndex = i;
                // Set the previous distance to the current distance
                prevDist = curDist;
            }
        }
        if (enemyIndex != -1)
        {
            nearestEnemy = enemies[enemyIndex];
            targetingEnemy = true;
        }
        else
        {
            nearestEnemy = null;
            targetingEnemy = false;
        }

    }

    //make unplaceable if touching another collider
    private void OnTriggerStay2D(Collider2D collision)
    {
        canPlace = false;
    }

    //make placeable upon leaving collider
    private void OnTriggerExit2D(Collider2D collision)
    {
        canPlace = true;
    }
    
    public void FindEnemySlow()
    {
        // Grab all enemies in scene
        enemies = GameObject.FindGameObjectsWithTag("Enemy");
        prevDist = float.MaxValue;
        enemyIndex = -1;
        // Loop through enemies
        for (int i = 0; i < enemies.Length; i++)
        {
            if (enemies[i].gameObject.GetComponent<Enemy>().slowed == false)
            {
                // Calculate distance between tower and current enemy
                curDist = Vector3.Distance(enemies[i].transform.position, this.transform.position);
                // If the distance between the enemy and player is in range and less than the previous distance
                if (curDist < towerRange && curDist < prevDist)
                {
                    // The enemy we want to target is at the current index
                    enemyIndex = i;
                    // Set the previous distance to the current distance
                    prevDist = curDist;
                }
            }
        }
        if (enemyIndex != -1)
        {
            nearestEnemy = enemies[enemyIndex];
            targetingEnemy = true;
        }
        else
        {
            nearestEnemy = null;
            targetingEnemy = false;
        }

    }

    public void Upgrade()
    {
        if (level <= 19)
        {
            level++;
            if (level % 4 == 0)
            {
                towerDamage++;
                if ((towerCooldown - (upgradeGain / 5f)) > 0.001f)
                {
                    towerCooldown -= (upgradeGain / 5f);
                }
            }
            towerRange += (upgradeGain / 2f);
            bulletSpeed += (upgradeGain / 2f);
        }
    }

    public void Place()
    {
        placed = true;
    }    
}
