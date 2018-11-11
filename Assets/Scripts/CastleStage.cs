using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStage : ACastlePart
{

    public const int canHandle = 10;
    private float remainingLife = canHandle;

    private Castle _attachedCastle;


    override public void Start()
    {
        ReductionPerShot = gameObject.transform.localScale.y / canHandle;
    }

    public override void Update()
    {
        //throw new System.NotImplementedException();
    }

    //public void Init(Castle attachedCastle, ACastlePart attachedObject, Vector3 position = new Vector3())
    //{
    //    gameObject.transform.SetParent(_attachedCastle.Parent, false);
    //    _attachedCastle = attachedCastle;
    //    AttachedObject = attachedObject;

    //    gameObject.transform.position = (position - Vector3.up * Height / 2);

    //    Debug.Log("A stage has been created with " + _attachedCastle.name);
    //}

    public override void Init(GameObject parent, ACastlePart attachedObject, Castle attachedCastle = null, Vector3 position = default(Vector3))
    {
        gameObject.transform.SetParent(parent.transform, false);
        _attachedCastle = attachedCastle;
        AttachedObject = attachedObject;

        gameObject.transform.position = (position - Vector3.up * Height / 2);

        Debug.Log("A stage has been created with " + attachedCastle.name);
    }

    public void ReceiveDamage(int amount)
    {
        TakeDamage(amount);
    }

    public float GetRemainingLife()
    {
        return remainingLife;
    }

    void TakeDamage(int amountDamage)
    {
        remainingLife += amountDamage;
        if (remainingLife <= 0)
        {
            int remainingDamage = (int)remainingLife * -1;
            _attachedCastle.DestroyStage(remainingDamage, this);
        }
        else
        {
            gameObject.transform.localScale += Vector3.up * (amountDamage * ReductionPerShot);
        }
        //Dimiuer le height et baisser la position de attachedObject
    }

    //public void RepositionStages(int nbStage)
    //{
    //    if (AttachedObject != null)
    //    {
    //        AttachedObject.RepositionStages(nbStage + 1);
    //    }
    //    gameObject.transform.position -= Vector3.up * ReductionPerShot * nbStage;
    //}

    private void OnMouseDown()
    {
        _attachedCastle.ReceiveDamage();
    }
}
