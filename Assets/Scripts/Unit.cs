using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Unit : MonoBehaviour
{
    public GameObject board;
    public BoardManager bm;
    public int boardSize;
    public Vector2Int pos;
    public int maxHealth;
    public int health;

    public UnitAudio uaudio;

    public Projectile bullet;
    public Bomb bomb;
    
    protected HealthNumber healthScript;

    public virtual void OnEnable()
    {
        uaudio = GetComponent<UnitAudio>();
        Canvas healthUI = gameObject.GetComponentInChildren<Canvas>();
        if (healthUI == null)
        {
            Debug.LogError("No canvas found for health UI on " + gameObject);
        }
        else
        {
        healthScript = healthUI.GetComponent<HealthNumber>();
            if (healthScript == null)
            {
                Debug.LogError("No health script found on " + gameObject);
            }
        }
        // Set unit's health to its maximum health. Disable this if it should start at less (or more?)
        health = maxHealth;
        // This needs to be kept regardless so the value initializes.
        ChangeHealthUI(0);
    }

    // Helper method to check if a move can be made
    public virtual bool LegalMove(int[] tileCoord)
    {
        //This is a mouthful but essentially checks the tile board to see if the give position
        if (0 <= tileCoord[0] && bm.currentBoardSize[0] > tileCoord[0] &&
            0 <= tileCoord[1] && bm.currentBoardSize[1] > tileCoord[1])
        {
            return bm.IsTileOpen(tileCoord);
        }
        else
        {
            Debug.Log("Illegal Move attempted to tile #" + tileCoord[0] + ", " + tileCoord[1]);
            return false;
        }

    }

    // Moves the unit to specified tile
    // Does not check to see if the move is legal, so check must be made beforehand
    public virtual void Move(int[] tileCoords)
    {
        Tile targetTile = bm.GetTile(tileCoords[0], tileCoords[1]);
        
        if (targetTile != null)
        {
            Vector3 newPos = targetTile.transform.position + new Vector3(0, 1f, 0);
            gameObject.transform.position = newPos;
            pos = new Vector2Int(tileCoords[0], tileCoords[1]);
        }
    }

    public virtual void LoseHealth(int damage)
    {
        health -= damage;
        ChangeHealthUI(damage);
        uaudio.DamageTakenSFX();
    }

    public void LoseHealth(int damage, bool isCrit)
    {
        health -= damage;
        ChangeHealthUI(damage);
        uaudio.DamageTakenSFX();
    }

    private void ChangeHealthUI(int damage)
    {
        healthScript.UpdateHealth(health, maxHealth, damage);
    }

    private void ChangeHealthUI(int damage, bool isCrit)
    {
        healthScript.UpdateHealth(health, maxHealth, damage, isCrit);
    }

    public virtual void Shoot()
    {

    }
}
