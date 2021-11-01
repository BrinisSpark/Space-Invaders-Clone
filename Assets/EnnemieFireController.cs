using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class EnnemieFireController : MonoBehaviour
{
    // Start is called before the first frame update
    public Transform shotSpawn;
    public AssetReference shot;


    IEnumerator Start()
    {
        if (GamePlayManager.instance == null) yield break;
        yield return new WaitForSeconds(Random.Range(0, 10));
        WaitForSeconds w = new WaitForSeconds(1.0f/GamePlayManager.instance.gamePlayCoords.fireRate);
        while(true)
        {
            yield return w;
            shot.InstantiateAsync(shotSpawn.position, shotSpawn.rotation);
            yield return new WaitForSeconds(Random.Range(0, 1));
        }
    }
}
