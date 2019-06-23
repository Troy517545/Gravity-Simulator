using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escMenuManager : MonoBehaviour
{
    public GameObject escMenu;
    cameraMovement cameraMovementScript;
    starSystemObject starSystemObjectScript;
    public GameObject dropdownMenuOptionLabelObject;
    public GameObject menuSliderObject;
    public GameObject timeStepTextObject;
    public GameObject InputFieldObject;
    initialConditions initialConditionsScript;

    private Dictionary<string, int> map = new Dictionary<string, int>();
    private float oldSliderValue; 

    // Start is called before the first frame update
    void Start()
    {
        cameraMovementScript = GameObject.Find("Main Camera").GetComponent<cameraMovement>();
        starSystemObjectScript = GameObject.Find("starSystemObject").GetComponent<starSystemObject>();
        initialConditionsScript = GameObject.Find("starSystemObject").GetComponent<initialConditions>();

        map.Add("Figure-8", 1);
        map.Add("Figure-8 with little bias", 2);
        map.Add("3 bodies special stable orbit", 3);
        map.Add("2 bodies stable", 4);
        map.Add("3 bodies with collisions", 5);

        oldSliderValue = (float)starSystemObjectScript.h;
        menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value = oldSliderValue;
        InputFieldObject.GetComponent<UnityEngine.UI.InputField>().text = menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        if(menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value != oldSliderValue)
        {
            oldSliderValue = menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value;
            InputFieldObject.GetComponent<UnityEngine.UI.InputField>().text = menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value.ToString();
            starSystemObjectScript.h = oldSliderValue;
        }
    }

    public void closeEscMenu()
    {
        cameraMovementScript.escMenuActiveStatus = false;
        escMenu.SetActive(false);
        starSystemObjectScript.startSystem();
    }

    public void terminateProgram()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void acceptDropdownMenuConditions()
    {
        starSystemObjectScript.clearAllStars(-1);
        cameraMovementScript.escMenuActiveStatus = false;
        escMenu.SetActive(false);
        initialConditionsScript.setInitialConditions(map[dropdownMenuOptionLabelObject.GetComponent<UnityEngine.UI.Text>().text]);
        starSystemObjectScript.startSystem();
    }

    public void updateSliderWithInputFieldValue()
    {
        oldSliderValue = float.Parse(InputFieldObject.GetComponent<UnityEngine.UI.InputField>().text);
        menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value = oldSliderValue;
        starSystemObjectScript.h = oldSliderValue;
    }
}
