using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public class Char
{
	private int id;
	private byte job;
	private bool isSpawn;
	private GameObject renderer;
	public Char(int id, byte job)
	{
		this.id = id;
		this.job = job;
	}

	public int getID()
	{
		return id;
	}
	public bool isSpawned()
	{
		return isSpawn;
	}

	public void setSpawn(bool isSpawn)
	{
		this.isSpawn = isSpawn;
	}

	public byte getJob()
	{
		return job;
	}

	public void setRenderer(GameObject renderer)
	{
		this.renderer = renderer;
	}

	public GameObject getRenderer()
	{
		return renderer;
	}
}

