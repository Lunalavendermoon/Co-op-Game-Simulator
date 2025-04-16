using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TextMessageCommand : MonoBehaviour
{
    public GameObject messageBubblePrefab;
    public RectTransform messageContainer;
    public ScrollRect scrollRect;

    float betweenMessages;

    void Start()
    {
        betweenMessages = messageContainer.GetComponent<VerticalLayoutGroup>().spacing;
    }

    public void SpawnMessage(string sender, string message)
    {
        var bubble = Instantiate(messageBubblePrefab, messageContainer);
        bubble.GetComponent<TextMessageBubble>().SetMessage(sender, message);

        float messageHeight = bubble.GetComponent<RectTransform>().rect.height;
        
        // Resizing the container extends it in both directions, so we must reposition it accordingly
        Vector2 newPos = new Vector2(messageContainer.localPosition.x,
                                        messageContainer.localPosition.y + messageHeight * 5 + betweenMessages);

        messageContainer.sizeDelta = new Vector2(messageContainer.sizeDelta.x, messageContainer.sizeDelta.y + messageHeight + betweenMessages);
        messageContainer.localPosition = newPos;

        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}

