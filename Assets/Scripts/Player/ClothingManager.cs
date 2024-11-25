using R3;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ClothingManager : MonoBehaviour
{
    public int SkinIndex { get; private set; }
    public List<Clothing> clothes;
    public List<SkinnedMeshRenderer> body;

    private ClothingFunctions _functions;

    private void Awake()
    {
        _functions = GetComponent<ClothingFunctions>();
    }

    private void Start()
    {
        Observable
            .EveryUpdate()
            .Select(_ => clothes
            .Where(clothingItem => Input.GetKeyDown(clothingItem.key)).ToList())
            .Where(activatedClothing => activatedClothing.Count > 0)
            .Subscribe(activatedClothing => SelectSkin(activatedClothing[0]))
            .AddTo(this);
    }

    public void SelectSkin(Clothing skin)
    {
        body.ForEach(part => part.material = skin.material);
        if (_functions != null) _functions.Invoke(skin.associatedFunction, 0);
        SkinIndex = clothes.IndexOf(skin);
    }

    [Serializable]
    public class Clothing
    {
        public Material material;
        public KeyCode key = KeyCode.None;
        public string associatedFunction = "Normalize";
    }
}
