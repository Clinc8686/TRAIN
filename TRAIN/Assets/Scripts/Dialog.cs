using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using TMPro;

public class Dialog : MonoBehaviour
{
    public static Dialog Instance { get; private set; }

    [SerializeField] private TextMeshProUGUI dialogTextField;
    [SerializeField] private float textTime = .1f;
    
    private String _dialog;
    private bool _isWritingText = false;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy((gameObject));
            return;
        }

        Instance = this;
    }

    public void ShowText(String text)
    {
        if (_isWritingText) return;

        dialogTextField.text = "";
        _dialog = text;
        _isWritingText = true;
        StartCoroutine(WriteTextDialog());
    }

    private IEnumerator WriteTextDialog()
    {
        for (int i = 0; i < _dialog.Length; i++)
        {
            yield return new WaitForSeconds(textTime);
            dialogTextField.text += _dialog[i];
        }

        _isWritingText = false;
    }
}
