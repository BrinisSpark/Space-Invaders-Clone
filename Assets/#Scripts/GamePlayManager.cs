using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GamePlayManager : MonoBehaviour
{
    // Start is called before the first frame update
    public static GamePlayManager instance;
    public GamePlayCoords gamePlayCoords;
    public Text gameOverText;
    public bool gameIsRunning = true;
    private void Awake()
    {
        instance = this;
        gameOverText.gameObject.SetActive(false);
    }
    public void  Victory()
    {
        gameOverText.text = "Victory !!!";
        gameOverText.gameObject.SetActive(true);
        gameIsRunning = false;
    }

}
