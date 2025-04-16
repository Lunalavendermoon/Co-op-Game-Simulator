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

    public GameObject image;

    private bool hasImage = false;

    static float maxImgWidth = 500, maxImgHeight = 300, imgSpacing = 50;

    public void SetMessage(string sender, string message)
    {
        senderText.text = sender;
        messageText.text = message;

        // reset mesh bounds
        senderText.ForceMeshUpdate();
        messageText.ForceMeshUpdate();

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

    public void SetImage(string filename) {
        hasImage = true;

        Color col = image.GetComponent<Image>().color;
        col.a = 1;
        image.GetComponent<Image>().color = col;

        var sprite = Resources.Load<Sprite>(filename);

        Image img = image.GetComponent<Image>();
        img.sprite = sprite;

        Rect spriteSize = sprite.rect;

        // Resize image to keep it small
        if (sprite.rect.height > maxImgHeight) {
            spriteSize.width = spriteSize.width * maxImgHeight / spriteSize.height;
            spriteSize.height = maxImgHeight;
        } else if (sprite.rect.width > maxImgWidth) {
            spriteSize.height = spriteSize.height * maxImgWidth / spriteSize.width;
            spriteSize.width = maxImgWidth;
        }

        image.GetComponent<RectTransform>().sizeDelta = new Vector2(spriteSize.width, spriteSize.height);

        Vector3 oldPos = image.GetComponent<RectTransform>().localPosition;
        image.GetComponent<RectTransform>().localPosition = 
            new Vector3(oldPos.x,
            senderText.GetComponent<RectTransform>().localPosition.y - GetTMPHeight(senderText) - GetTMPHeight(messageText) - imgSpacing);
    }

    public void UpdateMessageHeight() {
        // Message height is Max(text height, PFP height)
        float messageHeight = Math.Max(GetTMPHeight(senderText) + GetTMPHeight(messageText) + image.GetComponent<RectTransform>().rect.height + (hasImage ? imgSpacing : 0),
                            profilePic.GetComponent<RectTransform>().rect.height);
        bubbleRect.sizeDelta = new Vector2(bubbleRect.sizeDelta.x, messageHeight);

        Vector2 newPos = new Vector2(bubbleRect.localPosition.x,
                                        bubbleRect.localPosition.y - messageHeight / 2);
        bubbleRect.localPosition = newPos;
    }

    float GetTMPHeight(TMP_Text text) {
        Bounds b = text.bounds;
        return b.size.y;
    }
}
