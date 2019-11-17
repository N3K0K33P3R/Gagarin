﻿using Empty.Building;
using Microsoft.Xna.Framework.Graphics;
using MonoFlash.Engine;
using System;
using System.Collections.Generic;

namespace Empty.UI.Building
{
	public class Interface : Sprite
	{
		public static List<BuildingCell> cells;

		public Interface(Action<BuildType> build)
		{
			cells = new List<BuildingCell>();
			AddCell(Game1.stoneTexture, BuildType.Cannon, build);
			AddCell(Game1.woodTexture,  BuildType.Wall,   build);
			AddCell(Game1.ironTexture,  BuildType.Bridge, build);
			DrawCells();
		}

		public static void UpdateInterface()
		{
			Property.mainProperty.UpdateMainProperties();

			foreach (BuildingCell cell in cells)
			{
				cell.costs.RemoveChildren();
				cell.costs.props.Clear();
				cell.DrawCosts();
			}
		}


		public void AddCell(Texture2D icon, BuildType buildtype, Action<BuildType> build)
		{
			Structure str = StructureFabric.GetStructure(buildtype);

			cells.Add(new BuildingCell(icon, str.StoneCost, str.TimberCost, str.IronCost, buildtype, build));
		}

		private void DrawCells()
		{
			for (var i = 0; i < cells.Count; i++)
			{
				cells[i].x = Values.SCREEN_WIDTH / 2 - cells.Count * cells[i].quadWidth / 2 + i * cells[i].quadWidth;
				cells[i].y = Values.SCREEN_HEIGHT - cells[i].quadWidth;
				AddChild(cells[i]);
			}
		}


		public enum BuildType
		{
			Cannon,
			Wall,
			Bridge
		}
	}
}