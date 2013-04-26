using UnityEngine;
using System.Collections;

public class EnemySignals {
	public static Signal explodeSignal = new Signal();
	public static Signal enemyRelease = new Signal();
	public static Signal checkEnemies = new Signal();
	public static Signal destroyEnemy = new Signal();
	public static Signal enableSignals = new Signal();
	public static Signal disableSignals = new Signal();
}
