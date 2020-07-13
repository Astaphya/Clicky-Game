using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private float spawnRate = 1.0f;
    public List<GameObject> targets;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI bestScore;
    public TextMeshProUGUI gameOverText;
    public Button RestartButton;
    private int score;
    private int highScore;
    public bool isGameActive;
    public GameObject TitleScreen;
    // Start is called before the first frame update
    void Start()
    {
         highScore = PlayerPrefs.GetInt("highScore",0);
         bestScore.text = "" + highScore;
         
    }

    IEnumerator SpawnEnemy()
    {
        while(isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0,targets.Count);
            Instantiate(targets[index]);
        }
        
    }

    public void UpdateScore(int scoreToAdd)
    {   score += scoreToAdd;
        scoreText.text =  "Score: " +score;
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        RestartButton.gameObject.SetActive(true);
        isGameActive = false;
    }

    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void StartGame(int difficulty)
    {   
        isGameActive = true;
        score = 0;
        spawnRate /= difficulty;

        StartCoroutine(SpawnEnemy());
        UpdateScore(0);

        TitleScreen.gameObject.SetActive(false);
    }

    public void BestScore()
    {
        if(score > highScore)
        {
            PlayerPrefs.SetInt("highScore",score);
            bestScore.text = "" +score;
            Debug.Log("Score:" + score);
            PlayerPrefs.Save();
        }

    }

    // Update is called once per frame
    void Update()
    {
        BestScore();
    }
}
