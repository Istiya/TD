using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainNodeController : MonoBehaviour
{
    private TerrainNodeModel _model;
    private Renderer _rend;

    void Awake()
    {
        _rend = GetComponent<Renderer>();
        _model = GetComponent<TerrainNodeModel>();
    }

    void OnMouseEnter()
    {
        if (!_model.isTowerPlaced)
            _rend.material.color = Color.gray;
    }

    private void OnMouseExit()
    {
        _rend.material.color = _model.defaultColor;
    }

    private void OnMouseDown()
    {
        BuildManager.instance.BuildTurret(ref _model);
    }
}
