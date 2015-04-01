using UnityEngine;
using System.Collections;

public class MoreBigFireballSpell : FireballSpell {
	public MoreBigFireballSpell() : base (4, 5, 2.2f){
		TimeInterval = 0.3f;
		fireball = SpellDB.Bigfireball;
	}

}
