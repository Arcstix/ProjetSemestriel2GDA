using UnityEngine;

public class ShakeReceiver : MonoBehaviour 
{
    [SerializeField] private Camera _camera;
    [Tooltip("Exponent for calculating the shake factor. Useful for creating different effect fade outs")]
    [SerializeField] private float _traumaExponent = 1;
    [Tooltip("Maximum angle that the gameobject can shake. In euler angles.")]
    [SerializeField] private Vector3 _maximumAngularShake = Vector3.one * 5;
    [Tooltip("Maximum translation that the gameobject can receive when applying the shake effect.")]
    [SerializeField] private Vector3 _maximumTranslationShake = Vector3.one * .75f;


    private float _trauma;
    private Vector3 _lastPosition;
    private Vector3 _lastRotation;

    private void Update()
    {
        float shake = Mathf.Pow(_trauma, _traumaExponent);
        /* Only apply this when there is active trauma */
        if(shake > 0)
        {
            var previousRotation = _lastRotation;
            var previousPosition = _lastPosition;
            /* In order to avoid affecting the transform current position and rotation each frame we substract the previous translation and rotation */
            _lastPosition = new Vector3(
                _maximumTranslationShake.x * (Mathf.PerlinNoise(0, Time.time * 25) * 2 - 1),
                _maximumTranslationShake.y * (Mathf.PerlinNoise(1, Time.time * 25) * 2 - 1),
                _maximumTranslationShake.z * (Mathf.PerlinNoise(2, Time.time * 25) * 2 - 1)
            ) * shake;

            _lastRotation = new Vector3(
                _maximumAngularShake.x * (Mathf.PerlinNoise(3, Time.time * 25) * 2 - 1),
                _maximumAngularShake.y * (Mathf.PerlinNoise(4, Time.time * 25) * 2 - 1),
                _maximumAngularShake.z * (Mathf.PerlinNoise(5, Time.time * 25) * 2 - 1)
            ) * shake;

            _camera.transform.position += _lastPosition - previousPosition;
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles + _lastRotation - previousRotation);
            _trauma = Mathf.Clamp01(_trauma - Time.deltaTime);
        }
        else
        {
            if (_lastPosition == Vector3.zero && _lastRotation == Vector3.zero) return;
            /* Clear the transform of any left over translation and rotations */
            _camera.transform.position -= _lastPosition;
            _camera.transform.rotation = Quaternion.Euler(_camera.transform.rotation.eulerAngles - _lastRotation);
            _lastPosition = Vector3.zero;
            _lastRotation = Vector3.zero;
        }
    }

    /// <summary>
    ///  Applies a stress value to the current object.
    /// </summary>
    /// <param name="Stress">[0,1] Amount of stress to apply to the object</param>
    public void InduceStress(float Stress)
    {
        _trauma = Mathf.Clamp01(_trauma + Stress);
    }
}