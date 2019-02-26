using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineHost : MonoBehaviour {  }

public abstract class State<T>
{
    //IDK
    private static GameObject coroutineObject;
    protected StateMachine<T> currentMachine;

    //Makes sure the class instance exists
    protected T parentObject
    {
        get
        {
            if (currentMachine != null)
            {
                return currentMachine.parentObject;
            }
            else
            {
                Debug.LogWarning("!!! STATE DIDN'T FIND PARENT OBJECT");
                return default(T);
            }
        }
    }

    //IEnumerators are used with coroutines so all logic runs in it's specific order and timers can be set if need be
    public virtual IEnumerator OnEnter() { yield break; }
    public virtual void OnUpdate() { }
    public virtual IEnumerator OnExit() { yield break; }

    //Self-explanatory. Calls the SM and sets the machine
    public void SetMachine(StateMachine<T> machine)
    {
        currentMachine = machine;
    }

    //Self-explanatory. Starts the coroutine.
    public Coroutine StartCoroutine(IEnumerator routine)
    {
        if (coroutineObject == null)
        {
            coroutineObject = new GameObject("State Machine Coroutines");
            GameObject.DontDestroyOnLoad(coroutineObject);
            coroutineObject.AddComponent<CoroutineHost>();
        }

        return coroutineObject.GetComponent<CoroutineHost>().StartCoroutine(routine);
    }
}
