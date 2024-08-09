using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MissionDisplayPanel : MonoBehaviour
{
    [SerializeField] Image thumbnail;
    [SerializeField] TextMeshProUGUI hintsText;
    [SerializeField] TextMeshProUGUI missionStatusText;

    public ObjectiveData data;

    // Start is called before the first frame update
    void Start()
    {
        thumbnail.sprite = data.objectiveIcon;
        hintsText.text = data.objectiveDescription;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.instance.MissionComplete == true)
        {
            missionStatusText.text = "STATUS: MISSION COMPLETE!";
        }
        else
        {
            missionStatusText.text = "STATUS: SEARCHING...";
        }
    }
}
