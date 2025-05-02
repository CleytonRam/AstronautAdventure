using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

namespace Enemy
{
    public class EnemyWalk : EnemyBase
    {
        [Header("Enemy Walk")]
        public GameObject[] waypoints;
        public float minDistane = 1f;
        public float speed = 1f;
        public float lookAtSpeed = 1f;
        public Ease ease = Ease.Linear;

        private int _index = 0;

        public override void Update()
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

            Vector3 targetPosition = waypoints[_index].transform.position;
            transform.DOLookAt(targetPosition, lookAtSpeed).SetEase(ease);
        }
    }
}
