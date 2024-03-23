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
    public void WriteText(string[] text, Player player)
    {
        dialogSystemContent.gameObject.SetActive(true);
        dialogTextField.text = "";
        _isWritingText = true;
        StartCoroutine(WriteTextDialog(text, player));
    }
    public void ResetIsWritingState()
    {
        _isWritingText = false;
        StopAllCoroutines();
    }
    public void ResetWritingStateAndDeactivateUI()
    {
        _isWritingText = false;
        dialogSystemContent.gameObject.SetActive(false);
        StopAllCoroutines();
    }
    private IEnumerator WriteTextDialog()
    {
        for (int i = 0; i < _dialog.Length; i++)
        {
            //Debug.Log("WriteTextDialog " + _dialog[i]);
            yield return new WaitForSeconds(textTime);
            dialogTextField.text += _dialog[i];
        }

        yield return new WaitForSeconds(3);
        _isWritingText = false;
        dialogSystemContent.gameObject.SetActive(false);
    }
    private IEnumerator WriteTextDialog(string[] text, Player player)
    {
        int index = 0;
        while(index < text.Length)
        {
            int indexer = 0;
            for (int i = 0; i < text[index].Length; i++)
            {
                yield return new WaitForSeconds(textTime);
                dialogTextField.text += text[index][indexer++];
            }
            index++;

            yield return new WaitForSeconds(5);
            dialogTextField.text = "";
        }

        _isWritingText = false;
        dialogSystemContent.gameObject.SetActive(false);
        player.enabled = true;
    }
    public bool IsWriting() => _isWritingText;
}
