using UnityEngine;
using Yarn.Unity;
using System.Collections;

public static class YarnTextMessageCommands
{
    [YarnCommand("textmessage")]
    public static IEnumerator ShowTextMessage(string sender, string message)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnMessage(sender, message);
        AudioManager.Instance.PlaySFX("message");
        yield return new WaitForSeconds(0.5f);
    }
}
