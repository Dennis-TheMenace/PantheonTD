using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerResources : MonoBehaviour
{
    public Text textHolderHealth;
    public Text textHolderWave;
    public Text textHolderCoin;
    public Text textHolderSpeed;
    public int health = 100;
    public int coin = 10;
    public int wave = 1;
    private bool flashRed = false;
    private float redTimer = 0.0f;
    private float redTime = 1.25f;
    public bool spedUp = false;
    private Color myGreen;
    // Start is called before the first frame update

    public void Start()
    {
        textHolderSpeed.enabled = false;
        textHolderHealth.text = "Health: " + health;
        textHolderCoin.text = "Coin: " + coin;
        myGreen = textHolderCoin.color;
        textHolderWave.text = "Wave: " + wave;
    }

    public void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && spedUp == false && MainMenu.GameIsPaused == false)
        {
            textHolderSpeed.enabled = true;
            spedUp = true;
            Time.timeScale = 4.0f;
        }
        if (Input.GetKeyUp(KeyCode.Space) && spedUp == true && MainMenu.GameIsPaused == false)
        {
            spedUp = false;
            textHolderSpeed.enabled = false;
            Time.timeScale = 1.0f;
        }
        if (flashRed == true)
        {
            redTimer -= Time.deltaTime;
            if (redTimer <= 0)
            {
                textHolderCoin.color = myGreen;
                flashRed = false;
            }
        }
    }
    public void HealthBack(int healthBack)
    {
        health += healthBack;
        textHolderHealth.text = "Health: " + health;
    }

    public void TakeDamage(int healthLost)
    {
        health -= healthLost;
        textHolderHealth.text = "Health: " + health;
        if (health <= 0)
        {
            Time.timeScale = 1.0f;
            textHolderSpeed.enabled = false;
            SceneManager.LoadScene("GameOver");
        }
    }

    public void WaveIncreased()
    {
        GainCoin(wave * 5);
        wave++;
        textHolderWave.text = "Wave: " + wave;
    }

    public void GainCoin(int gainedCoin)
    {
        // Debug.Log("Currency generated");
        coin += gainedCoin;
        textHolderCoin.text = "Coin: " + coin;
    }

    public bool SpendCoin(int coinSpend)
    {
        if (coin - coinSpend >= 0)
        {
            coin -= coinSpend;
            textHolderCoin.text = "Coin: " + coin;
            return true;
        }
        if (flashRed == false)
        {
            redTimer = redTime;
            flashRed = true;
            textHolderCoin.color = Color.red;
        }
        return false;
    }
}
