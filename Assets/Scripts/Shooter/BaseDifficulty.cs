using UnityEngine;

public class BaseDifficulty : MonoBehaviour
{
    public void ChangeDiff(int lv) {
        ShooterManager.SetBaseDifficulty(lv);
    }
}
