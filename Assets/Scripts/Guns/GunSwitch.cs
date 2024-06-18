using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunSwitch : MonoBehaviour
{
    [System.Serializable]
    private class GunSelectionByKey
    {
        public KeyCode selectionKey;
        public GunBase correspondingGun;
    }

    [SerializeField] private List<GunSelectionByKey> _selection;
    private GunBase _currentGun;

    private void Update()
    {
        foreach (var pairing in _selection)
        {
            if (Input.GetKeyDown(pairing.selectionKey))
            {
                if (_currentGun != null) _currentGun.enabled = false;
                pairing.correspondingGun.enabled = true;
                _currentGun = pairing.correspondingGun;
            }
        }
    }
}
