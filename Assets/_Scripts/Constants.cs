using UnityEngine;
using System.Collections;

public class Constants : MonoBehaviour {
	// DEBUG
	// =============================================================
	// the minimun cool donw time for casting. protect the animation
	public const float MIN_CAST_COOL_DOWN = 0.3f;

	public const float PLAYER_ANGULAR_SPEED = 0.15f;

	public const float FIREBALL_MANA_COST = 0.1f;

	public const float DEFAULT_ATTACK_RADIUS = 30.0f;

	public const float COLLIDER_DELAY_SECONDS = 0.15f;

	public const float FIREBALL_DAMAGE = 0.3f;

	static public float[] SPELL_DAMAGE = {0.1f, 0.2f};

	public enum spell_mode{
		Ice,
		Fire,
		Thunder,
		Lighten,
		Dark
	}
}
