using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleStage : MonoBehaviour
{

    public const int life_stage = 10;

    public GameObject prefab;

    private Castle attached_castle;
    private GameObject attachedObject;

    public int life_point = life_stage;

    public void Init(Castle attached_castle, GameObject attachedObject, Vector3 position = new Vector3())
    {
        this.attached_castle = attached_castle;
        this.attachedObject = attachedObject;
        
        gameObject.transform.position = (position - Vector3.up * GetHeight() / 2);

        Debug.Log("A stage has been created with " + attached_castle.name);
    }

    public void ReceiveDamage(int _amount)
    {
        TakeDamage(_amount);
    }

    public int GetLifePoints()
    {
        return life_point;
    }

    public Collider GetCollider()
    {
        return this.gameObject.GetComponent<BoxCollider>();
    }

    public GameObject GetGameObject()
    {
        return this.gameObject;
    }

    public Vector3 GetPosition()
    {
        return this.gameObject.transform.position;
    }

    public float GetHeight()
    {
        var collider = gameObject.GetComponent<BoxCollider>();
        return collider.size.y* gameObject.transform.localScale.y;
    }

    void TakeDamage(int _amount)
    {
        life_point -= _amount;
        if (life_point <= 0)
        {
            Destroy(this.gameObject);
            int remaining_damage = life_point * -1;
            attached_castle.DestroyStage(remaining_damage, this);
        }

        //Dimiuer le height et baisser la position de attachedObject
    }
}
