using Cinemachine;
using UnityEngine;
public class CameraShaker : MonoBehaviour
{
	private CinemachineVirtualCamera cinemachineVirtualCamera;
	private float shaketimer;

	private float totalTimer;
	private float totalIntencity;

	private void Awake()
	{
		cinemachineVirtualCamera = GetComponent<CinemachineVirtualCamera>();
	}

	private void Update()
	{
		if (shaketimer > 0)
		{
			shaketimer -= Time.deltaTime;
			CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

			cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = Mathf.Lerp(totalIntencity, 0f, 1 - (shaketimer / totalTimer));
		}
	}

	public void ShakeCamera(float intensity, float timer)
	{
		CinemachineBasicMultiChannelPerlin cinemachineBasicMultiChannelPerlin = cinemachineVirtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();

		cinemachineBasicMultiChannelPerlin.m_AmplitudeGain = intensity;

		totalIntencity = intensity;
		totalTimer = timer;
		shaketimer = timer;
	}
}
