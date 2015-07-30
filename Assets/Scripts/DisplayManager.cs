using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class DisplayManager : MonoBehaviour {

	public GameObject Display;
	public GameObject Digit;
	public Color SymbolsColor;
	public Color[] colors;

	private Calculator calc;
	// Use this for initialization
	void Start () {
		calc = FindObjectOfType<Calculator> ();
		calc.OnNumberEvent += displayNumber;
		colorButtons ();
	}

	void colorButtons ()
	{
		Button[] buttons = FindObjectsOfType<Button> ();
		foreach (Button x in buttons) {
			string text = x.gameObject.GetComponentInChildren<Text>().text;
			int num = -1;
			Color color;
			try {
				num = int.Parse(text);
				color = colors[num];
			} catch (System.Exception ex) {
				color = SymbolsColor;
			};

			if(x.GetComponentInChildren<Text>() != null)
				x.GetComponentInChildren<Text>().color = color;
			if(x.GetComponentInChildren<Outline>() != null)
				x.GetComponentInChildren<Outline>().effectColor = color;
		}
	}

	void displayNumber(string number){
		Debug.Log (number);
		char[] digits = number.ToString().ToCharArray();
		foreach (Transform x in Display.GetComponentsInChildren<Transform>()) {
			if(x != Display.transform)
				Destroy(x.gameObject);
		}
		GameObject instance;

		foreach (char x in digits) {
			instance = Instantiate<GameObject> (Digit);
			instance.GetComponentInChildren<Text> ().text = x.ToString();
			Color color;
			try {
				color =colors[int.Parse(x.ToString())];
			} catch (System.Exception ex) {
				color = SymbolsColor;
			}
			instance.GetComponentInChildren<Text> ().color = color;
			instance.GetComponentInChildren<Outline> ().effectColor = color;
			instance.transform.SetParent (Display.transform);
		}
	}
	// Update is called once per frame
	void Update () {
	
	}
}
