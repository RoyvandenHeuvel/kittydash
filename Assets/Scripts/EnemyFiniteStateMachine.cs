﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    [RequireComponent(typeof(Transform))]
    public class EnemyFiniteStateMachine : MonoBehaviour
    {
        public List<Strategy> Strategies;

        public enum State
        {
            CatchingUp,
            Nearby,
            ExecuteStrategy
        }

        private State _currentState;
        private GameObject _player;
        private Vector3 _chaseTargetLocation;
        private Vector3 _currentPosition;

        private float _enemyCatchUpSpeed;
        private float _enemyNearbySpeed;
        private float _distanceForCatchingUpState;
        private float _randomness;
        private float _cameraSpeedFactor;

        void Start()
        {
            _enemyCatchUpSpeed = GameManager.Instance.GameData.EnemySpeedFar;
            _enemyNearbySpeed = GameManager.Instance.GameData.EnemySpeedNearby;
            _distanceForCatchingUpState = GameManager.Instance.GameData.EnemyNearbyDistance;
            _randomness = GameManager.Instance.GameData.EnemyRandomness;
            _cameraSpeedFactor = GameManager.Instance.GameData.EnemyCameraSpeedFactor;
            
            _player = GameObject.Find("Player");
            _currentState = State.CatchingUp;
            _chaseTargetLocation = _player.transform.position;
        }

        void Update()
        {
            _currentPosition = transform.position;
            switch (_currentState)
            {
                case State.CatchingUp:
                    CatchUp();
                    break;
                case State.Nearby:
                    Nearby();
                    break;
                case State.ExecuteStrategy:
                    ExecuteStrategy();
                    break;
            }

            transform.position = new Vector3(transform.position.x, transform.position.y + (_cameraSpeedFactor * GameManager.Instance.GameData.CameraSpeed), transform.position.z);
        }

        IEnumerator RandomChase()
        {
            for (float f = 6.0f; f >= 0; f -= 0.1f)
            {
                var targetPosition = _player.transform.position;
                _chaseTargetLocation = new Vector3(
                    UnityEngine.Random.Range(targetPosition.x - _randomness, targetPosition.x + _randomness),
                    UnityEngine.Random.Range(targetPosition.y, targetPosition.y + _randomness * 2),
                    targetPosition.z);

                yield return null;
            }
        }

        private void CatchUp()
        {
            var targetPosition = _player.transform.position;

            transform.position = Vector3.MoveTowards(_currentPosition, targetPosition, Time.deltaTime * _enemyCatchUpSpeed);

            if (Vector3.Distance(_currentPosition, targetPosition) < _distanceForCatchingUpState / 2) _currentState = State.Nearby;
        }

        private void Nearby()
        {
            StartCoroutine("RandomChase");

            var targetPosition = _player.transform.position;
            transform.position = Vector3.MoveTowards(_currentPosition, _chaseTargetLocation, Time.deltaTime * _enemyNearbySpeed);

            if (Vector3.Distance(_currentPosition, targetPosition) > _distanceForCatchingUpState) _currentState = State.CatchingUp;
            if (UnityEngine.Random.Range(0, 1) < 0.0001f) _currentState = State.ExecuteStrategy;
        }

        private Strategy _chosenStrategy;
        private List<Vector3> _screenPointMovementPoints = new List<Vector3>();

        private void ExecuteStrategy()
        {
            if (Vector3.Distance(_currentPosition, _player.transform.position) > _distanceForCatchingUpState) _currentState = State.CatchingUp;

            if (_chosenStrategy == null)
            {
                _chosenStrategy = Strategies[UnityEngine.Random.Range(0, Strategies.Count)];

                foreach (var movementPoint in _chosenStrategy.MovementPoints)
                {
                    _screenPointMovementPoints.Add(Camera.main.WorldToScreenPoint(_player.transform.position + movementPoint));
                }
            }

            if (_screenPointMovementPoints.Count > 0)
            {
                var currentPoint = _screenPointMovementPoints[0];
                var currentPointAsWorldPoint = Camera.main.ScreenToWorldPoint(currentPoint);
                transform.position = Vector3.MoveTowards(_currentPosition, currentPointAsWorldPoint, Time.deltaTime * _enemyNearbySpeed);

                if (Vector3.Distance(transform.position, currentPointAsWorldPoint) < 1) _screenPointMovementPoints.RemoveAt(0);
            }
            else
            {
                _chosenStrategy = null;
                _currentState = State.Nearby;
            }

        }

    }
}
