using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Bullet : MonoBehaviour
{
    private bool _hitted = false;
    public Vector3 targetVector;
    public int speed = 10;
    public int gunPower = 1;
    float maxLifeTime = 3;

    [SerializeField]
    private AudioClip _audioClipDestroy;
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, maxLifeTime);
        _audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(targetVector * speed * Time.deltaTime);
        checkHitted();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            if (_audioSource != null)
            {
                _audioSource.PlayOneShot(_audioClipDestroy);
            }
            else
            {
                destroyObjects();
            }

            // Reduce meteor's life
            Meteor meteorScript = collision.gameObject.GetComponent<Meteor>();

            // Multiple bullets could hit the meteor so we must check that the meteor stills has life
            if (meteorScript.lifePoints > 0)
            {
                meteorScript.lifePoints -= gunPower;
                if (meteorScript.lifePoints <= 0)
                {
                    Player.score += gunPower;
                    Destroy(collision.gameObject);
                }
            }

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

    private void checkHitted()
    {
        if (_hitted && _audioSource != null && !_audioSource.isPlaying)
        {
            destroyObjects();
        }

    }

    private void destroyObjects()
    {
        Destroy(gameObject);
    }
}
