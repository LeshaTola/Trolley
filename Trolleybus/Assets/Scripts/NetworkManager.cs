using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Networking;

public enum HttpMethods
{
	GET,
	POST,
	PUT,
	DELETE,
}

public class NetworkManager : MonoBehaviour
{
	private const string URI = "http://10.21.80.157:5000";

	public static NetworkManager Instance { get; private set; }

	private void Awake()
	{
		Instance = this;
	}

	public async Task AddChoiceAsync(Choice choice)
	{
		string URL = $"{URI}/Choices";
		var json = JsonUtility.ToJson(choice);

		using (var webRequest = GetUnityWebRequest(URL, HttpMethods.POST, json))
		{
			try
			{
				await SendRequestAsync(webRequest);
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

	public async Task<List<Choice>> GetChoicesAsync(int level)
	{
		string URL = $"{URI}/Choices/{level}";
		using (var webRequest = GetUnityWebRequest(URL, HttpMethods.GET))
		{
			await SendRequestAsync(webRequest);

			var json = webRequest.downloadHandler.text;
			var choices = JsonConvert.DeserializeObject<List<Choice>>(json);
			return choices;
		}
	}

	public async Task<List<Choice>> GetChoicesAsync()
	{
		string URL = $"{URI}/Choices";
		using (var webRequest = GetUnityWebRequest(URL, HttpMethods.GET))
		{
			try
			{
				await SendRequestAsync(webRequest);

				var json = webRequest.downloadHandler.text;
				var choices = JsonConvert.DeserializeObject<List<Choice>>(json);
				return choices;
			}
			catch (Exception ex)
			{
				throw ex;
			}
		}
	}

	public async Task SendRequestAsync(UnityWebRequest webRequest)
	{
		webRequest.SendWebRequest();

		while (!webRequest.isDone)
		{
			await Task.Yield();
		}

		if (webRequest.result == UnityWebRequest.Result.Success)
		{
			Debug.Log("Response: " + webRequest.downloadHandler.text);
		}
		else
		{
			if (webRequest.result == UnityWebRequest.Result.ConnectionError)
			{
				throw new Exception("Network Error: " + webRequest.error);
			}
			else if (webRequest.result == UnityWebRequest.Result.ProtocolError)
			{
				throw new Exception("HTTP Error: " + webRequest.error);
			}
			else
			{
				throw new Exception("HTTP Error: " + webRequest.error);
			}
		}
	}

	public UnityWebRequest GetUnityWebRequest(string URL, HttpMethods method = HttpMethods.GET, string data = null)
	{
		UnityWebRequest webRequest = new UnityWebRequest(URL, method.ToString());

		if (data != null)
		{
			var body = Encoding.UTF8.GetBytes(data);
			webRequest.uploadHandler = new UploadHandlerRaw(body);
		}

		webRequest.downloadHandler = new DownloadHandlerBuffer();
		webRequest.SetRequestHeader("Content-Type", "application/json");

		webRequest.timeout = 5;

		return webRequest;
	}
}

public class Choice
{
	public int Id;
	public int Level;
	public bool Pulled;
}
