using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CameraScaler : MonoBehaviour 
{
	public Transform[] players;
	
	
	Vector4 GetMaxDifferece()
	{
		float maxX = players[0].position.x;
		float minX = players[0].position.x;
		float maxY = players[0].position.y;
		float minY = players[0].position.y;
		
		for (int i=1; i<players.Length; i++)
		{
			if (players[i].position.x > maxX)
				maxX = players[i].position.x;
			if (players[i].position.x < minX)
				minX = players[i].position.x;
			if (players[i].position.y > maxY)
				maxY = players[i].position.y;
			if (players[i].position.y < minY)
				minY = players[i].position.y;
		}
		return new Vector4(maxX-minX, maxY-minY, maxX+minX, maxY+minY);
	}
	float rescaleSpeed = 2;
	float followSpeed = 2;
	void RescaleCam()
	{
		Vector4 posInfo = 0.5f * GetMaxDifferece();
		float camSize = Mathf.Max(Mathf.Max(posInfo.x * 9/16, posInfo.y) + 4, 5);
		Vector3 camPos = new Vector3(posInfo.z, posInfo.w, Camera.main.transform.position.z);
		
		float sizeDelta = camSize - Camera.main.orthographicSize;
		Vector3 posDelta = camPos - Camera.main.transform.position;
		
		Camera.main.orthographicSize += sizeDelta * rescaleSpeed * Time.deltaTime;
		Camera.main.transform.position += followSpeed * posDelta * Time.deltaTime;
	}
	
	void FixedUpdate()
	{
		RescaleCam();
	}
}
