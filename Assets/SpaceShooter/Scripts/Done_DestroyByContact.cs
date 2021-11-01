using System;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class Done_DestroyByContact : MonoBehaviour
{
    // ADDRESSABLES UPDATES
    public AssetReference explosion;
	public AssetReference playerExplosion;

	public int scoreValue;




	void OnTriggerEnter (Collider other)
	{

		return;
		if(other.GetComponent<PlayerBulletDetector>())
        {


			// ADDRESSABLES UPDATES
			
			explosion.InstantiateAsync(transform.position, transform.rotation);
			Addressables.ReleaseInstance(other.gameObject);
			Addressables.ReleaseInstance(gameObject);
			return;
		}

		
		if (other.tag == "Boundary" || other.tag == "Enemy")
		{
			return;
		}

		if (explosion != null)
		{
            // ADDRESSABLES UPDATES
            explosion.InstantiateAsync(transform.position, transform.rotation);
		}

	}
}