using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileController : MonoBehaviour
{
    private EnemyController _target;
    private ProjectileModel _model;

    public void Seek(GameObject target)
    {
        _target = target.GetComponent<EnemyController>();
    }

    private void Awake()
    {
        _model = GetComponent<ProjectileModel>();
    }

    private void Update()
    {
        if (_target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector3 direction = _target.transform.position - transform.position;
        float distanceThisFrame = _model.speed * Time.deltaTime;
        transform.rotation = Quaternion.LookRotation(direction);

        transform.Translate(direction.normalized * distanceThisFrame, Space.World);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponentInParent<EnemyController>() == null)
            return;
        else if(_target.GetComponent<EnemyModel>().health != 0)
        { 
            if(collision.collider is MeshCollider)
            {
                Debug.Log(collision.collider);
            }
            HitTarget();
        }
    }

    void HitTarget()
    {
        _target.ChangeHealth(_model.damage);
        GameObject particlles = Instantiate(PrefabManager.instance.Get(PrefabType.PROJECTILE_IMPACT_EFFECT), transform.position, Quaternion.identity, transform.root.Find("Particles"));
       // particlles.GetComponent<ParticleSystem>().GetComponent<Renderer>().material = _target.GetComponentInChildren<Renderer>().material;
        Destroy(gameObject);
    }
}
