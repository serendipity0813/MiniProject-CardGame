using UnityEngine;

public class EndTxt : MonoBehaviour
{
    [System.Obsolete]
    public void RetryGame()
    {
        GameManager.I.RetryGame();
    }
}
