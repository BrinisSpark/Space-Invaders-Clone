using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class HealthController : MonoBehaviour
{
    public int health=1;
    public  AssetReference explosion;

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<LeftLimitDetector>())
        {
            transform.root.GetComponent<WaveController>().OnDirectionChanged(false);
            return;
        }
        if (other.GetComponent<RightLimitDetector>())
        {
            transform.root.GetComponent<WaveController>().OnDirectionChanged(true);
            return;
        }
        if (other.attachedRigidbody)
        if(other.attachedRigidbody.GetComponentInChildren<PlayerBulletDetector>())
        {
                //Done_GameController.instance.AddScore(GamePlayManager.instance.gamePlayCoords.scoreAddedPerEnnemie);

            if(explosion!=null)
            explosion.InstantiateAsync(transform.position, transform.rotation);

            Addressables.ReleaseInstance(other.attachedRigidbody.gameObject);
            PlayerStatsManager.instance.AddScore(GamePlayManager.instance.gamePlayCoords.scoreAddedPerEnnemie);
            gameObject.SetActive(false);
        }
    }

}
