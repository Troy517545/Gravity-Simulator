using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Debug = UnityEngine.Debug;

public class escMenuManager : MonoBehaviour
{
    public GameObject escMenu;
    public GameObject tipMessage;
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

        tipMessage.SetActive(false);
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

    public void CloseEscMenu()
    {
        cameraMovementScript.escMenuActiveStatus = false;
        escMenu.SetActive(false);
        if(cameraMovementScript.systemRunningStatusBeforeMenuOpened == true)
        {
            starSystemObjectScript.StartSystem();
        }
    }

    public void TerminateProgram()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
            Application.Quit();
#endif
    }

    public void AcceptDropdownMenuConditions()
    {
        starSystemObjectScript.ClearAllStars(-1);
        cameraMovementScript.escMenuActiveStatus = false;
        escMenu.SetActive(false);
        initialConditionsScript.SetInitialConditions(map[dropdownMenuOptionLabelObject.GetComponent<UnityEngine.UI.Text>().text]);
        starSystemObjectScript.StartSystem();
    }

    public void UpdateSliderWithInputFieldValue()
    {
        oldSliderValue = float.Parse(InputFieldObject.GetComponent<UnityEngine.UI.InputField>().text);
        menuSliderObject.GetComponent<UnityEngine.UI.Slider>().value = oldSliderValue;
        starSystemObjectScript.h = oldSliderValue;
    }

    public void TipsButtonHoverEnter()
    {
        tipMessage.SetActive(true);
    }
    public void TipsButtonHoverExit()
    {
        tipMessage.SetActive(false);
    }
}
