using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class hitanimal : MonoBehaviour
{
    public int damage = 10;
    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.localPosition = new Vector3(0.01f, 0.1f, 1f);
        transform.localEulerAngles = new Vector3(0f, 0f, 0f);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.gameObject.layer == 12) return;
        int speed = (int)player.GetComponent<set_position>().getspeed();
        StartCoroutine(hitann(speed,collision));
    }

    IEnumerator hitann(int speed, Collision collision)
    {
        yield return new WaitForSeconds(0.1f);
        bool hitAnimal = collision.collider.gameObject.tag == "Animal";
        if (hitAnimal)
        {
            print("HIT!!!");
            collision.collider.gameObject.GetComponent<EmeraldAI.EmeraldAISystem>().Damage(damage*speed, EmeraldAI.EmeraldAISystem.TargetType.Player, transform.root, 200);
        }
    }
}
