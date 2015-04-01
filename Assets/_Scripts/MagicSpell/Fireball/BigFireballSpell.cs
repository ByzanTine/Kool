using UnityEngine;
using System.Collections;

public class BigFireballSpell : FireballSpell {
	public BigFireballSpell() : base (2, 5, 2.2f){
		fireball = SpellDB.Bigfireball;

	}
}
