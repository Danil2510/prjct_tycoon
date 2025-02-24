using UnityEngine;

public class ConveyorMove : MonoBehaviour
{
    [SerializeField] float speed;

    private void OnTriggerStay(Collider other)
    {
        other.transform.position += transform.right * speed;
    }

    public void SetSpeed(float newspeed)
    {
        speed = newspeed;
    }
}
