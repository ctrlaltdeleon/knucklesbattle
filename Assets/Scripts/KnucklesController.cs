using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class KnucklesController : NetworkBehaviour
{
    public float speed;
    public GameObject tower;
    public int type;

    public float hp;
    [SerializeField] public float attackDmg;

    public float GetAttackDmg
    {
        get { return attackDmg; }
    }

    public Slider knucklesHPSlider;

    //For spitting
    public Transform startPosition;

    public GameObject spitPrefab;
    private Vector3 towerPosition;
    float startTime;
    public GameObject explosion;

    public AudioClip[] ugandanDialect;
    public AudioClip spit;
    public AudioClip oof;
    public AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        towerPosition = new Vector3(0, 0, 0);

        // Set attributes based on color
        switch (type)
        {
            case (int) Knuckles.Red: //Normal
                speed = 4f;
                hp = 45;
                attackDmg = 1f;
                break;
            case (int) Knuckles.Blue: //Slow, heavy damage
                speed = 3f;
                hp = 145;
                attackDmg = 5f;
                break;
            case (int) Knuckles.Green: //Projectile/Spit
                speed = 4f;
                hp = 27;
                attackDmg = 2f;
                break;
            case (int) Knuckles.Orange: //Fast
                speed = 6f;
                hp = 23;
                attackDmg = 0.5f;
                break;
        }

        InvokeRepeating("SpeakUgandan", Random.Range(1f, 3f), Random.Range(5f, 8f));
    }

    public void SpeakUgandan()
    {
        audioSource.PlayOneShot(ugandanDialect[Random.Range(0, ugandanDialect.Length)]);
    }

    // Update is called once per frame
    void Update()
    {
        if (type == (int) Knuckles.Green)
        {
            if (Vector3.Distance(tower.transform.position, transform.position) > 15)
            {
                MoveTowards(tower.transform.position);
                startTime = Time.time;
            }
            else
            {
                if (Time.time - startTime > 3f)
                {
                    Spit();
                    startTime = Time.time;
                }
            }
        }
        else
        {
            MoveTowards(tower.transform.position);
        }
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


    private void OnCollisionEnter(Collision other)
    {
        //Debug.Log ("In OnCollisionEnter");
        if (other.gameObject.CompareTag("PlayerBullet"))
        {
            audioSource.PlayOneShot(oof);
            if (!isServer)
            {
                return;
            }
            float newSliderValue = 0;

            float newHealth = hp - other.gameObject.GetComponent<PlayerBullet>().BulletDamage; //15-10 = 5

            switch (type)
            {
                case (int) Knuckles.Red:
                    newSliderValue = newHealth / 45;
                    break;
                case (int) Knuckles.Blue:
                    newSliderValue = newHealth / 145;
                    break;
                case (int) Knuckles.Green:
                    newSliderValue = newHealth / 27;
                    break;
                case (int) Knuckles.Orange:
                    newSliderValue = newHealth / 23;
                    break;
            }

            knucklesHPSlider.value = newSliderValue;
            hp -= other.gameObject.GetComponent<PlayerBullet>().BulletDamage;
//            RpcTakeDamage(newSliderValue);
            if (hp <= 0)
            {
                GameObject explosion = Instantiate(this.explosion);
                explosion.transform.position = gameObject.transform.position;
                NetworkServer.Spawn(explosion);
                Destroy(gameObject); //Destroy knuckle 
                Destroy(explosion, 1.5f);
                LevelManager.Instance.numMonsters--;
            }
            Destroy(other.gameObject); //Destroy bullet
        }
    }

//    [ClientRpc]
//    public void RpcTakeDamage(float newSliderValue)
//    {
//        knucklesHPSlider.value = newSliderValue;
//    }


    //SPIT CODE FOR ORANGE KNUCKLE

    private void Spit()
    {
        //Vector3 gravityBase = new Vector3(0, -100, 0);
        audioSource.PlayOneShot(spit);
        GameObject ball = Instantiate(spitPrefab, startPosition.position, spitPrefab.transform.rotation);
        ball.GetComponent<SpitController>().m_KnucklesController = this;
        Rigidbody rb = ball.GetComponent<Rigidbody>();
        Vector3 gravityBase = new Vector3(0, -10, 0);
        rb.velocity = HitTargetByAngle(transform.position, towerPosition, gravityBase, 45f);
    }

    public static Vector3 HitTargetByAngle(Vector3 startPosition, Vector3 targetPosition, Vector3 gravityBase,
        float limitAngle)
    {
        if (limitAngle == 90 || limitAngle == -90)
        {
            return Vector3.zero;
        }

        Vector3 AtoB = targetPosition - startPosition;
        Vector3 horizontal = GetHorizontalVector(AtoB, gravityBase);
        float horizontalDistance = horizontal.magnitude;
        Vector3 vertical = GetVerticalVector(AtoB, gravityBase);
        float verticalDistance = vertical.magnitude * Mathf.Sign(Vector3.Dot(vertical, -gravityBase));

        float angleX = Mathf.Cos(Mathf.Deg2Rad * limitAngle);
        float angleY = Mathf.Sin(Mathf.Deg2Rad * limitAngle);

        float gravityMag = gravityBase.magnitude;

        if (verticalDistance / horizontalDistance > angleY / angleX)
        {
            return Vector3.zero;
        }

        float destSpeed = (1 / Mathf.Cos(Mathf.Deg2Rad * limitAngle)) * Mathf.Sqrt(
                              (0.5f * gravityMag * horizontalDistance * horizontalDistance) /
                              ((horizontalDistance * Mathf.Tan(Mathf.Deg2Rad * limitAngle)) - verticalDistance));
        Vector3 launch = ((horizontal.normalized * angleX) - (gravityBase.normalized * angleY)) * destSpeed;
        return launch;
    }

    public static Vector3 GetHorizontalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        Vector3 perpendicular = Vector3.Cross(AtoB, gravityBase);
        perpendicular = Vector3.Cross(gravityBase, perpendicular);
        output = Vector3.Project(AtoB, perpendicular);
        return output;
    }

    public static Vector3 GetVerticalVector(Vector3 AtoB, Vector3 gravityBase)
    {
        Vector3 output;
        output = Vector3.Project(AtoB, gravityBase);
        return output;
    }
}

enum Knuckles
{
    Red = 0,
    Blue = 1,
    Green = 2,
    Orange = 3
}