using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneLoader
{
    public enum Scenes
    {
        HomeMenuScene,
        LoadingScene,
        GameScene1,
    }
    private class SceneLoaderMonoBehaviour : MonoBehaviour {}
    private static Action onLoaderCallBack;
    public static void Load(Scenes scene)
    {
        onLoaderCallBack = () =>
        {
            GameObject loadingGameObject = new GameObject("Loading Scene GameObject");
            loadingGameObject.AddComponent<SceneLoaderMonoBehaviour>().StartCoroutine(LoadSceneAsync(scene));
        };

        SceneManager.LoadScene(Scenes.LoadingScene.ToString());
    }
    private static IEnumerator LoadSceneAsync(Scenes scene)
    {
        yield return null;

        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(scene.ToString());
        while(!asyncOperation.isDone)
        {
            yield return null;
        }
    }
    public static void LoaderCallBack()
    {
        if(onLoaderCallBack != null)
        {
            onLoaderCallBack();
            onLoaderCallBack = null;
        }
    }
}
