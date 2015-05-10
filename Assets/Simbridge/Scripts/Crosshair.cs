using UnityEngine;
using System.Collections;

/// <summary>
///  Crosshair allows you to point at objects that are interactable and customize the crosshair with sounds.
/// </summary>
public class Crosshair : MonoBehaviour
{
    
    #region OBJECT REFERENCES

    public GameObject crosshairs;
    public Texture2D standardCrosshairs;
    public Texture2D copyCodeCrosshair;
    public Texture2D accessibleObjectCrosshair;
    public Texture2D interactCrosshair;

    #endregion

    private float timeLastFired = 0.0f;
    private float delayTime = 0.5f;

    public AudioClip shootSound;
    public AudioClip hoverSound;
    public AudioClip[] execSounds;
    public AudioClip copySound;
    public AudioClip errorSound;
    public AudioClip selectSound;
    public AudioClip copiedSound;

    private RaycastHit rayHit;
    private bool raycastReturn;

    /// <summary>
    /// Plays a randomized sound clip.
    /// </summary>
    /// <param name="soundName"></param>
    /// <returns></returns>
    private AudioClip RandomizeSound(string soundName)
    {
        if (execSounds.Length > 0)
        {
            return execSounds[Random.Range(0, execSounds.Length)];
        }
        else
        {
            return shootSound;
        }
    }

    private void HoverSound()
    {
        GetComponent<AudioSource>().PlayOneShot(hoverSound);
    }

    private void ClickSound()
    {
        GetComponent<AudioSource>().PlayOneShot(shootSound);
    }

    private void ErrorSound()
    {
        GetComponent<AudioSource>().PlayOneShot(errorSound);
    }

    private void OnGUI()
    {
        if (triggerMessage != "")
        {
            GUILayout.BeginArea(new Rect(Screen.width / 2 - 150, Screen.height / 2.5f, 300, 100));
            {
                GUILayout.Label("<color=yellow><size=32>" + triggerMessage + "</size></color>");
            }
            GUILayout.EndArea();
        }
    }

    public GameObject targetObject;
    public GameObject lastTargetObject;
    public string triggerMessage = "";

    private void Update()
    {
        
        // reset cross hairs to normal unless we detect something else
        SwapCrosshairs(standardCrosshairs);

        raycastReturn = Physics.Raycast(transform.position, transform.forward, out rayHit, Mathf.Infinity);
        // check non-layer filtered mouseover here - copyableCode etc, change crosshair color
        if ((raycastReturn))
        {
            ActivateTrigger aTrigger = rayHit.transform.gameObject.GetComponent<ActivateTrigger>();
            if ( aTrigger != null )
            {
                SwapCrosshairs(interactCrosshair);
                targetObject = rayHit.transform.gameObject;
                triggerMessage = aTrigger.activateOnShootMessage;
                if (lastTargetObject == null || targetObject != lastTargetObject)
                {
                    HoverSound();
                }
                lastTargetObject = targetObject;
            }
            else
            {
                targetObject = null;
                triggerMessage = "";
            }
        }

        // on mouse press
        if (Input.GetButtonDown("Fire1"))
        {
            // check cool down
            if (Time.time > timeLastFired + delayTime)
            {
                    
                if (targetObject != null)
                {
                    targetObject.SendMessage("DoActivateTrigger");
                    ClickSound();
                    timeLastFired = Time.time;
                    targetObject = null;
                }
                else
                {
                    ErrorSound();
                }
            }
        }
    }

    /// <summary>
    /// Crosshair switches to indicate what is in front of you.
    /// </summary>
    /// <param name="crosshairTexture"></param>
    void SwapCrosshairs(Texture2D crosshairTexture)
    {
        crosshairs.GetComponent<Renderer>().material.mainTexture = crosshairTexture;
    }
}