using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointUseManager : CheckpointBase
{
    [SerializeField] private GameObject _playerRespawnPrefab;

    private GameObject _player;
    private Vector3 _initialPlayerPosition;

    private void Awake()
    {
        _player = GameObject.FindGameObjectWithTag("Player");
        _initialPlayerPosition = _player.transform.position;
    }

    private void Start()
    {
        PlayerPrefs.SetInt("isCheckpointAvailable", 0);
    }

    private void Update()
    {
        if (_player != null && _player.activeInHierarchy == false)
        {
            Destroy(_player);

            if (PlayerPrefs.GetInt("isCheckpointAvailable") == 1)
            {
                var x = PlayerPrefs.GetFloat("lastSavedPosition_X");
                var y = PlayerPrefs.GetFloat("lastSavedPosition_Y");
                var z = PlayerPrefs.GetFloat("lastSavedPosition_Z");

                Observable
                    .Timer(TimeSpan.FromSeconds(3))
                    .Do(_ => ReviveOnLastSavedPosition(new Vector3(x, y, z)))
                    .Subscribe();
            }
            else
            {
                Observable
                    .Timer(TimeSpan.FromSeconds(3))
                    .Do(_ => ReviveOnLastSavedPosition(_initialPlayerPosition))
                    .Subscribe();
            }
        }
    }

    private void ReviveOnLastSavedPosition(Vector3 savedPosition)
    {
        _player = Instantiate(_playerRespawnPrefab);
        _player.transform.position = savedPosition;
    }
}
