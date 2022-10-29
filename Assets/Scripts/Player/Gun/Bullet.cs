using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private float _speed = 10.0f;
    [SerializeField] private float _damage = 10f;
    private GameObject _target;
    private Enemy _enemy;
    private Player _player;
    private Vector3 _direction;

    private void Update()
    {
        _direction *= _speed;
        _direction = Vector3.ClampMagnitude(_direction, _speed);
        transform.Translate(Time.deltaTime * _direction, Space.World);
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out _target))
        {
            _target.GetDamage(_damage);
        }
        else if (other.gameObject.TryGetComponent(out _enemy))
        {
            _enemy.GetDamage(_damage);
        }
        else if (other.gameObject.TryGetComponent(out _player))
        {
            _player.GetDamage(_damage);
        }
        Destroy(this.gameObject);
    }

    public void SetDirection(Vector3 direction)
    {
        _direction = direction;
    }
}
