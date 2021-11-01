using UnityEngine;

[CreateAssetMenu(fileName = "Data", menuName = "ScriptableObjects/GamePlayCoords", order = 1)]
public class GamePlayCoords : ScriptableObject
{
    public int playerLivesEachGameStart = 7;
    public float fireRate = 1000;
    public int scoreAddedPerEnnemie = 20;
    public float waveInitialStepForwardDelay = 4;
    public float waveInitialStepForwardDistance = 1;
    public float invulnerableDelay = 3;
    //change this when you add more wave prefabs 
    public int lastWaveIndex = 5;
}