using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class move : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}

    public Texture2D[] frames;
    public int fps = 10;

    void Update()
    {
        int index = (int)(Time.time * fps) % frames.Length;
        GetComponent<RawImage>().texture = frames[index];
    }
}
