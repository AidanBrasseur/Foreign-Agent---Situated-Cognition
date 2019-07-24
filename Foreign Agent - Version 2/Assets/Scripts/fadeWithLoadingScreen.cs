using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class fadeWithLoadingScreen : MonoBehaviour
{
	public GameObject loadingScreen;
	private LoadingScreen loadScript;
	void Start()
    {
		loadScript = loadingScreen.GetComponent<LoadingScreen>();
    }

    // Update is called once per frame
    void Update()
    {
		if (loadScript.didTriggerFadeOutAnimation && loadScript.alpha <= GetComponent<Graphic>().color.a)
		{
			Color newColour = GetComponent<Graphic>().color;
			newColour.a = loadScript.alpha;
			GetComponent<Graphic>().color = newColour;
		}
	}
}
