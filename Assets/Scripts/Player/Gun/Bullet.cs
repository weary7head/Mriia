using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float speed = 10.0f;
    [SerializeField] private float damage = 10f;

    private Vector3 direction;

    private void Update()
    {
        direction *= speed;
        direction = Vector3.ClampMagnitude(direction, speed);
        transform.Translate(Time.deltaTime * direction, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
       
    }

    public void SetDirection(Vector3 direction)
    {
        this.direction = direction;
    }
}