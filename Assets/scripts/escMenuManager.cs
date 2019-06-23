using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class escMenuManager : MonoBehaviour
{
    public GameObject escMenu;
    cameraMovement cameraMovementScript;
    starSystemObject starSystemObjectScript;
    public GameObject dropdownMenuOptionLabelObject;
    initialConditions initialConditionsScript;

    private Dictionary<string, int> map = new Dictionary<string, int>();
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

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
