using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PlatformerMac
{
	/// <summary>
	/// Attack or defensive action taken by player (using WASD keys)
	/// </summary>
	abstract class Attack
	{
		#region ActionState
		public abstract bool isOffensive
		{
			get;
		}

		public abstract bool isTargetNeeded
		{
			get;
		}
		#endregion

		#region Stage
		/// <summary>
		/// Stage of execution for each attack
		/// </summary>

		public enum AttackStage
		{
			NotStarted,
			Preparing,
			Advancing,
			Executing,
			Returning,
			Finishing,
			Complete,
		};

		protected AttackStage current = AttackStage.NotStarted;

		public AttackStage CurrentStage
		{
			get { return current; }
		}

		protected virtual void AdvanceStage()
		{
			switch (current)
			{
				case AttackStage.Preparing:
					break;
				case AttackStage.Advancing:
					break;
				case AttackStage.Executing:
					break;
				case AttackStage.Returning:
					break;
				case AttackStage.Finishing:
					break;
				case AttackStage.Complete:
					break;
				
			}
		}

		protected virtual void UpdateCurrentStage(GameTime gameTime)
		{
			switch(current)
			{
				case AttackStage.NotStarted;
					break;
				case AttackStage.Preparing:
					break;
				case AttackStage.Advancing:
					break;
				case AttackStage.Executing:
					break;
				case AttackStage.Returning:
					break;
				case AttackStage.Finishing:
					break;
				case AttackStage.Complete:
					break;


			}
		}
	}
}

