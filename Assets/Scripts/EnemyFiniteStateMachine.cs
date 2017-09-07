using System.Collections;
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

        public State _currentState;
        private GameObject _player;
        private Vector3 _chaseTargetLocation;
        private Vector3 _currentPosition;

        public float EnemyCatchUpSpeed;
        public float EnemyNearbySpeed;
        public float DistanceForCatchingUpState;
        public float Randomness;
        public float CameraSpeedFactor;

        void Start()
        {
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

            transform.position = new Vector3(transform.position.x, transform.position.y + (CameraSpeedFactor * GameManager.Instance.GameData.CameraSpeed), transform.position.z);
        }

        IEnumerator RandomChase()
        {
            for (float f = 1.5f; f >= 0; f -= 0.1f)
            {
                var targetPosition = _player.transform.position;
                _chaseTargetLocation = new Vector3(
                    UnityEngine.Random.Range(targetPosition.x - Randomness, targetPosition.x + Randomness),
                    UnityEngine.Random.Range(targetPosition.y, targetPosition.y + Randomness * 2),
                    targetPosition.z);

                yield return null;
            }
        }

        private void CatchUp()
        {
            var targetPosition = _player.transform.position;

            transform.position = Vector3.MoveTowards(_currentPosition, targetPosition, Time.deltaTime * EnemyCatchUpSpeed);

            if (Vector3.Distance(_currentPosition, targetPosition) < DistanceForCatchingUpState / 2) _currentState = State.Nearby;
        }

        private void Nearby()
        {
            StartCoroutine("RandomChase");

            var targetPosition = _player.transform.position;
            transform.position = Vector3.MoveTowards(_currentPosition, _chaseTargetLocation, Time.deltaTime * EnemyNearbySpeed);

            if (Vector3.Distance(_currentPosition, targetPosition) > DistanceForCatchingUpState) _currentState = State.CatchingUp;
            if (UnityEngine.Random.Range(0, 1) < 0.0001f) _currentState = State.ExecuteStrategy;
        }

        private Strategy _chosenStrategy;
        private List<Vector3> _screenPointMovementPoints = new List<Vector3>();

        private void ExecuteStrategy()
        {
            if (Vector3.Distance(_currentPosition, _player.transform.position) > DistanceForCatchingUpState) _currentState = State.CatchingUp;

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
                transform.position = Vector3.MoveTowards(_currentPosition, currentPointAsWorldPoint, Time.deltaTime * EnemyNearbySpeed);

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
