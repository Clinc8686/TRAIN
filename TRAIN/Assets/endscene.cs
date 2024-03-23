using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class endscene : MonoBehaviour
{
    private float timer = 0f;
    
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= 60)
        {
            SceneLoader.Load(SceneLoader.Scenes.HomeMenuScene);
        }
    }
}
