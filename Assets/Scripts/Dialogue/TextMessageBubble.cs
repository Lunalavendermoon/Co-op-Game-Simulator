using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TextMessageBubble : MonoBehaviour
{
    public TMP_Text senderText;
    public TMP_Text messageText;
    public RectTransform bubbleRect;

    public GameObject profilePic;
    public Sprite playerPfp;
    public Sprite friendPfp;

    public void SetMessage(string sender, string message)
    {
        senderText.text = sender;
        messageText.text = message;

        Image pfp = profilePic.GetComponent<Image>();

        // Align based on sender
        if (sender == "You") {
            bubbleRect.anchorMin = bubbleRect.anchorMax = new Vector2(1, 1);
            bubbleRect.pivot = new Vector2(1, 1);
            pfp.sprite = playerPfp;
        } else {
            bubbleRect.anchorMin = bubbleRect.anchorMax = new Vector2(0, 1);
            bubbleRect.pivot = new Vector2(0, 1);
            pfp.sprite = friendPfp;
        }
    }
}
