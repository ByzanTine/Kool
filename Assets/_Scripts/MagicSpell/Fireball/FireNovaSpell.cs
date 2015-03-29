using UnityEngine;
using System.Collections;

public class FireNovaSpell : FireballSpell {

	public FireNovaSpell() : base (1, 1, 1.0f){
		fireball = SpellDB.fireNova;
		
	}
}
