using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine<T>
{
    //Keeping track of the state and the object holding the machine
    public State<T> currentState;
    //A parent object simply makes this script more versatile; it essentially allows the state machines to be controlled off an object that may dictate the flow of the game
    public T parentObject;

    //Sets parent object that references all the states
    public StateMachine(T parent)
    {
        parentObject = parent;
    }

    bool switching = false;

    //IEnum that causes the states to change while waiting for coroutine
    public IEnumerator SwitchState(State<T> s)
    {
        switching = true;

        if (currentState != null)
        {
            yield return currentState.StartCoroutine(currentState.OnExit());
            currentState.SetMachine(null);
        }

        currentState = s;
        Debug.Log("state machine set");
        if (currentState != null)
        {
            currentState.SetMachine(this);
            yield return currentState.StartCoroutine(currentState.OnEnter());
        }

        switching = false;
    }

    //When in a state, continue to update that state
    public void Update()
    {
        if (!switching && currentState != null)
        {
            currentState.OnUpdate();
        }
    }
}
