using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    [Serializable]
    public class Clothing
    {
        public Material material;
        public KeyCode key = KeyCode.None;
        public string associatedFunction = "Normalize";
    }

    public List<Clothing> clothes;
    public List<SkinnedMeshRenderer> body;

    private ClothingFunctions _functions;

    private void Awake()
    {
        _functions = transform.AddComponent<ClothingFunctions>();
    }

    private void Start()
    {
        var clothingActivationStream = Observable
            .EveryUpdate()
            .Select(_ => clothes
            .Where(clothingItem => Input.GetKeyDown(clothingItem.key)).ToList())
            .Where(activatedClothing => activatedClothing.Count > 0)
            .Subscribe(activatedClothing =>
            {
                body.ForEach(part => part.material = activatedClothing[0].material);
                _functions.Invoke(activatedClothing[0].associatedFunction, 0);
            })
            .AddTo(this);
    }
}
