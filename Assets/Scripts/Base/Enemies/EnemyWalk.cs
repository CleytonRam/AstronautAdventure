using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Enemy Walk")]
        public GameObject[] waypoints;
        public float minDistane = 1f;
        public float speed = 1f;

        private int _index = 0;

        private void Update()
        {
            if (Vector3.Distance(transform.position, waypoints[_index].transform.position) < minDistane)
            {
                _index++;
                if (_index >= waypoints.Length)
                {
                    _index = 0;
                }

            }

            transform.position = Vector3.MoveTowards(transform.position, waypoints[_index].transform.position, Time.deltaTime * speed);
        }
    }
}
