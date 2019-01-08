using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrolingBackground : MonoBehaviour {

    [SerializeField] float scrollSpeed = 0.5f;
    Material myMaterial;
    Vector2 offSet;

	// Use this for initialization
	void Start () {
        myMaterial = GetComponent<Renderer>().material;
        offSet = new Vector2(0f, scrollSpeed);
	}
	
	// Update is called once per frame
	void Update () {
        myMaterial.mainTextureOffset += offSet * Time.deltaTime;
	}
}
