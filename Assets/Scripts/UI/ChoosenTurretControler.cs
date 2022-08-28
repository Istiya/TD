using System;
using UnityEngine;

#nullable enable

public class ChoosenTurretController : MonoBehaviour
{
    public static ChoosenTurretController instance;

    Ray ray;
    MeshRenderer[] renderers;
    TurretModel _model;

    public PrefabType turret;
    public int cost;

    private void Awake()
    {
        instance = this;

        renderers = GetComponentsInChildren<MeshRenderer>();
        
        _model = GetComponent<TurretModel>();

        GetComponent<BoxCollider>().enabled = false;

        CircleDrawer.DrawCircle(gameObject, _model.range, 0.05f);

        if(TurretInfoPanel.instance != null)
        {
            Destroy(TurretInfoPanel.instance.gameObject);
        }
    }

    public void Init(PrefabType turret, int cost)
    {
        this.turret = turret;
        this.cost = cost;
    }

    private void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit raycastHit))
        {
            TerrainNodeModel node = raycastHit.collider.GetComponent<TerrainNodeModel>();
            if (node != null)
            {
                SetVisibility(false);
                if (!node.isTowerPlaced)
                {
                    SetVisibility(true);
                    Vector3 position = raycastHit.transform.position;
                    position.y += 0.05f;
                    transform.position = position;
                }
                else
                {
                    SetVisibility(false);
                }
            }
            else
            {
                SetVisibility(false);
            }
        }
        else
        {
            SetVisibility(false);
        }

        if (Input.GetMouseButtonDown(1))
        {
            Destroy(gameObject);
        }
    }

    void SetVisibility(bool visibility)
    {
        foreach(MeshRenderer rend in renderers)
        {
            rend.enabled = visibility;
        }

        GetComponent<LineRenderer>().enabled = visibility;
    }

    private void OnDestroy()
    {
        foreach(ShopPanelItemController controller in FindObjectsOfType<ShopPanelItemController>())
        {
            controller.OnUnselect();
        }
    }
}
