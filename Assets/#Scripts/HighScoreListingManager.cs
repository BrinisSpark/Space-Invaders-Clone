using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HighScoreListingManager : MonoBehaviour
{
    public Transform cellPrefab;
    public  PlayerStats playerStats;
    private IEnumerator Start()
    {
        cellPrefab.gameObject.SetActive(false);
        yield return null;
        if (!PlayerPrefs.HasKey(PlayerStatsManager.playerStatskey)) yield break;
        playerStats = Newtonsoft.Json.JsonConvert.DeserializeObject<PlayerStats>(PlayerPrefs.GetString(PlayerStatsManager.playerStatskey));
        playerStats.allHistory.Sort(SortByScore);
        int k = 0;
        
        foreach(WaveScoreAchievementCoords w in playerStats.allHistory)
        {
            Transform spawned = Instantiate(cellPrefab.gameObject).transform;
            spawned.transform.parent = cellPrefab.parent;
            spawned.localScale = cellPrefab.localScale;
            spawned.gameObject.SetActive(true);
            brinis.EasyCrudsManager.SetTextAutomaticly<WaveScoreAchievementCoords>(spawned, w);
            k++;
            if (k > 9)
            {
                break;
            }
        }
        //brinis.EasyCrudsManager.ShowAll<WaveScoreAchievementCoords>(cellPrefab, allScoreCoords);
    }
  
    static int SortByScore(WaveScoreAchievementCoords p1, WaveScoreAchievementCoords p2)
    {
        return p2.score.CompareTo(p1.score);
    }
}
