using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ACastlePart : MonoBehaviour
{
    protected readonly int _canHandle;
    protected readonly float _remainingLife;
    protected int CanHandle
    {
        get
        {
            return _canHandle;
        }
    }
    protected float RemainingLife
    {
        get
        {
            return _remainingLife;
        }
    }
    public float ReductionPerShot { get; protected set; }

    protected ACastlePart AttachedObject { get; set; }
    public float Height
    {
        get
        {
            var collider = gameObject.GetComponent<BoxCollider>();
            return collider.size.y * gameObject.transform.localScale.y;
        }
    }
    
    public abstract void Start();
    public abstract void Update();
    public abstract void Init(GameObject parent, ACastlePart attachedObject, Castle attachedCastle = null, Vector3 position = new Vector3());
}
