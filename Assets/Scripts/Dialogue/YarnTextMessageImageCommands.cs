using System.Collections;
using Yarn.Unity;
using UnityEngine;

public class YarnTextMessageImageCommands : MonoBehaviour
{
    [YarnCommand("textmessageimage")]
    public static IEnumerator ShowTextMessage(string sender, string message, string filename)
    {
        var manager = GameObject.FindObjectOfType<TextMessageCommand>();
        manager.SpawnImageMessage(sender, message, filename);

        yield return new WaitForSeconds(0.5f);
    }
}
