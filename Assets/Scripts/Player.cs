using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private bool _gameOver = false;
    Rigidbody _rigid;
    private float _speed = 5f;
    private float _speedRotation = 1000f;
    private int _gunPower = 1;
    public static float borderLimitX;
    public static float borderLimitY;
    public GameObject bulletPrefab;
    public GameObject gunMain;
    public GameObject gunLeft;
    public GameObject gunRigh;

    public Vector3 targetVector;
    public static int score;
    public SpaceshipDB spaceshipDB;
    public SpriteRenderer artworkSprite;
    public Animator animator;
    private int selectedOption = 0;    // Start is called before the first frame update

    // Audio
    private AudioSource _audioSource;
    void Start()
    {
        _audioSource = GetComponent<AudioSource>();
        _rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();

        borderLimitX = Camera.main.orthographicSize + 4;
        borderLimitY = (Camera.main.orthographicSize + 1) * Screen.width / Screen.height;

        PrepareSpaceship();
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
        animator.runtimeAnimatorController = spaceship.animatorController;
        _gunPower = spaceship.gunPower;
        _speed = spaceship.speed;
        _speedRotation = 100f * spaceship.rotationSpeed;
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
            shoot();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Hangar");
        }

        checkGameOver();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
        {
            _gameOver = true;
            if (_audioSource != null)
            {
                _audioSource.Play();
            }
            else
            {
                resetLevel();
            }

        }
    }

    private void checkGameOver()
    {
        if (_gameOver && _audioSource != null && !_audioSource.isPlaying)
        {
            resetLevel();
        }

    }

    private void resetLevel()
    {
        _gameOver = false;
        Application.LoadLevel(Application.loadedLevel);
        score = 0;
    }

    private void shoot()
    {
        switch (_gunPower)
        {
            case 1:
                createBullet(gunMain);
                break;
            case 2:
                createBullet(gunLeft);
                createBullet(gunRigh);
                break;
            case 3:
                createBullet(gunMain);
                createBullet(gunLeft);
                createBullet(gunRigh);
                break;
            case 4:
                createBullet(gunMain);
                createBullet(gunLeft);
                createBullet(gunRigh);
                break;

        }
    }

    private void createBullet(GameObject gun)
    {
        GameObject bullet = Instantiate(bulletPrefab, gun.transform.position, Quaternion.identity);
        bullet.transform.localScale = new Vector3(0.05f * _gunPower, 0.05f * _gunPower, 0.05f);
        Bullet bulletScript = bullet.GetComponent<Bullet>();
        bulletScript.targetVector = transform.up;
        bulletScript.gunPower = _gunPower;
    }
}
