using UnityEngine;
using System.Collections;

public class PlayerSignals {

	public static Signal fireSignal = new Signal();
	public static Signal passCollider = new Signal();
	public static Signal destroyBase = new Signal();
	public static Signal enableSignals = new Signal();
	public static Signal disableSignals = new Signal();
	public static Signal showPauseMenu = new Signal();
	public static Signal onPause = new Signal();
	public static Signal onGameOver = new Signal();
}
