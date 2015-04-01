using UnityEngine;
using System.Collections;

public class BigIceBallSpell : IceBallSpell {
	public BigIceBallSpell() : base (2, 5, 1){
		iceball = SpellDB.Bigiceball;
	}
}
