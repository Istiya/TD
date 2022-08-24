using UnityEngine;

public class TurretModel : MonoBehaviour
{
    public Transform partToRotate;
    public Transform[] firePoint;
    public PrefabType projectilePrefab;
    public Color defaultColot = Color.clear;
    public Color onMouseDownColor = Color.gray;

    public string enemyTag = "Enemy";

    public Sprite sprite;
    public string turretName;
    public float range = 5f;
    public float turnSpeed = 10f;
    public float fireRate = 1f;
    public TurretCost cost;

    public string controller;

    public TerrainNodeModel terrainNodeModel;
}
