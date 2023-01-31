using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerManager : MonoBehaviour
{
    private bool holdingTower;
    GameObject myTower;
    private bool changedColor;
    public GameObject pManager;
    public List<GameObject> towers = new List<GameObject>();
    public GameObject rangeCircle;

    // Start is called before the first frame update
    void Start()
    {
        pManager = GameObject.Find("PlayerManager");
        rangeCircle.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if(holdingTower == true)
        {
            rangeCircle.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
            //updates tower position based on mouse
            myTower.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);

            //places tower if object isn't touching another // AND if you can afford it
            if (Input.GetMouseButtonDown(0) && myTower.GetComponent<TowerController>().canPlace && pManager.GetComponent<PlayerResources>().SpendCoin(myTower.GetComponent<TowerController>().towerPrice))
            {
                holdingTower = false;
                myTower.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
                myTower.GetComponent<TowerController>().canAttack = true;
                myTower.GetComponent<TowerController>().Place();
                towers.Add(myTower);
                rangeCircle.SetActive(false);
            }
            //changes color if target can't be placed
            else if(!myTower.GetComponent<TowerController>().canPlace)
            {
                myTower.GetComponent<SpriteRenderer>().color = Color.red;
                changedColor = true;
            }
            //resets color if target switches to being able to being placed
            else if(changedColor && myTower.GetComponent<TowerController>().canPlace)
            {
                changedColor = false;
                myTower.GetComponent<SpriteRenderer>().color = Color.white;
            }

            if (Input.GetMouseButtonDown(1))
            {
                rangeCircle.SetActive(false);
                holdingTower = false;
                Destroy(myTower);
            }
        }
    }

    //equips a tower if not already holding one
    public void GetTower(GameObject tower)
    {
        if (holdingTower == false)
        {
            rangeCircle.transform.localScale = new Vector3(tower.GetComponent<TowerController>().towerRange * 2, tower.GetComponent<TowerController>().towerRange * 2, tower.GetComponent<TowerController>().towerRange * 2);
            rangeCircle.SetActive(true);
            rangeCircle.transform.position = new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f);
            holdingTower = true;
            myTower = Instantiate(tower, new Vector3(Camera.main.ScreenToWorldPoint(Input.mousePosition).x, Camera.main.ScreenToWorldPoint(Input.mousePosition).y, 0.0f), Quaternion.identity);
            myTower.GetComponent<TowerController>().canAttack = false;
        }
        //else if (holdingTower)
        //{
        //    if (myTower != tower)
        //    {
        //        myTower = tower;
        //    }
        //}
        return;
    }

    public void UpgradeTowers()
    {
        bool maxLevel = true;
        for (int i = 0; i < towers.Count; i++)
        {
            if (towers[i].GetComponent<TowerController>().level < 20)
            {
                maxLevel = false;
            }
        }

        if (towers.Count > 0 && maxLevel == false)
        {
            if (pManager.GetComponent<PlayerResources>().SpendCoin(5))
            {
                for (int i = 0; i < towers.Count; i++)
                {
                    towers[i].GetComponent<TowerController>().Upgrade();
                }
            }
        }
    }
}
