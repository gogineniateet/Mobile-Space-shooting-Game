using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{

    
    //public Button playAgain;
    public GameObject gameOverPanel;

    public static GameManager instance;
    public static GameManager Instance
    {
        get
        {
            if (instance == null)
            {
                instance = GameObject.FindObjectOfType<GameManager>();
                if (instance == null)
                {
                    GameObject container = new GameObject("GameManager");
                    instance = container.AddComponent<GameManager>();
                }
            }
            return instance;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        gameOverPanel.SetActive(false);
        //playAgain.onClick.AddListener(PlayAgain);
    }

    public void GameOver()
    {
        if (ShipController.Instance.isWon == true)
        {
            Debug.Log("Game Won");
            gameOverPanel.SetActive(true);
        }
        if(ShipController.instance.isGameOver == true)
        {
            Debug.Log("Game Over");
            gameOverPanel.SetActive(true);
        }
            
    }
    private void PlayAgain()
    {
        SceneManager.LoadScene(1);
    }
   



}
