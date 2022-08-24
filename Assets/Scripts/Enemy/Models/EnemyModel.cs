using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyModel : MonoBehaviour
{
    public float speed;
    public Health health;
    public int reward;
    public int damage;
    public Vector3 targetWaypoint;
    public int waypointIndex = 0;
    public int score;
}
