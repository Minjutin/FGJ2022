using UnityEngine;
using System.Collections;
using UnityEditor;

// HUOMIO !!!

// All the useful code has been commented out

// This is because the game can't be built with this Editor code in it
//(for some frikking reason)

// --> Comment it out and comment out the "fake" stuff to re-activate

// ( Ctrl + K, Ctrl + C			-to comment out large areas
// ( Ctrl + K, Ctrl + U			-to uncomment out large areas


//[CustomEditor(typeof(FieldOfView))]
//public class FieldOfViewEditor : Editor
public class FieldOfViewEditor : MonoBehaviour	// <-- FAKE! REMOVE!
{

	//void OnSceneGUI()
	//{
	//	FieldOfView fow = (FieldOfView)target;
	//	Handles.color = Color.white;
	//	Handles.DrawWireArc(fow.transform.position, Vector3.up, Vector3.forward, 360, fow.viewRadius);
	//	Vector3 viewAngleA = fow.DirFromAngle(-fow.viewAngle / 2, false);
	//	Vector3 viewAngleB = fow.DirFromAngle(fow.viewAngle / 2, false);

	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleA * fow.viewRadius);
	//	Handles.DrawLine(fow.transform.position, fow.transform.position + viewAngleB * fow.viewRadius);

	//	Handles.color = Color.red;
	//	foreach (Transform visibleTarget in fow.visibleTargets)
	//	{
	//		Handles.DrawLine(fow.transform.position, visibleTarget.position);
	//	}
	//}

}