using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SpaceshipManager : MonoBehaviour
{
    public SpaceshipDB spaceshipDB;
    public SpriteRenderer artworkSprite;
    private int selectedOption = 0;    // Start is called before the first frame update
    void Start()
    {
        if (PlayerPrefs.HasKey("selectedOption"))
        {
            Load();
        }
        else
        {
            selectedOption = 0;
        }
        UpdateSpaceship(selectedOption);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.KeypadEnter) || Input.GetKeyDown(KeyCode.Return))
        {
            StartLevel(0);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            NextOption();
        }
        else if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            BackOption();
        }
    }

    public void NextOption()
    {
        selectedOption++;
        if (selectedOption >= spaceshipDB.SpaceshipCount)
        {
            selectedOption = 0;
        }
        Save();
        UpdateSpaceship(selectedOption);
    }

    public void BackOption()
    {
        selectedOption--;
        if (selectedOption < 0)
        {
            selectedOption = spaceshipDB.SpaceshipCount - 1;
        }
        Save();
        UpdateSpaceship(selectedOption);
    }

    private void UpdateSpaceship(int selectedOption)
    {
        Spaceship spaceship = spaceshipDB.GetSpaceship(selectedOption);
        artworkSprite.sprite = spaceship.spaceshipSprite;
    }

    private void Load()
    {
        selectedOption = PlayerPrefs.GetInt("selectedOption");
    }

    private void Save()
    {
        PlayerPrefs.SetInt("selectedOption", selectedOption);
    }

    public void StartLevel(int sceneId)
    {
        SceneManager.LoadScene(sceneId);
    }
}
