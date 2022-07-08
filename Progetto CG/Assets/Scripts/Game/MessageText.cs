using System.Collections;
using TMPro;
using UnityEngine;

// classe per gestire i messaggi di servizio
public class MessageText : MonoBehaviour
{
    public static MessageText Instance { get; private set; }

    private TextMeshProUGUI _textMeshProUGUI;

    private void Awake()
    {
        Instance = this;
        _textMeshProUGUI = GetComponent<TextMeshProUGUI>();
    }

    public void WriteMessage(string message)
    {
        StartCoroutine(MessageCoroutine(message));
    }

    private IEnumerator MessageCoroutine(string message)
    {
        _textMeshProUGUI.text = message;
        yield return new WaitForSeconds(1);
        _textMeshProUGUI.text = "";
    }
}
