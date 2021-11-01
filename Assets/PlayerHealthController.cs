using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class PlayerHealthController : MonoBehaviour
{
    public static PlayerHealthController instance;
    public AssetReference explosion;
    public bool invulnerable = false;
    void Start()
    {
        lastHitTime = Time.realtimeSinceStartup;
        instance = this;
    }
    public float lastHitTime;
    private void OnTriggerEnter(Collider other)
    {


      
            if (Time.realtimeSinceStartup - lastHitTime < 3) return;
            if (invulnerable) return;
            if(other.attachedRigidbody)
            if(other.attachedRigidbody.GetComponentInChildren<EnnemieDetectionController>())
            {
                PlayerStatsManager.instance.HitPlayer();
                explosion.InstantiateAsync(transform.position, transform.rotation);
                StartCoroutine(BeinginvulnerableForDelay());
                lastHitTime = Time.realtimeSinceStartup;
            }
       
    }
    IEnumerator BeinginvulnerableForDelay()
    {
        invulnerable = true;
        float delay = GamePlayManager.instance.gamePlayCoords.invulnerableDelay;
        float time = 0;
        WaitForSeconds w = new WaitForSeconds(0.07f);
        while (time<delay)
            {
            GetComponentInChildren<MeshRenderer>().material.color = Color.red;
            yield return w;
            GetComponentInChildren<MeshRenderer>().material.color = Color.blue;
            yield return w;
            GetComponentInChildren<MeshRenderer>().material.color = Color.green;
            yield return w;
            time+= 0.07f*3;
        }
        GetComponentInChildren<MeshRenderer>().material.color = Color.white;
       // yield return new WaitForSeconds(GamePlayManager.instance.gamePlayCoords.invulnerableDelay);
        invulnerable = false;
    }
   





}
