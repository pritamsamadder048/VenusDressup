using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MiniJsonCompare : IComparer {
	
	public int Compare(object a, object b){

		if (int.Parse (( a as Hashtable) ["Rapo"].ToString ()) > int.Parse ((b as Hashtable) ["Rapo"].ToString ()))
			return -1;
		else
			return 1;

		return 0;
	}
}
