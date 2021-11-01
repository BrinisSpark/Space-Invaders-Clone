using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;

public class WaveController : MonoBehaviour
{
    // Start is called before the first frame update
    public float WaveSpeed = 1;
    IEnumerator Start()
    {

        transform.parent = null;
        Vector3 pivot = transform.localPosition;
        pivot.y = 0;
        transform.localPosition = pivot;
     
        yield return null;
        WaitForSeconds w = new WaitForSeconds(GamePlayManager.instance.gamePlayCoords.waveInitialStepForwardDelay);
        yield return MoveInitialPlace();
        int speedLevel = 1;
        while (true)
        {
            yield return new WaitForSeconds(GamePlayManager.instance.gamePlayCoords.waveInitialStepForwardDelay/speedLevel*4*WaveSpeed);

            if (GetComponentsInChildren<EnnemieDetectionController>().Length == 0)
            {
                PlayerStatsManager.instance.InitWaveStats();
                Addressables.ReleaseInstance(gameObject);
            }
            else
            {
                if (movingForward)
                {
                    yield return StartCoroutine(MoveToward());
                    movingForward = false;
                    speedLevel++;
                }
                StartCoroutine(Slide());
            }

         
        }

    }
    IEnumerator Slide()
    {
        Vector3 destination;
        Vector3 direction;
        if (moveingLeft)
            direction = transform.right*(-1);
        else
            direction = transform.right;

             destination = transform.position - direction * GamePlayManager.instance.gamePlayCoords.waveInitialStepForwardDistance/10;

        float delay = 0.2f, time = 0;

        while (time < delay)
        {
            time += Time.deltaTime;
            yield return null;
            transform.position = Vector3.Lerp(transform.position, destination, time / delay);
        }
        transform.position = destination;
    }

    IEnumerator MoveToward()
    {
        Vector3 destination = transform.position - transform.forward * GamePlayManager.instance.gamePlayCoords.waveInitialStepForwardDistance;
        float delay = 0.7f, time = 0;
      
        while(time<delay)
        {
            time += Time.deltaTime;
            yield return null;
            transform.position = Vector3.Lerp(transform.position, destination, time / delay);
        }
        transform.position = destination;
    }
    IEnumerator MoveInitialPlace()
    {
        Vector3 destination = WavesManager.instance.waveSlideToPoint.position;
        float delay = 2f, time = 0;
        while (time < delay)
        {
            time += Time.deltaTime;
            yield return null;
            transform.position = Vector3.Lerp(transform.position, destination, time / delay);
        }
        transform.position = destination;
    }

   public bool moveingLeft = true;
    bool movingForward=false;
    public void OnDirectionChanged(bool newDirectionIsLeft)
    {
        Debug.Log("direction changed to left =" + newDirectionIsLeft);
        moveingLeft = newDirectionIsLeft;
        movingForward = true;
    }
  
}
