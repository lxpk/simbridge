using UnityEngine;
using System.Collections;

public class MovieTexturePlay : MonoBehaviour {
#if UNITY_STANDALONE_OSX
    public MovieTexture movTexture;
    void Start() {
        GetComponent<Renderer>().material.mainTexture = movTexture;
		movTexture.loop = true;
        movTexture.Play();
    }
#endif
}