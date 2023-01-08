using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet : MonoBehaviour
{
    public Vector3 targetVector;
    public int speed = 10;
    float maxLifeTime = 3;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetVector * speed * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Destroy(collision.gameObject);
            Destroy(gameObject);
            Player.score++;
            RefreshUI();
        }
    }

    private void RefreshUI()
    {
        GameObject go = GameObject.FindGameObjectWithTag("Score");
        if (go != null)
        {
            go.GetComponent<TextMeshProUGUI>().text = "Score: " + Player.score;
        }

    }
}
