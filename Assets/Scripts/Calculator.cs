using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Text;

public class Calculator : MonoBehaviour {

	private enum State {FirstEntry, SecondEntry, ResultShowcase};
	private State state = State.FirstEntry;

	private Dictionary<State,StringBuilder> Slots = new Dictionary<State, StringBuilder>(){
		{State.FirstEntry, new StringBuilder()},
		{State.SecondEntry,new StringBuilder()}
	};
	
	private StringBuilder currentSlot
	{
		get{
			return Slots[state];
		}
	}
	private bool ready
	{
		get{
			return Slots[State.FirstEntry].Length != 0;
		}
	}
	private double currentNumber
	{
		get{
			return double.Parse(Slots[state].ToString());
		}
	}
	private double firstNumber{
		get{
			return double.Parse(Slots[State.FirstEntry].ToString());
		}
	}
	
	private double secondNumber{
		get{
			return double.Parse(Slots[State.SecondEntry].ToString());
		}
	}

	public delegate double Operation(double first, double second);
	private Operation opertation;

	public delegate void NumberEventHandler(string number);
	public event NumberEventHandler OnNumberEvent;


	public void Push(int val)
	{
		if (state == State.ResultShowcase)
			Flush ();
		currentSlot.Append(val);
		if (OnNumberEvent != null)
			OnNumberEvent (currentNumber.ToString());
	}
	public void Operate(Operation o, string symbol)
	{
		if (!ready)
			return;
		if (state == State.SecondEntry)
			Equals();
		OnNumberEvent (symbol + " " + firstNumber);
		state = State.SecondEntry;
		opertation = o;
	}

	public void Equals()
	{
		if (state != State.SecondEntry)
			return;
		if (opertation == null)
			return;
		try {
			double result = opertation (firstNumber, secondNumber);
			
			if (OnNumberEvent != null)
				OnNumberEvent (result.ToString());
			
			Flush ();

			state = State.ResultShowcase;

			Slots [State.FirstEntry].Append(result);
		} catch (Exception ex) {
			Debug.Log("Something funny happened.");
		}

	}

	public void AddComma()
	{
		if (state == State.ResultShowcase)
			Flush ();
		if (currentSlot.Length == 0)
			currentSlot.Append ("0");
		if(!currentSlot.ToString().Contains("."))
			currentSlot.Append (".");
		OnNumberEvent (currentSlot.ToString ());
	}
	public void Clear()
	{
		Flush ();
		if (OnNumberEvent != null)
			OnNumberEvent ("0");
	}
	private void Flush()
	{
		Slots [State.FirstEntry] = new StringBuilder ();
		Slots [State.SecondEntry] = new StringBuilder ();
		opertation = null;
		state = State.FirstEntry;
	}
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
