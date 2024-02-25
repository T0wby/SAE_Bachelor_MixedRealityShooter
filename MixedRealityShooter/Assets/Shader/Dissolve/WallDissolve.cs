using System;
using System.Collections;
using UnityEngine;

[RequireComponent(typeof(MeshRenderer))]
public class WallDissolve : MonoBehaviour
{
    [SerializeField] private MeshRenderer _dissolveRenderer;
    private Material _dissolveMaterial;
    private static readonly int AlphaClipThreshold = UnityEngine.Shader.PropertyToID("_AlphaClipThreshold");

    private void Awake()
    {
        _dissolveRenderer = GetComponent<MeshRenderer>();
        _dissolveMaterial = _dissolveRenderer.material;
    }

    public void DissolveMaterial(float duration)
    {
        StartCoroutine(StartDissolving(duration));
    }

    private IEnumerator StartDissolving(float duration)
    {
        _dissolveMaterial.SetFloat(AlphaClipThreshold, 1.0f);
        float time = duration;

        while (time > 0.0f)
        {
            _dissolveMaterial.SetFloat(AlphaClipThreshold, 1.0f - (time / duration));
            time -= Time.deltaTime;
            yield return null;
        }

        //_dissolveMaterial.SetFloat(AlphaClipThreshold, 0.0f);
    }

    public void ReturnMaterial(float duration)
    {
        StartCoroutine(ReturnDissolving(duration));
    }

    private IEnumerator ReturnDissolving(float duration)
    {
        _dissolveMaterial.SetFloat(AlphaClipThreshold, 0.0f);
        float time = duration;

        while (time > 0.0f)
        {
            _dissolveMaterial.SetFloat(AlphaClipThreshold, time / duration);
            time -= Time.deltaTime;
            yield return null;
        }

        //_dissolveMaterial.SetFloat(AlphaClipThreshold, 1.0f);
    }
}