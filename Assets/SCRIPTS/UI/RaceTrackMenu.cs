using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RaceTrackMenu : MonoBehaviour
{
    private void Update() {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene(0);
        }
    }
    public void PlayHouseTrack()
    {
        GameObject.FindGameObjectWithTag("Music").GetComponent<BGMusic>().StopMusic();
        SceneManager.LoadScene(2);
    }
}
