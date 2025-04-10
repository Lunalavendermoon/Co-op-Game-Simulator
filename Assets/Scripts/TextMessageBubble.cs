using UnityEngine;
using TMPro;

public class TextMessageBubble : MonoBehaviour
{
    public TMP_Text senderText;
    public TMP_Text messageText;
    public RectTransform bubbleRect;

    public void SetMessage(string sender, string message)
    {
        senderText.text = sender;
        messageText.text = message;

        // Align based on sender
        if (sender == "You") {
            bubbleRect.anchorMin = bubbleRect.anchorMax = new Vector2(1, 1);
            bubbleRect.pivot = new Vector2(1, 1);
        } else {
            bubbleRect.anchorMin = bubbleRect.anchorMax = new Vector2(0, 1);
            bubbleRect.pivot = new Vector2(0, 1);
        }
    }
}
