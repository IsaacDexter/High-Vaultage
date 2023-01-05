using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class s_lamp : s_triggerable
{
    /// <summary>A reference to the Lights bulb</summary>
    [SerializeField] private GameObject m_bulb;

    /// <summary>The material to use while the lamp is not lit</summary>
    [SerializeField] private Material m_materialUnlit;
    /// <summary>The material to use while the lamp is lit</summary>
    [SerializeField] private Material m_materialLit;

    private void SetLampMaterial(Material material)
    {
        m_bulb.GetComponent<MeshRenderer>().material = material;
    }

    override public void Activate()
    {
        base.Activate();
        Light();
    }

    override public void Deactivate()
    {
        base.Deactivate();
        Dim();
    }

    private void Light()
    {
        SetLampMaterial(m_materialLit);
    }

    private void Dim()
    {
        SetLampMaterial(m_materialUnlit);
    }
}
