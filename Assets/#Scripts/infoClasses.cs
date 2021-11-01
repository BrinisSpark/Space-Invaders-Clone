using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

[System.Serializable]
public class PlayerStats
{
    public string id="brinis";
    public int totalScore;
    public int currentWaveScore;
    public int topWave;
    public int currentWaveIndex;
    public int waveIndexWhenClickedStartGame;
    public int playerLives=7;
    public List<WaveScoreAchievementCoords> allHistory = new List<WaveScoreAchievementCoords>();
    
}
public class WaveJson
{
    // this will make each unit as Addressablr instead of the whole wave
    public List<AdressableCoords> allAssets = new List<AdressableCoords>();
}
public class AdressableCoords
{
    public AssetReference assetRef;
    public Vector3 LocalPoition;
    public Vector3 LocalRotation;
}
[System.Serializable]
public class WaveScoreAchievementCoords
{
    public int waveIndex;
    public int score;
    public System.DateTime date;
}
