using UnityEngine;
using System.Collections;
using UnityEngine.UI;

[RequireComponent (typeof (Image))]
public class ExpressionSwitcher : MonoBehaviour {

	[System.Serializable]
	public struct SpriteInfo {
		public string name;
		public Sprite sprite;
	}

	public SpriteInfo[] sprites;

	[Yarn.Unity.YarnCommand("setsprite")]
	public void UseSprite(string spriteName) {

		//"off" command hides the Sprite
		if(spriteName == "off")
		{
			transform.gameObject.SetActive(false);
			return;
		}

		//Show the Sprite if it wasn't visible before.
		if(!transform.gameObject.activeSelf) transform.gameObject.SetActive(true);

		Sprite s = null;
		foreach(var info in sprites) {
			if (info.name == spriteName) {
				s = info.sprite;
				break;
			}
		}
		if (s == null) {
			Debug.LogErrorFormat("Can't find sprite named {0}!", spriteName);
			return;
		}

		GetComponent<Image>().sprite = s;
	}
}
