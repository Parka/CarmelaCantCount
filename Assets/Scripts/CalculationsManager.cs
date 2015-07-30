using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class CalculationsManager : MonoBehaviour {

	private Calculator calc;
	private Dictionary<string,Calculator.Operation> operations = new Dictionary<string, Calculator.Operation>{
		{"+",(x,y) => x+y},
		{"-",(x,y) => x-y},
		{"x",(x,y) => x*y},
		{"/",(x,y) => x/y}
	};
	public void keyPressed(string op)
	{
		Debug.Log ("Key Pressed: " + op);
		calc.Operate(operations[op],op);
	}
	// Use this for initialization
	void Start () {
		calc = FindObjectOfType<Calculator> ();
	}
}
