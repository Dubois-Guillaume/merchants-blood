using UnityEngine.Tilemaps;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    public Tilemap tilemap;
    public GameObject player;
    public float selectionRadius = 1f; // Limite de distance autour du joueur en tiles

    void Update()
    {
        SnapCursorToGrid();
    }

    private void SnapCursorToGrid()
    {
        // Obtenir la position du curseur par rapport à la caméra
        Vector2 mousePos = Input.mousePosition;
        Vector2 worldPosition = Camera.main.ScreenToWorldPoint(mousePos);

        // Convertir la position du monde en position de grille
        Vector3Int gridPosition = tilemap.WorldToCell(worldPosition);

        // Obtenir la position du joueur dans la grille
        Vector3Int playerGridPosition = tilemap.WorldToCell(player.transform.position);

        // Calculer la distance entre le joueur et la position du curseur
        float distance = Vector3Int.Distance(gridPosition, playerGridPosition);

        // Si la distance dépasse le rayon défini, limiter le curseur
        if (distance <= selectionRadius)
        {
            // Le curseur reste dans la zone autour du joueur
            Vector3 snappedPosition = tilemap.GetCellCenterWorld(gridPosition);
            this.transform.position = snappedPosition;
        }
        else
        {
            // Le curseur reste sur la case adjacente la plus proche du joueur
            Vector3Int clampedPosition = ClampGridPositionToRadius(gridPosition, playerGridPosition);
            this.transform.position = tilemap.GetCellCenterWorld(clampedPosition);
        }
    }

    // Limiter la position du curseur autour du joueur dans un rayon donné
    private Vector3Int ClampGridPositionToRadius(Vector3Int cursorPosition, Vector3Int playerPosition)
    {
        int clampedX = Mathf.Clamp(cursorPosition.x, playerPosition.x - 1, playerPosition.x + 1);
        int clampedY = Mathf.Clamp(cursorPosition.y, playerPosition.y - 1, playerPosition.y + 1);

        return new Vector3Int(clampedX, clampedY, cursorPosition.z);
    }
}
