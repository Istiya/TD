using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private EnemyModel _model;

    private void Awake()
    {
        _model = GetComponent<EnemyModel>();
    }

    void Start()
    {
        _model.targetWaypoint = Waypoints.GetWaypoint(_model.waypointIndex);
        _model.health.health = _model.health.max;
        _model.waypointIndex++;
    }

    void Update()
    {
        Move();
    }

    void Move()
    {

        Vector3 direction = _model.targetWaypoint - transform.position;
        transform.Translate(_model.speed * Time.deltaTime * direction.normalized, Space.World);

        Vector3 rotation = Quaternion.Lerp(transform.rotation, Quaternion.LookRotation(direction), Time.deltaTime * _model.speed).eulerAngles;
        transform.rotation = Quaternion.Euler(0f, rotation.y, 0f);

        if (Vector3.Distance(transform.position, _model.targetWaypoint) <= 0.1f)
        {
            if (_model.waypointIndex == Waypoints.waypointList.Count)
            {
                GameManager.instance.ChangeHealth(_model.damage);
                Destroy(gameObject);
                return;
            }

            _model.targetWaypoint = Waypoints.GetWaypoint(_model.waypointIndex);
            _model.waypointIndex++;
        }


    }

    public void ChangeHealth(Damage damage)
    {
        _model.health -= damage;

        if (_model.health == 0)
        {
            DestroyEnemy();
            return;
        }
    }
    private void DestroyEnemy()
    {
        GameManager.instance.ChangeMoney(_model.reward);
        GameManager.instance.ChangeScore(_model.score);
        Destroy(gameObject);
    }

    public float GetDistanceToWaypoint()
    {
        return Vector3.Distance(transform.position, _model.targetWaypoint);
    }

    public int GetWaypointIndex()
    {
        return _model.waypointIndex;
    }
}
