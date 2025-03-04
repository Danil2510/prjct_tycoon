using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] GameObject look;
    [SerializeField] GameObject g;

    public void Do(bool f)
    {
        Cursor.visible = f;
        if (f)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
        look.GetComponent<FirstPersonLook>().enabled = !f;
        g.SetActive(f);
    }
}
