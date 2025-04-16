using System;
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

        // reset mesh bounds
        senderText.ForceMeshUpdate();
        messageText.ForceMeshUpdate();

        // Message height is Max(text height, PFP height)
        float messageHeight = Math.Max(GetTMPHeight(senderText) + GetTMPHeight(messageText),
                            profilePic.GetComponent<RectTransform>().rect.height);
        bubbleRect.sizeDelta = new Vector2(bubbleRect.sizeDelta.x, messageHeight);

        Debug.Log("BEFORE " + bubbleRect.localPosition);

        Vector2 newPos = new Vector2(bubbleRect.localPosition.x,
                                        bubbleRect.localPosition.y - messageHeight / 2);
        bubbleRect.localPosition = newPos;

        Debug.Log("AFTER " + bubbleRect.localPosition);

        Image pfp = profilePic.GetComponent<Image>();

        // Align based on sender and set PFP
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

    float GetTMPHeight(TMP_Text text) {
        Bounds b = text.bounds;
        return b.size.y;
    }
}
