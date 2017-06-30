using UnityEngine;
using System.Collections;

public class LevelChanger : MonoBehaviour {

    public ScreenFader fadeScr;

    void OnTriggerEnter2D(Collider2D col)
    {

        fadeScr.EndScene();
        
    }
}
