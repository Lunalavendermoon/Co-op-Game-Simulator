using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class TextMessageCommand : MonoBehaviour
{
    public DialogueRunner dialogueRunner;

    public GameObject messageBubblePrefab;
    public RectTransform messageContainer;
    public ScrollRect scrollRect;
    public GameObject usernamePrompt;
    public TMP_InputField usernameInput;
    public GameObject usernameInvalidText;

    public TextAsset badWordsFile;

    public int usernameCharLimit;

    public static string playerName = "You";

    float betweenMessages;

    HashSet<string> badWords;

    void Start()
    {
        betweenMessages = messageContainer.GetComponent<VerticalLayoutGroup>().spacing;

        if (usernameInvalidText != null) {
            usernameInvalidText.SetActive(false);
            badWords = new HashSet<string>(Regex.Split(badWordsFile.text, "\n|\r|\r\n"));
            badWords.Remove(""); // sometimes the last line gets read as an empty string
        }
    }

    public void SpawnMessage(string sender, string message)
    {
        var bubble = Instantiate(messageBubblePrefab, messageContainer);
        bubble.GetComponent<TextMessageBubble>().SetMessage(sender.Equals("You") ? playerName : sender, message);

        ResizeContainer(bubble);
    }

    public void SpawnImageMessage(string sender, string message, string filename) {
        var bubble = Instantiate(messageBubblePrefab, messageContainer);
        bubble.GetComponent<TextMessageBubble>().SetMessage(sender.Equals("You") ? playerName : sender, message);

        bubble.GetComponent<TextMessageBubble>().SetImage(filename);
        
        ResizeContainer(bubble);
    }

    void ResizeContainer(GameObject bubble) {
        bubble.GetComponent<TextMessageBubble>().UpdateMessageHeight();

        float messageHeight = bubble.GetComponent<RectTransform>().rect.height;
        
        // Resizing the container extends it in both directions, so we must reposition it accordingly
        Vector2 newPos = new Vector2(messageContainer.localPosition.x,
                                        messageContainer.localPosition.y + messageHeight * 5 + betweenMessages);

        messageContainer.sizeDelta = new Vector2(messageContainer.sizeDelta.x, messageContainer.sizeDelta.y + messageHeight + betweenMessages);
        messageContainer.localPosition = newPos;

        scrollRect.normalizedPosition = new Vector2(0, 0);
    }

    public void SetPlayerName() {
        string userInput = usernameInput.text;
        if (userInput.Length == 0) {
            SetUsernameInvalidText("Username can't be empty >:(");
        } else if (userInput.Length > usernameCharLimit) {
            SetUsernameInvalidText("Username is too long (" + usernameCharLimit + " characters max) >:(");
        } else if (HasBadWord(userInput.ToLower())) {
            SetUsernameInvalidText("No bad words allowed >:(");
        } else {
            playerName = userInput;
            SceneChanger.ChangeSceneByIndex(1);
        }
    }

    bool HasBadWord(string s) {
        foreach (string badWord in badWords) {
            if (s.Contains(badWord.ToLower())) {
                return true;
            }
        }
        return false;
    }

    void SetUsernameInvalidText(string warning) {
        usernameInvalidText.GetComponent<TMP_Text>().text = warning;
        usernameInvalidText.SetActive(true);
    }
}

