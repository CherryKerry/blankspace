using UnityEngine;
using System.Collections;

public class RainController : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}

    public float scrollSpeed = 0.5F;
    void Update()
    {
        float offset = Time.time * scrollSpeed;
        renderer.material.SetTextureOffset("rain", new Vector2(0, offset));
    }

}
