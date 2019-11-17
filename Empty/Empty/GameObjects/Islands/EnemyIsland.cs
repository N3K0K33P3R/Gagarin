using Empty.Building;
using MonoFlash.Engine;
using System;

namespace Empty.GameObjects
{
	public class EnemyIsland : Island
	{
		private readonly AnimationController acY;
		private readonly AnimationController acSpeed;
		public           float               Velocity     { get; set; }
		public           float               Acceleration { get; set; }

		public bool IsDefeated { get; set; }

		public bool Builded = false;

		/// <inheritdoc />
		public EnemyIsland(int w = IslandSize, int h = IslandSize) : base(w, h)
		{
			y            = -h * 2 * Values.TILE_SIZE;
			Velocity     = (float)Values.GlobalSpeed;
			Acceleration = -0.1f;

			acY     = new AnimationController((float)y);
			acSpeed = new AnimationController((float)Values.GlobalSpeed);

			acY.StartAnimation(Maths.easeInOutQuad, y, 0, 0.005);
			acSpeed.StartAnimation(Maths.easeInOutQuad, Values.GlobalSpeed, 0, 0.005);
		}

		/// <inheritdoc />
		public override void Update(float delta)
		{
			if (IsDefeated)
			{
				y += Values.GlobalSpeed;
			}
			else
			{
				y = acY.MakeStep(delta);
			}

			Values.GlobalSpeed = acSpeed.MakeStep(delta);
            UpdateAI();
			base.Update(delta);
		}

		public void Kill()
		{
			IsDefeated = true;
			acSpeed.StartAnimation(Maths.easeInOutQuad, 0, 1, 0.005);
		}

		private double GetTime() => -(Math.Sqrt(2 * Acceleration * x + Velocity * Velocity) + Velocity) / Acceleration;

        private void UpdateAI()
        {
            int randomHuman = -1;
            bool allIsNotMoving = true;
            for (int i = 0; i < humans.Count; i++)
            {
                if (!humans[i].isMoving) randomHuman = i;
            }
            if (randomHuman != -1)
            {
                int randomX;
                int randomY;

                int whatToDo = Values.RANDOM.Next(0, 100);
                if (humans[randomHuman].tilePos.X <= 4 && whatToDo < 45)
                {
                    int whatToBuild = Values.RANDOM.Next(0, 100);
                    UI.Building.Interface.BuildType bt = UI.Building.Interface.BuildType.Bridge;
                    if (whatToBuild < 33) bt = UI.Building.Interface.BuildType.Cannon;
                    if (whatToBuild > 66) bt = UI.Building.Interface.BuildType.Wall;
                    if (bt != UI.Building.Interface.BuildType.Bridge)
                    {
                        if (humans[randomHuman].tilePos.X - 1 >= 0)
                        {
                            if (Cells[humans[randomHuman].tilePos.X - 1, humans[randomHuman].tilePos.Y] == TileType.Grass ||
                            Cells[humans[randomHuman].tilePos.X - 1, humans[randomHuman].tilePos.Y] == TileType.Sand ||
                            Cells[humans[randomHuman].tilePos.X - 1, humans[randomHuman].tilePos.Y] == TileType.Stone)
                            {
								if (!Builded)
								{
									Building.Structure structure = Building.StructureFabric.GetStructure(bt);
									Structures.Add(structure);
									structure.position.X =  humans[randomHuman].tilePos.X - 1;
									structure.position.Y =  humans[randomHuman].tilePos.Y;
									structure.position   *= Values.TILE_SIZE;
									structure.parent     =  this;
									Builded = true;
								}

								//    Building.BuildProcessing.island = this;
                                //    Building.BuildProcessing.SetBuild(structure);
                                /*Building.BuildProcessing.vector.X = humans[randomHuman].tilePos.X - 1;
                                Building.BuildProcessing.vector.Y = humans[randomHuman].tilePos.Y;
                                Building.Structure structure = Building.StructureFabric.GetStructure(bt);
                                Building.BuildProcessing.CallBuilding(this, structure);
                                Building.BuildProcessing.vector = Building.BuildProcessing.curStructure.position;*/
                            }
                        }
                            
                    }
                }
                else
                {
                    do
                    {
                        randomX = Values.RANDOM.Next(0, Cells.GetLength(0));
                        randomY = Values.RANDOM.Next(0, Cells.GetLength(1));
                    } while (Cells[randomX, randomY] != TileType.Grass &&
                        Cells[randomX, randomY] != TileType.Sand &&
                        Cells[randomX, randomY] != TileType.Stone);

                    if (allIsNotMoving) humans[randomHuman].SetTilePos(randomX, randomY);
                }
            }
            

        }
	}
}