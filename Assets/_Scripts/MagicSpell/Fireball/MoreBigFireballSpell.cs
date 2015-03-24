using UnityEngine;
using System.Collections;

public class MoreBigFireballSpell : FireballSpell {
	public MoreBigFireballSpell() : base (2, 10, 2.2f){
		TimeInterval = 0.3f;
		fireball = SpellDB.Bigfireball;
	}

}
