using UnityEngine;
using System.Collections;
// This script will take charge of dynamic coefficient
// Say Main Menu variables: Volume, Game Speed, Round Time etc
public class Globals : MonoBehaviour {
	// DEBUG
	// =============================================================
	[Range(1.0f, 100.0f)]
	public const string FORCE_MULTIPLIER_LABEL = "FORCE_MULTIPLIER";
	public static float FORCE_MULTIPLIER = 100.0f;

	public static void setValue(string label, float value) {
		if (label == FORCE_MULTIPLIER_LABEL) {
			FORCE_MULTIPLIER = value;
		}
	}
}
