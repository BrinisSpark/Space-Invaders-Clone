using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.UI;
using Newtonsoft.Json;
public class PlayerStatsManager : MonoBehaviour
{
    public static string playerStatskey = "playerStats";
    public static PlayerStatsManager instance;
    public Transform HUDPanel,resultScreen;
    public Text wavesPassedText;
    public AssetReference playerExplosion;

     [SerializeField]
    private PlayerStats playerStats = new PlayerStats();
    public PlayerStats PlayerStats
    {
        get {
            LoadStatsFromDisk();
            return playerStats; 
            }
        set
        {
            playerStats = value;
            SaveStats();
        }
    }
    private void Awake()
    {
        instance = this;
        LoadStatsFromDisk();
        playerStats.currentWaveScore = 0;

        if (GamePlayManager.instance.gamePlayCoords.playerLivesEachGameStart!=0)
        playerStats.playerLives = GamePlayManager.instance.gamePlayCoords.playerLivesEachGameStart;
        if (playerStats.playerLives <= 0) playerStats.playerLives = 1;

        playerStats.waveIndexWhenClickedStartGame = playerStats.currentWaveIndex;
        SaveStats();

    }
    public void LoadStatsFromDisk()
    {
        if (PlayerPrefs.HasKey(playerStatskey))
            playerStats = JsonConvert.DeserializeObject<PlayerStats>(PlayerPrefs.GetString(playerStatskey));
        brinis.EasyCrudsManager.SetTextAutomaticly<PlayerStats>(HUDPanel, playerStats);
    }
    public void SaveStats()
    {
        PlayerPrefs.SetString(playerStatskey, JsonConvert.SerializeObject(playerStats));
        brinis.EasyCrudsManager.SetTextAutomaticly<PlayerStats>(HUDPanel, playerStats);
     
    }
    public void AddScore(int score)
    {
        playerStats.currentWaveScore += score;
        playerStats.totalScore += score;
        SaveStats();
    }
    public void InitWaveStats()
    {
        WaveScoreAchievementCoords ws = new WaveScoreAchievementCoords();
        ws.score = playerStats.currentWaveScore;
        ws.date = System.DateTime.Now;
        ws.waveIndex = playerStats.currentWaveIndex;
        playerStats.allHistory.Add(ws);
        playerStats.currentWaveScore = 0;



        playerStats.currentWaveIndex++;
        SaveStats();
    }
    public void HitPlayer()
    {
         playerStats.playerLives--;
        SaveStats();
        if (playerStats.playerLives <= 0)
        Loose();
    }
    public void Loose()
    {
        GamePlayManager.instance.gameIsRunning = false;
        playerExplosion.InstantiateAsync(transform.position, transform.rotation);
        brinis.EasyCrudsManager.SetTextAutomaticly<PlayerStats>(resultScreen, playerStats);
        wavesPassedText.text = "Waves:" + playerStats.waveIndexWhenClickedStartGame + "->"
            + playerStats.currentWaveIndex;
        resultScreen.gameObject.SetActive(true);
        Addressables.ReleaseInstance(PlayerHealthController.instance.gameObject);
    }
    public void Win()
    {
        GamePlayManager.instance.gameIsRunning = false;
        
        brinis.EasyCrudsManager.SetTextAutomaticly<PlayerStats>(resultScreen, playerStats);
        wavesPassedText.text = "Waves:" + playerStats.waveIndexWhenClickedStartGame + "->"
            + playerStats.currentWaveIndex;
        resultScreen.gameObject.SetActive(true);
        //Addressables.ReleaseInstance(PlayerHealthController.instance.gameObject);
    }

}
