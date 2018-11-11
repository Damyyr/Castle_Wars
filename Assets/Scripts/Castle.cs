using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : ACastlePart
{
    public CastleStage stagePrefab;

    public int amountDamagePerHit = 5;

    public float lifePoint = 100;
    private int NbStages;

    private Stack<CastleStage> stages = new Stack<CastleStage>();

    override public void Start()
    {
        //gameObject.transform.position = _spawnLocation.gameObject.transform.position + Vector3.up * (gameObject.transform.localScale.y / 2);
        //Parent = _spawnLocation.transform;

        //ReductionPerShot = 0;
        //AttachedObject = null;

        //gameObject.transform.position += Vector3.up * stagePrefab.transform.localScale.y * CalculateStage();
        //for (int i = 0; i < CalculateStage(); i++)
        //{
        //    var instance = Instantiate(stagePrefab) as CastleStage;

        //    var target = (stages.Count <= 0) ? this : stages.Peek() as ACastlePart;
        //    var targetCollider = target.GetComponent<BoxCollider>();

        //    var posX = target.gameObject.transform.position.x;
        //    var posY = target.gameObject.transform.position.y;
        //    var posZ = target.gameObject.transform.position.z;
            
        //    var height = targetCollider.size.y * target.gameObject.transform.localScale.y;
        //    var centersDifference = targetCollider.center.y + posY;

        //    var nextPositon = new Vector3(posX, (centersDifference - height / 2), posZ);
        //    instance.Init(this, target, nextPositon);
        //    stages.Push(instance);

        //}

        //Debug.Log("Castle " + this.name + " at " + NbStages + " lifes.");
    }

    override public void Update()
    {

    }

    public override void Init(GameObject parent, ACastlePart attachedObject, Castle attachedCastle = null, Vector3 position = default(Vector3))
    {
        gameObject.transform.SetParent(parent.transform);
        gameObject.transform.position = position + Vector3.up * (gameObject.transform.localScale.y / 2);

        ReductionPerShot = 0;
        AttachedObject = null;

        gameObject.transform.position += Vector3.up * stagePrefab.transform.localScale.y * CalculateStage();
        for (int i = 0; i < CalculateStage(); i++)
        {
            var instance = Instantiate(stagePrefab) as CastleStage;

            var target = (stages.Count <= 0) ? this : stages.Peek() as ACastlePart;
            var targetCollider = target.GetComponent<BoxCollider>();

            var posX = target.gameObject.transform.position.x;
            var posY = target.gameObject.transform.position.y;
            var posZ = target.gameObject.transform.position.z;

            var height = targetCollider.size.y * target.gameObject.transform.localScale.y;
            var centersDifference = targetCollider.center.y + posY;

            var nextPositon = new Vector3(posX, (centersDifference - height / 2), posZ);
            instance.Init(parent, target, this, nextPositon);
            stages.Push(instance);

        }
    }

    private void OnMouseDown()
    {
        ReceiveDamage();
        UpdateLifePoints();
    }

    private bool CheckForDamage()
    {
        return true;
    }

    private void CreatePlane(Vector3 position, Color color)
    {
        var plane = GameObject.CreatePrimitive(PrimitiveType.Plane);
        plane.transform.position = position;
        plane.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);
        plane.GetComponent<Renderer>().material.color = color;

    }

    public void ReceiveDamage()
    {
        TakeDamage(amountDamagePerHit);
    }

    void TakeDamage(int amountDamage)
    {
        amountDamage = amountDamage > 0 ? amountDamage * -1 : amountDamage;
        Debug.Log(this.name + " just took " + amountDamage + " damage!");

        CastleStage concernedStage = stages.Peek();

        concernedStage.ReceiveDamage(amountDamage);
        RepositionStages(amountDamage, concernedStage);
    }

    public void DestroyStage(int remaining_damages, CastleStage _stage)
    {
        Debug.Log("One stage has to be destroyed.");

        stages.Pop();
        Destroy(_stage.gameObject);
        TakeDamage(remaining_damages);
    }

    void UpdateLifePoints()
    {
        float life = 0;
        foreach (var stage in stages)
        {
            life += stage.GetRemainingLife();
        }

        lifePoint = life;
    }

    float CalculateStage()
    {
        return lifePoint / CastleStage.canHandle;
    }

    public void RepositionStages(int amountDamage, ACastlePart source)
    {
        var movement = Vector3.up * (amountDamage * source.ReductionPerShot);

        foreach (CastleStage stage in stages)
        {
            if (stage == source)
                stage.gameObject.transform.position += Vector3.up * (amountDamage * source.ReductionPerShot / 2);
            else
                stage.gameObject.transform.position += Vector3.up * (amountDamage * source.ReductionPerShot);
        }

        gameObject.transform.position += movement;
    }
}
