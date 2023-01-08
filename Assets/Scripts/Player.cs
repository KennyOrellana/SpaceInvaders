using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    Rigidbody _rigid;
    public float _speed = 5f;
    public float _speedRotation = 1000f;
    public static float borderLimitX;
    public static float borderLimitY;
    public GameObject bulletPrefab;
    public GameObject gun;

    public Vector3 targetVector;
    public static int score;
    public SpaceshipDB spaceshipDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;    // Start is called before the first frame update
    void Start()
    {
        PrepareSpaceship();

        _rigid = GetComponent<Rigidbody>();

        borderLimitX = Camera.main.orthographicSize + 4;
        borderLimitY = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;
    }

    private void PrepareSpaceship()
    {
        if (PlayerPrefs.HasKey("selectedOption"))
        {
            selectedOption = PlayerPrefs.GetInt("selectedOption");
        }
        else
        {
            selectedOption = 0;
        }
        UpdateSpaceship(selectedOption);
    }

    private void UpdateSpaceship(int selectedOption)
    {
        Spaceship spaceship = spaceshipDB.GetSpaceship(selectedOption);
        artworkSprite.sprite = spaceship.spaceshipSprite;
    }

    private void FixedUpdate()
    {
        float rotation = Input.GetAxis("Rotate");
        float forward = Input.GetAxis("Forward");
        transform.Rotate(Vector3.forward, -rotation * _speedRotation * Time.deltaTime);
        _rigid.AddForce(forward * transform.up * _speed);
    }

    // Update is called once per frame
    void Update()
    {
        // Adjust position when move out of boundaries
        var adjustedPosition = transform.position;
        if (adjustedPosition.x > borderLimitX)
        {
            adjustedPosition.x = -borderLimitX + 1;
        }
        else if (adjustedPosition.x < -borderLimitX)
        {
            adjustedPosition.x = borderLimitX - 1;
        }
        else if (adjustedPosition.y > borderLimitY)
        {
            adjustedPosition.y = -borderLimitY + 1;
        }
        else if (adjustedPosition.y < -borderLimitY)
        {
            adjustedPosition.y = borderLimitY - 1;
        }
        transform.position = adjustedPosition;

        // Set targetVector for bullet's direction
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
            Bullet bulletScript = bullet.GetComponent<Bullet>();
            bulletScript.targetVector = transform.up;
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            Application.LoadLevel(Application.loadedLevel);
            score = 0;
        }
    }
}
