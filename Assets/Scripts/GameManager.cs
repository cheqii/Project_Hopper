using System;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Player player;
    
    [Header("Player Element")]
    public GameObject heartPrefab;
    public Transform heartContainer;
    public List<Image> heartImage;
    
    
    [Header("Player Score")] 
    public int currentPlayerScore;
    public int highScore;
    
    [Header("Score Text")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    public GameObject gameOverPanel;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(gameObject);
        }

        // DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        highScore = LoadHighScore();
        InitialHeart(player.MaxHealth);
    }

    public void OnGameOver()
    {
        gameOverPanel.SetActive(true);
        CheckForHighScore();
        UpdateHighScore();
        DOTween.KillAll();
    }

    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }

    #region *Score*

    public void UpdatePlayerScore(int value)
    {
        currentPlayerScore += value;
        currentScoreText.text = currentPlayerScore.ToString();
    }

    public void UpdateHighScore()
    {
        highScoreText.text = $"Best Run: {LoadHighScore()}";
    }

    public void CheckForHighScore()
    {
        int currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
        if(currentPlayerScore <=  currentHighScore) return;
        PlayerPrefs.SetInt("HighScore", currentPlayerScore);
        PlayerPrefs.Save();
    }

    private int LoadHighScore()
    {
        return PlayerPrefs.GetInt("HighScore", 0);
    }

    #endregion

    #region -Player Health-

    public void UpdatePlayerHealthUI(bool isIncreaseHealth)
    {
        var heartFill = player.Health;
        if (!isIncreaseHealth)
            heartImage[heartFill].color = Color.clear;
        else
            heartImage[heartFill - 1].color = Color.white;
    }

    private void InitialHeart(int value)
    {
        for (int i = 0; i < value; i++)
        {
            var health = Instantiate(heartPrefab, heartContainer);
            heartImage.Add(health.GetComponent<Image>());
        }
    }

    #endregion
}
