using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretController : MonoBehaviour
{
    private TurretModel _model;
    private Transform _target = null;
    private MeshRenderer[] rend;
    public bool isSelected = false;

    private void Start()
    {
        _model = GetComponent<TurretModel>();
        rend = GetComponentsInChildren<MeshRenderer>();
        
        InvokeRepeating("FindTarget", 0f, 0.3f);
        InvokeRepeating("Shoot", 0f, _model.fireRate);
    }

    private void Update()
    {
        if (_target == null)
        {
            return;
        }
        RotateTo(_target.position);
    }

    protected virtual void Shoot()
    {
        if (_target == null)
            return;

        for (int i = 0; i < _model.firePoint.Length; i++)
        {
            GameObject bullet = Instantiate(PrefabManager.instance.Get(_model.projectilePrefab), _model.firePoint[i].position, _model.firePoint[i].transform.rotation, transform.root.Find("Projectiles"));
            bullet.AddComponent<ProjectileController>();
            bullet.GetComponent<ProjectileController>().Seek(_target.gameObject);
        }
    }

    protected virtual void FindTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag(_model.enemyTag);
        int maxWaypointIndex = 0;
        Transform potencialTarget = null;
        float minDistanceToWaypoint = 0f;

        foreach (GameObject enemy in enemies)
        {
            float distanceToEnemy = Vector3.Distance(transform.position, enemy.transform.position);

            if (enemy.GetComponent<EnemyController>().GetWaypointIndex() >= maxWaypointIndex && distanceToEnemy <= _model.range)
            {
                maxWaypointIndex = enemy.GetComponent<EnemyController>().GetWaypointIndex();

                if (minDistanceToWaypoint < enemy.GetComponent<EnemyController>().GetDistanceToWaypoint())
                {
                    minDistanceToWaypoint = enemy.GetComponent<EnemyController>().GetDistanceToWaypoint();
                    potencialTarget = enemy.transform;
                }
            }
        }

        _target = potencialTarget;
    }
    //DoTweener

    private void RotateTo(Vector3 targetPos)
    {
        Vector3 direction = targetPos - transform.position;
        Quaternion lookRotation = Quaternion.LookRotation(direction);
        Vector3 rotation = Quaternion.Lerp(_model.partToRotate.rotation, lookRotation, Time.deltaTime * _model.turnSpeed).eulerAngles;
        _model.partToRotate.rotation = Quaternion.Euler(0f, rotation.y, 0f);
    }

    public void Unselect()
    {
        isSelected = false;
        foreach (MeshRenderer renderer in rend)
        {
            renderer.material.color = _model.defaultColot;
        }
    }

    public void OnMouseDown()
    {
        if (isSelected)
        {
            return;
        }

        foreach (MeshRenderer renderer in rend)
        {
            renderer.material.color = _model.onMouseDownColor;
        }

        GameObject panel = Instantiate(PrefabManager.instance.Get(PrefabType.TURRET_INFO_PANEL), GameObject.FindGameObjectWithTag("UI").transform);
        panel.GetComponent<TurretInfoPanel>().Init(_model);
        isSelected = true;
    }
}
