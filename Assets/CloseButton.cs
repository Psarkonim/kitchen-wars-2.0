using UnityEngine;

public class CloseButton : MonoBehaviour
{
    private void OnMouseDown()
    {
        Application.Quit();
    }
}
