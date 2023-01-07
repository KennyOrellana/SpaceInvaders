using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rigid;
    public float _speed = 5f;
    public GameObject bulletPrefab;
    public GameObject gun;

    public Vector3 targetVector;

    // Start is called before the first frame update
    void Start()
    {
        _rigid = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        float rotation = Input.GetAxis("Rotate");
        float forward = Input.GetAxis("Forward");

        transform.Rotate(Vector3.forward, -rotation * _speed);
        _rigid.AddForce(forward * transform.right * _speed);

        if (Input.GetKeyDown(KeyCode.Space)){
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = transform.right;
        }
    }

    private void OnCollisionEnter(Collision collision) {
        if(collision.gameObject.tag == "Enemy") {
            Application.LoadLevel(Application.loadedLevel);
        }
    }
}
