﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GOAP;
using FP;
using System;

public class Node : MonoBehaviour, IGraphNode<Node>
{
    public List<Node> adyacent;
    public Node Content { get { return this; } }

    public void Awake()
    {
        RaycastHit[] hits = Physics.SphereCastAll(transform.position, 1, transform.up, LayerMask.NameToLayer(StringTagManager.tagNode));

        for (int i = 0; i < hits.Length; i++)
        {
            Node node = hits[i].collider.gameObject.GetComponent<Node>();
            if (node != null)
            {
                adyacent.Add(node);
            }                
        }
    }

    public IEnumerator<FP.Tuple<float, IGraphNode<Node>>> GetEnumerator()
    {
        foreach (var neighbor in adyacent)
            yield return FP.Tuple.New(
                Heuristic(neighbor),
                (IGraphNode<Node>)neighbor.Content
            );
    }

    public float Heuristic(IGraphNode<Node> goal)
    {
        return Vector3.Distance(transform.position, goal.Content.transform.position);
    }

    public bool Satisfies(IGraphNode<Node> goal)
    {
        return Content == goal.Content;
    }

    IEnumerator IEnumerable.GetEnumerator()
    {
        return ((IEnumerable<IGraphNode<string>>)this).GetEnumerator();
    }

    void OnDrawGizmos()
    {
        /*
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, 0.3f);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, 1);
        Gizmos.color = Color.green;
        foreach (var wp in adyacent)
        {
            Gizmos.DrawLine(transform.position, wp.transform.position);
        }
        */
    }
}
