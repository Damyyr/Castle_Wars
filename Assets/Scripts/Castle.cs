using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Castle : MonoBehaviour
{
    public CastleStage stagePrefab;

    public int amountDamagePerHit = 5;

    public int lifePoint = 100;
    private int nb_stages;

    private Stack<CastleStage> stages = new Stack<CastleStage>();

    // Use this for initialization
    void Start()
    {
        //for (int i = 0; i < CalculateStage(); i++)
        //{
        //    var collider = (nb_stages <= 0) ? gameObject.GetComponent<BoxCollider>() : stages.Peek().GetCollider();
        //    var last_collider = collider != null ? collider.bounds : new Bounds();
        //    var last_position = (nb_stages <= 0) ? gameObject.transform.position : stages.Peek().GetPosition();

        //    var _gameObject = (nb_stages <= 0) ? gameObject : stages.Peek().GetGameObject();

        //    var height = last_collider.extents.y * _gameObject.transform.localScale.y;

        //    Debug.Log(height);

        //    stages.Push(ScriptableObject.CreateInstance("CastleStage") as CastleStage);
        //    stages.Peek().init(this, stagePrefab, new Vector3(0, last_position.y + height - 50, 0));
        //    nb_stages++;
        //}

        var last_collider = gameObject.GetComponent<BoxCollider>();
        var height = last_collider.size.y * gameObject.transform.localScale.y;
        var height2 = stagePrefab.GetComponent<BoxCollider>().size.y * stagePrefab.gameObject.transform.localScale.y;
        
        var posY = gameObject.transform.position.y;

        var instance = Instantiate(stagePrefab) as CastleStage;
        instance.Init(this, instance.gameObject, Vector3.up * (posY - height / 2));

        Debug.Log("allo " + gameObject.GetComponent<BoxCollider>());

        //Debug.Log("Castle " + this.name + " at " + nb_stages + " lifes.");
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ReceiveDamage(amountDamagePerHit);
        }

        if (Input.GetMouseButtonDown(1))
        {
            this.gameObject.transform.Translate(new Vector3(0, 1, 0));
            Debug.Log(gameObject.GetComponent<MeshFilter>().mesh.bounds);
        }
        UpdateLifePoints();
    }

    public void ReceiveDamage(int _amount)
    {
        TakeDamage(_amount);
    }

    void TakeDamage(int _amount)
    {
        Debug.Log(this.name + "just took " + _amount + " damage!");

        //CastleStage concerned_stage = stages.Peek();

        //concerned_stage.ReceiveDamage(_amount);
    }

    public void DestroyStage(int remaining_damages, CastleStage _stage)
    {
        Debug.Log("One stage has to be destroyed.");

        TakeDamage(remaining_damages);
    }

    void UpdateLifePoints()
    {
        int life = 0;
        foreach (var stage in stages)
        {
            life += stage.GetLifePoints();
        }

        lifePoint = life;
    }

    int CalculateStage()
    {
        return (lifePoint / CastleStage.life_stage);
    }
}
