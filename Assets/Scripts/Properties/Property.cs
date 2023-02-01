using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// A property has an owner. Abstract class.
/// </summary>
public abstract class Property
{
    /// <summary>
    /// Owner of the property
    /// </summary>
    protected MonopolyCharacter owner;

    public MonopolyCharacter Owner
    {
        get => owner;
        set
        {
            if (owner)
                return;

            owner = value;
        }
    }

    /// <summary>
    /// Set informations of a property as default, including owner.
    /// </summary>
    public virtual void Reset()
    {
        owner = null;
    }
}
