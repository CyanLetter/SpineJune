using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class ScreenFader : MonoBehaviour
{
    public SpriteRenderer FadeImg;
    public float fadeTime = 1.5f;
    public bool sceneStarting = true;
    public bool sceneEnding = false;
    public string sceneToLoad;

    float lerpTime = 0f;
    float percentComplete = 0f;

    void Update()
    {  
        if (sceneStarting) {
            lerpTime += Time.deltaTime;
            percentComplete = lerpTime / fadeTime;
            if (percentComplete >= 1f) {
                percentComplete = 1f;
                sceneStarting = false;
            }
            FadeImg.color = Color.Lerp(Color.black, Color.clear, percentComplete);
        }

        if (sceneEnding) {
            lerpTime += Time.deltaTime;
            percentComplete = lerpTime / fadeTime;
            if (percentComplete >= 1f) {
                percentComplete = 1f;
                sceneEnding = false;
                SceneManager.LoadScene(sceneToLoad);
            }
            FadeImg.color = Color.Lerp(Color.clear, Color.black, percentComplete);
        }
    }


    void StartScene()
    {
        sceneEnding = false;
        sceneStarting = true;
        lerpTime = 0f;
    }

    public void EndScene()
    {
        sceneStarting = false;
        sceneEnding = true;
        lerpTime = 0f;
    }
}   