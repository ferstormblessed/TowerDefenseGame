using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class LookAtTarget : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _pivotRotationSpeed = 10;
    [SerializeField] private float _yOffset = 0.5f;

    private void Update()
    {
        if (_target == null)
        {
            return;
        }

        Vector3 direction = _target.position - transform.position;
        direction.y += _yOffset;
        Quaternion targetRotation = Quaternion.LookRotation(direction, Vector3.up);
        transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, Time.deltaTime * _pivotRotationSpeed);
    }

    public void SetTarget(Transform target)
    {
        _target = target;
    }

    public void LoseTarget()
    {
        _target = null;
    }
}
