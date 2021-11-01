using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class SceneLoaderByEvent : MonoBehaviour
{
    // Start is called before the first frame update
    public string sceneAdressName;

        public void LoadScene()
    {
        Addressables.LoadSceneAsync(sceneAdressName);
    }
}
