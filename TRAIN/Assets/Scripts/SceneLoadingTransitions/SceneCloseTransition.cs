using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneCloseTransition : MonoBehaviour
{
    public void TransitionIsFinished()
    {
        SceneLoader.Load(SceneLoader.Scenes.GameScene1);
    }
}
