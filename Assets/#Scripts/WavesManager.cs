using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.UI;

public class WavesManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static WavesManager instance;
    public GameObject currentWave;
   
    public string wavesKey = "wave";
    public Transform spawnWavePoint,waveSlideToPoint,playerStartPoint;
    public Text loadingText;
    AsyncOperationHandle preloadOp;


    private void Awake()
    {
        instance = this;
    }
    WaitForSeconds w = new WaitForSeconds(0.3f);

    IEnumerator Start()
    {
        // ADDRESSABLES UPDATES
        loadingText.text = string.Format("Loading: {0}%", 0);
        preloadOp = Addressables.DownloadDependenciesAsync("preload");
        Addressables.InstantiateAsync("Done_Player", playerStartPoint);
        while (true)
        {

            yield return w;
            if(GamePlayManager.instance.gameIsRunning)
            if (!currentWave)
            {
                    if (PlayerStatsManager.instance.PlayerStats.currentWaveIndex < GamePlayManager.instance.gamePlayCoords.lastWaveIndex)
                        Addressables.InstantiateAsync(wavesKey + PlayerStatsManager.instance.PlayerStats.currentWaveIndex, spawnWavePoint).Completed += WavesManager_Completed;
                    else
                        PlayerStatsManager.instance.Win();
                }

        }
    }

    private void WavesManager_Completed(UnityEngine.ResourceManagement.AsyncOperations.AsyncOperationHandle<GameObject> obj)
    {
        if(!obj.IsDone)
        {
            GamePlayManager.instance.Victory();
            return;
        }
        obj.Result.transform.localPosition = Vector3.zero;
        obj.Result.transform.eulerAngles = Vector3.zero;
        
      
        currentWave = obj.Result;
    }

    // Update is called once per frame
    void Update()
    {
        if (preloadOp.IsValid())
        {
            loadingText.text = string.Format("Loading: {0}%", (int)(preloadOp.PercentComplete * 100));
            if (preloadOp.PercentComplete == 1)
            {
                Addressables.Release(preloadOp);
                preloadOp = new AsyncOperationHandle();
                loadingText.text = "";
            }
        }
      
    }
}
