using UnityEngine;

public class ChoosenTurretController : MonoBehaviour
{
    public static ChoosenTurretController instance;

    Ray ray;
    MeshRenderer[] rend;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if(instance != null)
        {
            Destroy(instance);
        }

        rend = GetComponentsInChildren<MeshRenderer>();
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
                    transform.position = raycastHit.transform.position;
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
        rend[0].enabled = visibility;
        rend[1].enabled = visibility;
    }

    private void OnDestroy()
    {
        Destroy(gameObject);
        BuildManager.instance.SetTurretToBuild(null);
    }
}
