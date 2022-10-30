using System;
using UnityEngine;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform;
    [SerializeField, Range(0, 1)] private float smoothing = 0.2f;
    [SerializeField] private Transform target;
    [SerializeField] private Vector3 offset;

    void LateUpdate()
    {
        cameraTransform.position = Vector3.Lerp(cameraTransform.position, target.position + offset, smoothing);
    }
}