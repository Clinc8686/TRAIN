using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Testing : MonoBehaviour
{
    private bool _isFirstFrame = false;
    private void Update()
    {
        if (!_isFirstFrame)
        {
            _isFirstFrame = true;
            DialogController.Instance.WriteText("Luka und Mario sind die geilsten!");
            //StartCoroutine(Test());
        }
    }

    private IEnumerator Test()
    {
        yield return new WaitForSeconds(2);

        DialogController.Instance.WriteText("Yes");
    }
}
