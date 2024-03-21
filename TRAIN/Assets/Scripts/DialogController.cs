using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class DialogController : MonoBehaviour
{
    public static DialogController Instance { get; private set; }

    [SerializeField] private Transform dialogSystemContent;
    [SerializeField] private TextMeshProUGUI dialogTextField;
    [SerializeField] private float textTime = .1f;

    private string _dialog;
    private bool _isWritingText = false;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        dialogSystemContent.gameObject.SetActive(false);
        DontDestroyOnLoad(this);
    }
    public void WriteText(string text)
    {
        if (_isWritingText) return;

        dialogSystemContent.gameObject.SetActive(true);
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

        yield return new WaitForSeconds(3);
        _isWritingText = false;
        dialogSystemContent.gameObject.SetActive(false);
    }
}
