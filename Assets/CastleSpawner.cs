using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastleSpawner : MonoBehaviour {

    public Castle castlePrefab;

	// Use this for initialization
	void Start () {
        var instance = Instantiate(castlePrefab);
        instance.Init(this.gameObject, null, null, this.gameObject.transform.position);
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
