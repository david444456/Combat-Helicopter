
using UnityEngine;

public static class DeviceRotation 
{
    static bool gyroInitialized = false;

    public static bool HasGyroscope {
        get {

            return SystemInfo.supportsGyroscope;
        }
    }
    public static Quaternion Get (){
        
        if (!gyroInitialized){
            InitGyro();
        }
        return HasGyroscope
            ? ReadGyroscopeRotation()
            : Quaternion.identity;
    }
    static void InitGyro() {
        Debug.Log(HasGyroscope);
        if (HasGyroscope) {
            Input.gyro.enabled = true;
            Input.gyro.updateInterval = 0.0167f;
            Debug.Log("Estoy aqui");
        }
         gyroInitialized = true;
    }
    static Quaternion ReadGyroscopeRotation()
    {
        Debug.Log(Input.gyro.attitude);
        return new Quaternion(0.5f, 0.5f, 0.5f, 0.5f) * Input.gyro.attitude * new Quaternion(0, 0, 1, 0);
    }
}
