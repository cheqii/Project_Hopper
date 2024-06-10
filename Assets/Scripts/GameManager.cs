using System;
using System.Collections.Generic;
using Character;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Player player;
    [Header("Player Score")]
    public int currentPlayerScore;
    public TextMeshProUGUI playerScoreText;

    public List<GameObject> HealthObjects;
    public TextMeshProUGUI playerHealthText;

    private void Awake()
    {
        if (_instance == null) _instance = this;
        else
        {
            Destroy(gameObject);
        }
        
        DontDestroyOnLoad(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerHealthText.text = player.Health.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    #region -Player Methods-

    #region *Score*

    public void UpdatePlayerScore(int value)
    {
        currentPlayerScore += value;
        playerScoreText.text = currentPlayerScore.ToString();
    }

    public void UpdateHighScore()
    {
        
    }

    #endregion

    #region *Health*

    public void UpdatePlayerHealth()
    {
        playerHealthText.text = player.Health.ToString();
        HealthObjects[player.Health].SetActive(false);
    }

    #endregion
    
    #endregion
}
