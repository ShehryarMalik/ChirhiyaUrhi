using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MapDisplay : MonoBehaviour
{
    [SerializeField] private TMP_Text mapName, mapDescription;
    [SerializeField] Image mapImage;
    [SerializeField] GameObject lockIcon;
    [SerializeField] Button playButton;

    public void DisplayMap(Map map)
    {
        mapName.text = map.mapName;
        mapDescription.text = map.mapDescription;
        mapImage.sprite = map.mapImage;

        bool mapUnlocked = PlayerPrefs.GetInt("currentScene", 0) >= map.mapIndex;

        lockIcon.SetActive(!mapUnlocked);
        playButton.interactable = mapUnlocked;

        if (mapUnlocked)
            mapImage.color = Color.white;
        else
            mapImage.color = Color.gray;

        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => SceneManager.LoadScene(map.sceneToLoad));
    }
}
