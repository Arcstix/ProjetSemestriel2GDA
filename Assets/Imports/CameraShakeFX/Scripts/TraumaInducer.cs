 using UnityEngine;
using System.Collections;

/* Example script to apply trauma to the camera or any game object */
public class TraumaInducer : MonoBehaviour 
{
    [Tooltip("Maximum stress the effect can inflict upon objects Range([0,1])")]
    [SerializeField] private float _maximumStress = 0.6f;
    [Tooltip("Maximum distance in which objects are affected by this TraumaInducer")]
    [SerializeField] private float _range = 45;

    private void StartTrauma(StressReceiver stressReceiver)
    {
        float distance = Vector3.Distance(transform.position, stressReceiver.transform.position);
        float distance01 = Mathf.Clamp01(distance / _range);
        float stress = (1 - Mathf.Pow(distance01, 2)) * _maximumStress;
        stressReceiver.InduceStress(stress);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Entity"))
        {
            if(other.TryGetComponent<StressReceiver>(out StressReceiver stressReceiver))
            {
                StartTrauma(stressReceiver);
            }
        }
    }
}