using System;
using System.Collections.Generic;
using Character;
using DG.Tweening;
using LevelGenerate;
using ObjectPool;
using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Serialization;
using UnityEngine.UI;

public enum GameState
{
    Level1,
    Level2,
    Level3
}

public enum PlayerState
{
    Jump,
    EnterSecretRoom,
    ExitSecretRoom,
    Dead
}

public class GameManager : MonoBehaviour
{
    public static GameManager _instance;

    public Player player;
    
    [Header("Player Element")]
    public GameObject heartPrefab;
    public Transform heartContainer;
    public List<Image> heartImage;

    #region -Score-

    [Header("Player Score")] 
    public int currentPlayerScore;
    public int highScore;
    
    [Header("Score Text")]
    public TextMeshProUGUI currentScoreText;
    public TextMeshProUGUI highScoreText;

    #endregion

    public GameObject gameOverPanel;

    [Header("Game State")]
    public GameState currentGameState = GameState.Level1;

    #region -Tiles-

    [Space]
    [Header("Normal Generate")]
    public NormalGenerate normalGenerate;
    [Header("Secret Room Generate")]
    public SecretRoomGenerate secretRoomGenerate;

    [Header("Move tile")]
    [SerializeField] private MoveNormalTile normalTileMove;
    [SerializeField] private MoveSecretTile secretTileMove;

    #endregion

    [SerializeField] private Lava lava;

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
    private void Start()
    {
        highScore = LoadHighScore();
        InitialHeart(player.MaxHealth);

        normalGenerate._Player = player;
        secretRoomGenerate._Player = player;
        
        normalGenerate._Dictionary = normalGenerate.TileStage.ToDictionary();
        
        for (var i = 0; i <= normalGenerate.RetainStep; i++)
            normalGenerate.GenerateTile(i, true);
    }

    #region -Tile Functions-

    public void  GenerateTileByStep()
    {
        if(!player.PlayerCheckGround()) return;
        if(player.CurrentRoom == RoomState.SecretRoom) return;
        var step = normalGenerate.RetainStep;
        normalGenerate.GenerateTile(++step, false);
    }
    
    public void CheckMoveGroundTile()
    {
        normalTileMove.MoveTileState(player.CurrentRoom);
        secretTileMove.MoveTileState(player.CurrentRoom);
    }

    public void SetTile(bool normalTile, bool secretRoom)
    {
        normalTileMove.transform.parent.gameObject.SetActive(normalTile);
        secretTileMove.transform.gameObject.SetActive(secretRoom);
        
        if(player.CurrentRoom != RoomState.NormalRoom) return;
        ReleaseSecretRoom();
        lava.ResetLava();
    }

    public void ReleaseSecretRoom()
    {
        if(player.CurrentRoom == RoomState.SecretRoom) return;
        foreach (Transform child in secretTileMove.transform)
        {
            PoolManager.ReleaseObject(child.gameObject);
        }
    }

    #endregion

    #region -Game State-

    public void OnGameOver()
    {
        SoundManager.Instance.musicSource.Stop();
        gameOverPanel.SetActive(true);
        CheckForHighScore();
        UpdateHighScore();
        DOTween.KillAll();
    }

    public void LoadScene(string sceneName)
    {
        SoundManager.Instance.musicSource.Play();
        SceneManager.LoadScene(sceneName);
    }

    private void ChangeState(GameState newState)
    {
        currentGameState = newState;
    }

    private void CheckChangeStateByScore()
    {
        switch (currentPlayerScore)
        {
            case >= 100 and < 200:
                ChangeState(GameState.Level2);
                break;
            case >= 200:
                ChangeState(GameState.Level3);
                break;
            default:
                ChangeState(GameState.Level1);
                break;
        }
    }

    #endregion

    #region *Score*

    public void UpdatePlayerScore(int value)
    {
        currentPlayerScore += value;
        currentScoreText.text = currentPlayerScore.ToString();
        CheckChangeStateByScore();
    }

    private void UpdateHighScore()
    {
        highScoreText.text = $"Best Run: {LoadHighScore()}";
    }

    private void CheckForHighScore()
    {
        var currentHighScore = PlayerPrefs.GetInt("HighScore", 0);
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
