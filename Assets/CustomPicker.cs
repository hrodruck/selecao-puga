using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class CustomPicker : MonoBehaviour
{
    [Header("References")]
    public Image cannonLeftArrow;
    public Image cannonRightArrow;
    public Text cannonTitleText;
    public Text cannonContentText;

    public Image droneLeftArrow;
    public Image droneRightArrow;
    public Text droneTitleText;
    public Text droneContentText;

    [Header("Settings")]
    public string[] cannonNames;
    public string[] droneNames;
    public Color highlightColor;

    [Header("Behaviour")]
    private int state=0;
    private Color originalColor;
    private int selectedCannon=0;
    private int selectedDrone=0;
    private bool canSelectAgain;
        
    void Start()
    {
        if (cannonNames.Length > 0)
        {
            cannonContentText.text = cannonNames[0];
        }
        if (droneNames.Length > 0)
        {
            droneContentText.text = droneNames[0];
        }
        if (highlightColor==null)
        {
            highlightColor = Color.black;
        }
    }

    void Update()
    {
        if (Input.GetAxis("Horizontal") == 0)
        {
            canSelectAgain = true;
        }
        if (Input.GetButtonDown("Submit"))
        {
            state++;
            switch (state)
            {
                case 1:
                    highlight(cannonLeftArrow, cannonRightArrow, cannonTitleText);
                    break;
                case 2:
                    unlight(cannonLeftArrow, cannonRightArrow, cannonTitleText);
                    highlight(droneLeftArrow, droneRightArrow, droneTitleText);
                    break;
                case 3:
                    unlight(droneLeftArrow, droneRightArrow, droneTitleText);
                    SceneManager.Instance.goToTest(selectedCannon, selectedDrone);
                    break;
            }
        }
        else if (state==1 && Input.GetAxis("Horizontal") > 0 && selectedCannon<cannonNames.Length-1 && canSelectAgain)
        {
            selectedCannon++;
            cannonContentText.text = cannonNames[selectedCannon];
            canSelectAgain = false;
        }
        else if (state == 1 && Input.GetAxis("Horizontal") < 0 && selectedCannon >0 && canSelectAgain)
        {
            selectedCannon--;
            cannonContentText.text = cannonNames[selectedCannon];
            canSelectAgain = false;
        }
        else if (state == 2 && Input.GetAxis("Horizontal") > 0 && selectedDrone < droneNames.Length - 1 && canSelectAgain)
        {
            selectedDrone++;
            droneContentText.text = droneNames[selectedDrone];
            canSelectAgain = false;
        }
        else if (state == 2 && Input.GetAxis("Horizontal") < 0 && selectedDrone > 0 && canSelectAgain)
        {
            selectedDrone--;
            droneContentText.text = droneNames[selectedDrone];
            canSelectAgain = false;
        }
    }
    private void highlight(Image leftArrow, Image rightArrow, Text title)
    {
        originalColor = title.color;
        leftArrow.color = highlightColor;
        rightArrow.color = highlightColor;
        title.color = highlightColor;
    }
    private void unlight(Image leftArrow, Image rightArrow, Text title)
    {
        leftArrow.color = originalColor;
        rightArrow.color = originalColor;
        title.color = originalColor;
    }
}
