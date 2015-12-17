using UnityEngine;

public class BGMPlayer : MonoBehaviour {
	static BGMPlayer _instance;
	public AudioClip[] clips;
	int prevScene = -1;
	AudioSource source;

	// Use this for initialization
	void Awake () {
		prevScene = Application.loadedLevel;
		if(_instance!=null)
			Destroy(gameObject);
		else
			_instance = this;
		source = GetComponent<AudioSource>();
		DontDestroyOnLoad(gameObject);
	}
	
	// Update is called once per frame
	void OnLevelWasLoaded(int level) {
		if(prevScene==level)
			return;
		if(level==0){
			source.clip = clips[0];
		}else{
			source.clip = clips[1];
		}
		source.Play();
		prevScene = level;
	}
	
	void Update(){
		transform.position = Camera.main.transform.position;
	}
}
