using UnityEngine;
using System.Collections;

public class MoreBigIceBallSpell : IceBallSpell {
	public MoreBigIceBallSpell() : base (4, 5, 1){
		TimeInterval = 0.3f;
		iceball = SpellDB.Bigiceball;
	}
}
