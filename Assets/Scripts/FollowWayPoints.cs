using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowWayPoints : MonoBehaviour
{
    [SerializeField] private string _pathName;
    [SerializeField] private List<Vector3> _waypointPositions = new List<Vector3>();
    private int _currentWaypoint;
    [SerializeField] private float _distanceThreshold = 0.3f;
    [SerializeField] private float _walkSpeed = 5;
    
    private void Start()
    {
        StartCoroutine(MoveToNextWaypoint());
    }

    void GetWayPoints()
    {
        Transform path = GameObject.Find(_pathName).transform;

        for (int i = 0; i < path.childCount; i++)
        {
            _waypointPositions.Add(path.GetChild(i).position);
        }
    }

    IEnumerator MoveToNextWaypoint()
    {
        if (_waypointPositions.Count == 0)
        {
            GetWayPoints();
        }
        
        float distance = Vector3.Distance(transform.position, _waypointPositions[_currentWaypoint]);
        while (distance > _distanceThreshold && GameManager.Instance.CurrentGameState == GameManager.GameState.Playing)
        {
            transform.position =
                Vector3.MoveTowards(transform.position, _waypointPositions[_currentWaypoint], Time.deltaTime * _walkSpeed);
            distance = Vector3.Distance(transform.position, _waypointPositions[_currentWaypoint]);
            yield return null;
        }

        if (_currentWaypoint < _waypointPositions.Count - 1)
        {
            _currentWaypoint++;
            StartCoroutine(MoveToNextWaypoint());
        }
    }
}
