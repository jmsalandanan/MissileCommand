using UnityEngine;
using System;
using System.Collections;

#pragma warning disable 0693

/// <summary>
/// This is the events class used to dispatch signals using
/// a broadcast and subscription model.
/// 
/// Note: Currently you can have as much as 3 types of parameters and types. 
/// This is to have ample amount of parameter passing while
/// not over passing. If you need to pass many parameters using signals,
/// consider creating an <code>EventArgs</code> or create your own object class.
/// </summary>
public class Signal<T, U, V>  {
	
	/// <summary>
	/// The signal generic type delegate
	/// </summary>
    public delegate void SignalHandler<T, U, V>(T param1, U param2, V param3);
	
	/// <summary>
	/// The signal generic type event
	/// </summary>
    private event SignalHandler<T, U, V> EventSignal;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Signals"/> class.
	/// </summary>
	public Signal(){}
	
	/// <summary>
	/// Add a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the signal is dispatched.
	/// </param>
    public void add(SignalHandler<T, U, V> function){
        EventSignal += function;
    }
	
	/// <summary>
	/// Remove a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a signal is dispatched. 
	/// </param>

    public void remove(SignalHandler<T, U, V> function){   
        EventSignal -= function;
    }   
	
	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param1, U param2, V param3){
		try{
	        if (EventSignal != null){
	            EventSignal(param1, param2, param3);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (Exception e){
			Debug.Log("Dispatch Error: " + e.Message);
		}
    }
}

/// <summary>
/// This is the events class used to dispatch signals using
/// a broadcast and subscription model.
/// </summary>
public class Signal<T, U>  {
	
	/// <summary>
	/// The signal generic type delegate
	/// </summary>
    public delegate void SignalHandler<T, U>(T param1, U param2);
	
	/// <summary>
	/// The signal generic type event
	/// </summary>
    private event SignalHandler<T, U> EventSignal;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Signals"/> class.
	/// </summary>
	public Signal(){}
	
	/// <summary>
	/// Add a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the signal is dispatched.
	/// </param>
    public void add(SignalHandler<T, U> function){
        EventSignal += function;
    }
	
	/// <summary>
	/// Remove a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a signal is dispatched. 
	/// </param>

    public void remove(SignalHandler<T, U> function){   
        EventSignal -= function;
    }   
	
	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param1, U param2){
		try{
	        if (EventSignal != null){
	            EventSignal(param1, param2);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (ArgumentException e){
			Debug.Log("Dispatch Error: " + e.Message);
		}
    }
}

/// <summary>
/// This is the events class used to dispatch signals using
/// a broadcast and subscription model.
/// </summary>
public class Signal<T>  {
	
	/// <summary>
	/// The signal generic type delegate
	/// </summary>
    public delegate void SignalHandler<T>(T param);
	
	/// <summary>
	/// The signal generic type event
	/// </summary>
    private event SignalHandler<T> EventSignal;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Signals"/> class.
	/// </summary>
	public Signal(){}
	
	/// <summary>
	/// Add a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the signal is dispatched.
	/// </param>
    public void add(SignalHandler<T> function){
        EventSignal += function;
    }
	
	/// <summary>
	/// Remove a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a signal is dispatched. 
	/// </param>

    public void remove(SignalHandler<T> function){   
        EventSignal -= function;
    }   
	
	/// <summary>
	/// Dispatch an event with the corresponding events argument.
	/// </summary>
	/// <param name='ea'>
	/// The attachment to be sent.
	/// </param>
    public void dispatch(T param){
		try{
	        if (EventSignal != null){
	            EventSignal(param);
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (ArgumentException e){
			Debug.Log("Dispatch Error: " + e.Message);
		}
    }
}

public class Signal  {
	
	/// <summary>
	/// The signal generic type delegate
	/// </summary>
    public delegate void SignalHandler();
	
	/// <summary>
	/// The signal generic type event
	/// </summary>
    private event SignalHandler EventSignal;
	
	/// <summary>
	/// Initializes a new instance of the <see cref="Signals"/> class.
	/// </summary>
	public Signal(){}
	
	/// <summary>
	/// Add a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function to be executed when the signal is dispatched.
	/// </param>
    public void add(SignalHandler function){
        EventSignal += function;
    }
	
	/// <summary>
	/// Remove a signal event.
	/// </summary>
	/// <param name='function'>
	/// The function of the event used to be executed when a signal is dispatched. 
	/// </param>

    public void remove(SignalHandler function){   
        EventSignal -= function;
    }   
	
	/// <summary>
	/// Dispatch an event signal.
	/// </summary>
    public void dispatch(){
		try {
	        if (EventSignal != null){
	            EventSignal();
	        } else {
				//Debug.Log("No event listeners found");
			}
		} catch (ArgumentException e){
			Debug.Log("Dispatch error: " + e.Message);
		}
    }
}
