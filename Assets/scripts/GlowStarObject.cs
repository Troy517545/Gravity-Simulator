using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlowStarObject : MonoBehaviour
{
    public Color GlowColor;
    public float LerpFactor = 10;
    starObject starObjectScript;

    public Renderer[] Renderers
    {
        get;
        private set;
    }

    public Color CurrentColor
    {
        get { return _currentColor; }
    }

    private List<Material> _materials = new List<Material>();
    private Color _currentColor;
    private Color _targetColor;
    private bool glowStatus = false;

    void Start()
    {
        starObjectScript = GetComponent<starObject>();


        Renderers = GetComponentsInChildren<Renderer>();

        foreach (var renderer in Renderers)
        {
            _materials.AddRange(renderer.materials);
        }
    }

    private void OnMouseEnter()
    {
        if(glowStatus == false)
        {
            _targetColor = GlowColor;
            enabled = true;
        }
    }

    private void OnMouseExit()
    {
        if(glowStatus == false)
        {
            _targetColor = Color.black;
            enabled = true;
        }
    }

    /// <summary>
    /// Loop over all cached materials and update their color, disable self if we reach our target color.
    /// </summary>
    private void Update()
    {
        if(starObjectScript.selectedToDisplayInfo == true & glowStatus == false)
        {
            _targetColor = GlowColor;
            enabled = true;
            glowStatus = true;
        }
        else if (starObjectScript.selectedToDisplayInfo == false & glowStatus == true)
        {
            _targetColor = Color.black;
            enabled = true;
            glowStatus = false;
        }

        _currentColor = Color.Lerp(_currentColor, _targetColor, Time.deltaTime * LerpFactor);

        for (int i = 0; i < _materials.Count; i++)
        {
            _materials[i].SetColor("_GlowColor", _currentColor);
        }

        if (_currentColor.Equals(_targetColor))
        {
            //enabled = false;
        }
    }
}
