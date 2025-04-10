using UnityEngine;
using Yarn.Unity;

public static class YarnTextMessageCommands
{
    [YarnCommand("textmessage")]
    public static void ShowTextMessage(string sender, string message)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnMessage(sender, message);
    }
}
