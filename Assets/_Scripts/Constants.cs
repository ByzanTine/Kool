using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	// DEBUG
	// =============================================================
	// the minimun cool donw time for casting. protect the animation
	public const float minCastCoolDown = 0.4f;

	public const float DEFAULT_ATTACK_RADIUS = 10.0f;

	public const float COLLIDER_DELAY_SECONDS = 0.3f;

	public const float FIREBALL_DAMAGE = 0.3f;

	static public float[] SPELL_DAMAGE = {0.3f, 0.2f};
}
