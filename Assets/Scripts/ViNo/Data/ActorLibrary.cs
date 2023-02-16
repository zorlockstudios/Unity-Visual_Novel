//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
// 			VisualNovelToolkit		     /_/_/_/_/_/_/_/_/_/.
// Copyright ©2013 - Sol-tribe.	/_/_/_/_/_/_/_/_/_/.
//_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/_/.
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

namespace ViNoToolkit{
	
	/// <summary>
	/// Actor library.
	/// </summary>
	public class ActorLibrary : MonoBehaviour {
		/// <summary>
		/// The actor entries.
		/// </summary>
		public ActorInfo[] actorEntries;

		public string[] GetActorNameList(){
			List<string> actorList = new List<string>();
			for(int i=0;i<actorEntries.Length;i++){
				actorList.Add( actorEntries[ i ].actorName );
			}
			return actorList.ToArray();
		}

	}

}