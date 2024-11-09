using R3;
using R3.Triggers;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;

public class DestructibleChest : ChestBase
{
    public Color albedoColor = Color.white;
    public string projectileTag = "Projectile";
    public int shotsToDestroy;
    public List<GameObject> chestContents;

    private SphereCollider _trigger;
    private List<MeshRenderer> _rendererComponents;

    public void Awake()
    {
        if (shotsToDestroy <= 0) shotsToDestroy = 1;
        _trigger = GetComponent<SphereCollider>();
        _rendererComponents = GetComponentsInChildren<MeshRenderer>().ToList();
        _rendererComponents.ForEach(mr => mr.material.color = albedoColor);
    }

    public void Start()
    {
        _trigger
            .OnTriggerEnterAsObservable()
            .Where(collider => collider.gameObject.CompareTag(projectileTag))
            .Select(collision => 1)
            .Scan(0, (currentSum, collisionCounter) => currentSum + collisionCounter)
            .Where(sum => sum == shotsToDestroy)
            .Subscribe(_ => AwardChestContent())
            .AddTo(this);
    }

    public override void AwardChestContent()
    {
        chestContents.ForEach(obj => obj.transform.SetParent(null, true));
        chestContents.ForEach(obj => obj.GetComponent<BoxCollider>().enabled = true);
        chestContents.ForEach(obj => obj.GetComponent<Rigidbody>().useGravity = true);
        DeactivateChest();
    }

    public override void DeactivateChest()
    {
        Destroy(gameObject, 1);
    }
}
