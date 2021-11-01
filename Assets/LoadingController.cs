using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LoadingController : MonoBehaviour
{
    // Start is called before the first frame update
    AsyncOperationHandle preloadOp;
    public Text loadingText;
    public string nextSceneAddress="MainMenu";
     IEnumerator Start()
    {

        AsyncOperationHandle<IResourceLocator> handle = Addressables.InitializeAsync();
        yield return handle;
        loadingText.text = string.Format("Loading: {0}%", 0);
        preloadOp = Addressables.DownloadDependenciesAsync("preload");
        while (!preloadOp.IsValid()) yield return null;
        while (preloadOp.IsValid())
        {
            loadingText.text = string.Format("Loading: {0}%", (int)(preloadOp.PercentComplete * 100));
            if (preloadOp.PercentComplete == 1)
            {
                Addressables.Release(preloadOp);
                preloadOp = new AsyncOperationHandle();
               
                break;
            }
            yield return null;
        }
        AsyncOperationHandle<GameObject> loadOp = Addressables.LoadAssetAsync<GameObject>("wave0");
        yield return loadOp;
        AsyncOperationHandle<Scene> loadOp2 = Addressables.LoadAssetAsync<Scene>("SpaceShooter");
        yield return loadOp2;
        loadOp2 = Addressables.LoadAssetAsync<Scene>(nextSceneAddress);
        yield return loadOp2;
        loadingText.text = "";
        Addressables.LoadSceneAsync(nextSceneAddress);
    }


    // ADDRESSABLES UPDATES


    //LoadHazards();

    // Update is called once per frame

 
}
