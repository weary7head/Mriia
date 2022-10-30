using Player.Input;
using UnityEngine;
using UnityEngine.UI;

public class UserIntarface : MonoBehaviour
{
    [SerializeField] private Slider slider;
    [SerializeField] private Hero hero;

    private void OnEnable()
    {
        hero.HealthChanged += HealthChanged;
    }

    private void OnDisable()
    {
        hero.HealthChanged -= HealthChanged;
    }

    private void HealthChanged(float value)
    {
        slider.value = value / 100;
    }
}
