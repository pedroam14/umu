using UnityEditor;
using UnityEngine;
public class YourClassAsset {
	[MenuItem ("Assets/Create/Conversatio Data")]
	public static void CreateConversationData () {
		ScriptableObjectUtility.CreateAsset<ConversationData> ();
	}
}