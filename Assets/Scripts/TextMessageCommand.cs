using UnityEngine;
using Yarn.Unity;

public class TextMessageCommand : MonoBehaviour
{
    public GameObject messageBubblePrefab;
    public Transform messageContainer;

    public void SpawnMessage(string sender, string message)
    {
        var bubble = Instantiate(messageBubblePrefab, messageContainer);
        bubble.GetComponent<TextMessageBubble>().SetMessage(sender, message);
    }
}

