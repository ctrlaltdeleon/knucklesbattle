using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KnucklesController : MonoBehaviour
{
    public List<Texture> colors;
    public float speed;
    public GameObject tower;
    public int type;
    public float hp;

    void Awake()
    {
        // Change the color of knuckles
        int randomColor = Random.Range(0, colors.Count);
        gameObject.transform.GetChild(1).GetComponent<Renderer>().materials[1].mainTexture =
            colors[randomColor];
        type = randomColor;

        // Set attributes based on color
        switch (type)
        {
            case (int) Knuckles.Red:
                speed = 10;
                hp = 20;
                break;
            case (int) Knuckles.Blue:
                gameObject.transform.localScale *= 2.5f;
                transform.GetChild(2).localScale /= 2.5f;
                speed = 7.5f;
                hp = 40;
                break;
            case (int) Knuckles.Green:
                speed = 10;
                hp = 15;
                break;
            case (int) Knuckles.Orange:
                speed = 30;
                hp = 15;
                break;
        }
    }

    // Update is called once per frame
    void Update()
    {
        MoveTowards(tower.transform.position);
    }


    //Moves towards target 
    void MoveTowards(Vector3 target)
    {
        gameObject.transform.rotation = Quaternion.Euler(0f, gameObject.transform.rotation.y, 0f);
        // Looks at the target
        gameObject.transform.LookAt(new Vector3(target.x, 0, target.z));
        gameObject.transform.position =
            Vector3.MoveTowards(gameObject.transform.position, new Vector3(target.x, 0, target.z),
                speed * Time.deltaTime);
    }
}

enum Knuckles
{
    Red = 0,
    Blue = 1,
    Green = 2,
    Orange = 3
}