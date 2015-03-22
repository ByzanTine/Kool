using UnityEngine;
using System.Collections;

public class BigFireballSpell : FireballSpell {
	public BigFireballSpell() : base (1, 10, 5){
		fireball = SpellDB.Bigfireball;

	}
}
