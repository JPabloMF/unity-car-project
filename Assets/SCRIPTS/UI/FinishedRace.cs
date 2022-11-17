using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FinishedRace : MonoBehaviour
{
    public int nextRaceNumber = 3;
    public void NextRace()
    {
        SceneManager.LoadScene(nextRaceNumber);
    }
    public void ExitRace()
    {
        SceneManager.LoadScene(0);
    }
}
