using System;
using System.Collections.Generic;
using ggj26.Services;
using MyBox;
using UnityEngine;

namespace ggj26
{
    public class SheepSpawner : MonoBehaviour
    {
        private const int SHEEPS = 16;
        
        [SerializeField] private Vector2 _spawnArea;
        [SerializeField] private SheepController _botSheepPrefab;
        [SerializeField] private PlayerSheepController _playerSheepPrefab;

        private List<SheepController> _sheeps = new();
        private void Start()
        {
            SpawnSheeps();
            ClockService.Instance.AddDelayCall(2, () => RhythmManager.Instance.GenerateLevel());
        }

        [ButtonMethod]
        private void SpawnSheeps()
        {
            Clean();
            var selectedSheep = RhythmManager.Instance.WolfID;
            for (int i = 0; i < SHEEPS; i++)
            {
                var position = GetRandomPosition();
                var prefab = i == selectedSheep ? _playerSheepPrefab : _botSheepPrefab;
                SheepController newSheep = Instantiate(prefab, position + transform.position.ToVector2(),
                    Quaternion.identity, transform);
                newSheep .SetSheepMask(i);
                newSheep.SetMovement(transform.position, _spawnArea);
                _sheeps.Add(newSheep);
            }
        }
        
        [ButtonMethod]
        public void Clean()
        {
            for (int i = transform.childCount - 1; i >= 0; i--)
            {
                DestroyImmediate(transform.GetChild(i).gameObject);
            }
        }

        private Vector2 GetRandomPosition()
        {
            return new Vector2(
                UnityEngine.Random.Range(-_spawnArea.x*.5f, _spawnArea.x*.5f), 
                UnityEngine.Random.Range(-_spawnArea.y*.5f, _spawnArea.y*.5f));
        }

        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, _spawnArea);
        }
    }
}
