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

		protected virtual bool IsReadyForNextStage
		{
			get
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

				}
			}

			return true;
		}

		#endregion

		#region Enemy

		protected Enemy enemy;

		public Enemy Enemy
		{
			get { return enemy; }
		}

		public virtual bool IsCharacterValidUser
		{
			get { return true; }
		}

		#endregion

		#region Target

		public Enemy Target = null;

		protected int adjacentTargets = 0;

		public int AdjacentTargets
		{
			get { return adjacentTargets; }
		}

		#endregion

		#region Heuristic
		/// <summary>
		/// Gets the heuristic.
		/// </summary>
		/// <value>The heuristic.</value>
		/// <remarks>
		/// I am unsure if we actually need this part as of yet.
		/// </remarks>
		public abstract int Heuristic
		{
			get;
		}

		public static int CompareAttacksByHeuristic(Attack a, Attack b)
		{
			return b.Heuristic.CompareTo(a.Heuristic);
		}

		#endregion

		#region Initialization
		public Attack(Enemy enemy)
		{
			if (enemy == null)
			{
				throw new ArgumentNullException("enemy");
			}

			this.enemy = enemy;

			Reset();
		}

		public virtual void Reset()
		{
			current = AttackStage.NotStarted;
		}

		public virtual void Start(){
			current = AttackStage.Preparing;
			AdvanceStage();
		}

		#endregion

		#region Updating

		public virtual void Update(GameTime gameTime)
		{
			UpdateCurrentStage(gameTime);

			if((current != AttackStage.NotStarted) && (current != AttackStage.Complete) && IsReadyForNextStage)
			{
				switch (current)
				{
					case AttackStage.Preparing:
						current = AttackStage.Advancing;
						break;
					case AttackStage.Advancing:
						current = AttackStage.Executing;
						break;
					case AttackStage.Executing:
						current = AttackStage.Returning;
						break;
					case AttackStage.Returning:
						current = AttackStage.Finishing;
						break;
					case AttackStage.Finishing:
						current = AttackStage.Complete;
						break;
				}
				AdvanceStage();
			}
		}

		#endregion

		#region Drawing

		public virtual void Draw(GameTime gameTime, SpriteBatch spriteBatch){}

		#endregion
	}
}

