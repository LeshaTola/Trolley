using Dreamteck.Splines;
using UnityEngine;

[RequireComponent(typeof(SplineFollower))]
public class Trolley : MonoBehaviour
{
	private SplineFollower splineFollower;

	private void Awake()
	{
		splineFollower = GetComponent<SplineFollower>();
	}

	public void SetSpline(SplineComputer spline)
	{
		splineFollower.spline = spline;
	}

	public void StartMove()
	{
		splineFollower.follow = true;
	}
}
