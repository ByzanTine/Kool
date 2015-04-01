﻿using UnityEngine;
using System.Collections;

public class UserData {

	public int userID = -1;
	public int teamID = 2;

	public GameObject wizardInstance = null;
	public Material wizardMaterial;

	public string Username = "Alice";
	public Color Usercolor = Color.green;


	public int deathCount = 0;
	public int rebornTime = -1;
	public Vector3 initPosition;
}
