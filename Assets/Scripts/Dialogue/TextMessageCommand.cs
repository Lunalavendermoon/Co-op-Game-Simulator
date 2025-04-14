using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TextMessageCommand : MonoBehaviour
{
    public GameObject messageBubblePrefab;
    public RectTransform messageContainer;
    public ScrollRect scrollRect;

    // the height of a single text message
    public float messageHeight;
    float betweenMessages;

    void Start()
    {
        betweenMessages = GetComponent<VerticalLayoutGroup>().spacing;
    }

    public void SpawnMessage(string sender, string message)
    {
        var bubble = Instantiate(messageBubblePrefab, messageContainer);
        bubble.GetComponent<TextMessageBubble>().SetMessage(sender, message);

        
        // Resizing the container extends it in both directions, so we must reposition it accordingly
        Vector2 newPos = new Vector2(messageContainer.localPosition.x,
                                        messageContainer.localPosition.y + messageHeight/2 + betweenMessages);

        messageContainer.sizeDelta = new Vector2(messageContainer.sizeDelta.x, messageContainer.sizeDelta.y + messageHeight);
        messageContainer.localPosition = newPos;

        scrollRect.normalizedPosition = new Vector2(0, 0);
    }
}

