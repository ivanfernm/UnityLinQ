using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Controls : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    [SerializeField] protected Image stickBall;
    protected Vector2 stickStartPos;
    protected Vector2 currentPositionDelta;
    protected Vector2 normalizedPositionDelta;
    [SerializeField] protected float maxStickDistance; //Distancia máxima
    protected float squaredMaxStickDelta; //Límite
    public Vector2 value { get { return normalizedPositionDelta; } } //Solo lectura
    public Vector2 currentPos { get { return currentPositionDelta; } }
    public Vector2 normPos { get { return normalizedPositionDelta; } }

    private void Start()
    {
        stickStartPos = stickBall.transform.position; //Declara la posición inicial
        squaredMaxStickDelta = maxStickDistance * maxStickDistance; //Distancia máxima al cuadrado
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
      
    }

    public void OnDrag(PointerEventData eventData) //Cantidad de pixeles que se movió cuando estoy haciendo el drag devolviendo un vector
    {
        currentPositionDelta += eventData.delta; //Guarda eventData como variable Vector
        if (currentPositionDelta.sqrMagnitude > squaredMaxStickDelta) { currentPositionDelta = currentPositionDelta.normalized * maxStickDistance; } //Lo normaliza y lo iguala a la distancia máxima
        normalizedPositionDelta = currentPositionDelta / maxStickDistance;
        stickBall.transform.position = stickStartPos + currentPositionDelta; //Suma dos vectores. La posición del stick y el vector de cuanto lo estoy moviendo
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        stickBall.transform.position = stickStartPos;
        currentPositionDelta.x = 0f;                            //Reinicio todo al dejar de dragear
        currentPositionDelta.y = 0f;
        normalizedPositionDelta.x = 0f;
        normalizedPositionDelta.y = 0f;
    }
}


