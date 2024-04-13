using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public List<GameObject> targets;

    private float spawnRate = 1.0f;

    private int score;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI gameOverText;
    public Button restartButton;
    public bool isGameActive = true;

    public int health;
    public TextMeshProUGUI healthText;

    public GameObject titleScreen;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void StartGame()
    {
        isGameActive = true;

        score = 0;
        health = 3;
        StartCoroutine(SpawnTarget());
        UpdateScore(0);
        UpdateHealth(0);

        titleScreen.gameObject.SetActive(false);
    }

    void Update()
    {
        if (health <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        gameOverText.gameObject.SetActive(true);
        isGameActive = false;
        restartButton.gameObject.SetActive(true);
    }
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
    

    IEnumerator SpawnTarget()
    {
        while (isGameActive)
        {
            yield return new WaitForSeconds(spawnRate);
            int index = Random.Range(0, targets.Count);
            Instantiate(targets[index]);
        }
    }

    public void UpdateScore(int scoreToAdd)
    {
        score += scoreToAdd;
        scoreText.text = "Score: " + score;
    }

    public void UpdateHealth(int hpLoss)
    {
        health += hpLoss;
        healthText.text = "Health: " + health;
    }
}
