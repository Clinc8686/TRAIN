using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneLoaderCallback : MonoBehaviour
{
    [SerializeField] private float loadingSceneWaitTime = 2f;
    private bool _isFirstUpdate = true;
    private void Update()
    {
        if(_isFirstUpdate)
        {
            _isFirstUpdate = false;
            StartCoroutine(LoaderCallback());
        }
    }
    private IEnumerator LoaderCallback()
    {
        yield return new WaitForSeconds(loadingSceneWaitTime);
        SceneLoader.LoaderCallBack();
    }
}
