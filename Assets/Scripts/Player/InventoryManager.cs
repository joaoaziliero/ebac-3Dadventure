using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using R3.Triggers;
using R3;
using Unity.VisualScripting;
using System.Linq;

public class InventoryManager : MonoBehaviour
{
    [Serializable]
    public class Item
    {
        public string tag;
        public string associatedFunction = "Empty";
        public KeyCode key = KeyCode.None;
        public TextMeshProUGUI userInterface;
    }

    public List<Item> inventory;
    private ItemFunctions _functions;
    private Collider _playerCollider;
    
    private void Awake()
    {
        _functions = transform.AddComponent<ItemFunctions>();
        _playerCollider = GetComponentInParent<Collider>();
    }

    private void Start()
    {
        _playerCollider
            .OnTriggerEnterAsObservable()
            .Select(collider => inventory.Where(item => collider.gameObject.CompareTag(item.tag)).ToList())
            .Subscribe(collectedItems => collectedItems.ForEach(i => i.userInterface.text = UpdatedCount(i.userInterface, 1)))
            .AddTo(this);

        Observable
            .EveryUpdate()
            .Select(_ => inventory
            .Where(item => Input.GetKeyDown(item.key) && int.Parse(item.userInterface.text) > 0)
            .ToList())
            .Subscribe(activatedItems => activatedItems.ForEach(i =>
            {
                i.userInterface.text = UpdatedCount(i.userInterface, -1);
                _functions.Invoke(i.associatedFunction, 0);
            }))
            .AddTo(this);
    }

    private string UpdatedCount(TextMeshProUGUI userInterface, int value)
    {
        return (int.Parse(userInterface.text) + value).ToString();
    }
}
