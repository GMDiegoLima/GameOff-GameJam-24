using UnityEngine;
using UnityEngine.Tilemaps;

public class SurfaceType : MonoBehaviour
{
    public string surfaceName;
    public Tilemap Ground;
    public Transform Player;

    // void Update()
    // {
    //     Vector3Int gridPosition = Ground.WorldToCell(Player.position);
    //     TileBase currentTile = Ground.GetTile(gridPosition);
    //     if (currentTile.name.ToLower().Contains("cobblestone"))
    //     {
    //         AkSoundEngine.SetSwitch("GroundTextures", "dirt", gameObject);
    //         return;
    //     }
    //     switch (currentTile.name)
    //     {
    //         case "abyss_0":
    //             AkSoundEngine.SetSwitch("GroundTextures", "water", gameObject);
    //             break;
    //         case "GrassRuleTIle":
    //             AkSoundEngine.SetSwitch("GroundTextures", "grass", gameObject);
    //             break;
    //         default:
    //             AkSoundEngine.SetSwitch("GroundTextures", "dirt", gameObject);
    //             break;
    //     }
    // }
    void OnTriggerEnter2D(Collider2D other)
    {
        AkSoundEngine.SetSwitch("GroundTextures", "grass", gameObject);
    }
    void OnTriggerExit2D(Collider2D other)
    {
        AkSoundEngine.SetSwitch("GroundTextures", "stone", gameObject);
    }
}
