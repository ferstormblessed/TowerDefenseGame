using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlashEffect : MonoBehaviour
{
    [SerializeField] private Renderer[] _renderers;
    [SerializeField] private Color _flashColor;
    [SerializeField] private Color _originalColor;
    [SerializeField] private float _cooldownSpeed = 5;
    [SerializeField] private bool _playOnAwake;

    private void Awake()
    {
        if(_playOnAwake)
            PlayFlash();
    }

    public void PlayFlash()
    {
        foreach (var renderer in _renderers)
        {
            renderer.material.color = _flashColor;
        }
    }
    private void Update()
    {
        if(_renderers.Length == 0)
            return;
        
        if (_renderers[0].material.color.a > 0)
        {
            foreach (var renderer in _renderers)
            {
                renderer.material.color = Color.Lerp(renderer.material.color,
                    _originalColor, Time.deltaTime * _cooldownSpeed);
            }
        }
    }
}
