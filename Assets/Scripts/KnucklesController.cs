using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesController : MonoBehaviour
{
    public List<Texture> colors;

    void Awake()
    {
        int randomColor = Random.Range(0, colors.Count);
        gameObject.transform.GetChild(1).GetComponent<Renderer>().materials[1].mainTexture =
            colors[randomColor];
    }

    // Update is called once per frame
    void Update()
    {
    }
}