using UnityEngine;
using System.Collections;

public class ChangeLevel : MonoBehaviour {
    public string name;

    public void Change()
    {
        Application.LoadLevel(name);
    }

    void QuiteGame()
    {
        Application.Quit();
    }
}
