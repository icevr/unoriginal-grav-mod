using System;
using System.Collections.Generic;
using HarmonyLib;
using BepInEx;
using UnityEngine;
using System.Reflection;
using UnityEngine.XR;
using Photon.Pun;
using System.IO;
using System.Net;
using Photon.Realtime;
using UnityEngine.Rendering;

namespace random
{
    [BepInPlugin("org.icevr.monkeytag.random no gravity", "random no gravity", "VERSION 1")]
    public class MyPatcher : BaseUnityPlugin
    {
        public void Awake()
        {
            var harmony = new Harmony("com.icevr.monkeytag.HOW TO USE");
            harmony.PatchAll(Assembly.GetExecutingAssembly());
        }
    }

    [HarmonyPatch(typeof(GorillaLocomotion.Player))]
    [HarmonyPatch("Update", MethodType.Normal)]
    public class Class1
    {
        static bool body = false;  
        static void Postfix(GorillaLocomotion.Player __instance)
        {
            if (!PhotonNetwork.CurrentRoom.IsVisible || !PhotonNetwork.InRoom)
            {
                List<InputDevice> list = new List<InputDevice>();
                InputDevices.GetDevicesWithCharacteristics(UnityEngine.XR.InputDeviceCharacteristics.HeldInHand | UnityEngine.XR.InputDeviceCharacteristics.Left | UnityEngine.XR.InputDeviceCharacteristics.Controller, list);
                list[0].TryGetFeatureValue(CommonUsages.triggerButton, out body);

                if (body)
                {
                    __instance.bodyCollider.attachedRigidbody.detectCollisions = false;
                    __instance.bodyCollider.attachedRigidbody.useGravity = false;
                }
                else
                {
                    __instance.bodyCollider.attachedRigidbody.detectCollisions = true;
                    __instance.bodyCollider.attachedRigidbody.useGravity = true;
                }

            }
        }
    }
}