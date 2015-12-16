using UnityEngine;
using System.Collections;

public class BridgeControl : MonoBehaviour 
{
	public GameObject bridge;
	public GameObject safe1;
	public GameObject safe2;
	public Marker[] markers;
	void Update()
	{
		OpenBridge(CheckMarkers());
	}
	void OpenBridge(bool onOff)
	{
		bridge.SetActive(onOff);
		safe1.SetActive(!onOff);
		safe2.SetActive(!onOff);
	}
	bool CheckMarkers()
	{
		foreach (Marker marker in markers)
		{
			if (!marker.IsSatisfied)
				return false;
		}
		return true;
	}
}
