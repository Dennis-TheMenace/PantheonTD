using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletManager : MonoBehaviour
{
    public List<GameObject> BulletArray = new List<GameObject>();
    public TowerManager tManager;
    [SerializeField]
    private GameObject bulletPrefab;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Check to see if array needs to expand/ add more enemies
        if(BulletArray.Count < tManager.towers.Count *4)
        {
            AddBullets();
        }
    }

    public void AddBullets()
    {
        for(int i = 0; i < 10; i++)
        {
            var bullet = Instantiate(bulletPrefab, this.transform.position, Quaternion.identity);
            BulletArray.Add(bullet);
        }
    }

    public List<GameObject> getBulletList()
    {
        return BulletArray;
    }
}
