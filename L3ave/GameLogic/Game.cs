using System;
using System.Collections.Generic;

using L3ave.Models;
using L3ave.Levels;
using L3ave.Drawing;

namespace L3ave.GameLogic
{
    public class Game
    {
        public const int CellSize = 20;

        public const double MinSize = 0.05;
        
        public const double StepSize = 0.125;
        
        public const double JumpSize = 0.6000000000000001;

        public static float TouchOpacity = 1f;

        public Level Level;

        public Triggers Triggers;

        public float MessageOpacity;

        public string Text;

        public bool Exited;

        public bool Noclip;

        private Dictionary<int, Point> startPositions;

        private Lightning lightning;

        private double velocity;

        private bool standing;

        private string message;

        private int cooldown;

        private int levelCooldown;

        public string Message
        {
            get
            {
                return message;
            }
            set
            {
                message = value;
                MessageOpacity = TouchOpacity;
            }
        }

        public Game()
        {
            Text = "";
            Message = "";
            Triggers = new Triggers();
            startPositions = new Dictionary<int, Point>();

            ChangeLevel(0, null);
        }

        public void ChangeLevel(int number, Point start)
        {
            if (levelCooldown > 0) {
                return;
            }

            Level = LevelLoader.LoadFromFile($"L3ave.Resources.level{number}.txt");

            lightning = new Lightning(Level.Field);

            if (startPositions.ContainsKey(number))
            {
                Level.Position = startPositions[number];
            }

            Level.Position = start ?? Level.Position;
            startPositions[number] = Level.Position;

            levelCooldown = 50;
        }

        public void MoveLeft()
        {
            if (CanMove(-0.125, 0.0) || Noclip)
            {
                Level.Position.X -= 0.125;
            }
        }

        public void MoveRight()
        {
            if (CanMove(0.125, 0.0) || Noclip)
            {
                Level.Position.X += 0.125;
            }
        }

        public void Jump()
        {
            if (Noclip)
            {
                Level.Position.Y -= 0.125;
            }
            else if (standing)
            {
                velocity = -0.6000000000000001;
                standing = false;
            }
        }

        public void MoveDown()
        {
            if (Noclip)
            {
                Level.Position.Y += 0.125;
            }
        }

        public void StartLightning()
        {
            MusicPlayer.Shock();

            if (Level.LightLeft > 0 && cooldown == 0)
            {
                lightning.Begin(Level.Position);
                cooldown = 50;
                Level.LightLeft--;
                Message = Level.LightLeft.ToString();
            }
        }

        public void SwitchNoclip()
        {
            Noclip = !Noclip;
        }

        public void Update()
        {
            cooldown = Math.Max(0, cooldown - 1);
            levelCooldown = Math.Max(0, levelCooldown - 1);

            lightning.Step();

            ApplyGravity();
            CheckEvents();
            RecalculateOpacity();
        }

        private void RecalculateOpacity()
        {
            for (var i = 0; i < Level.Width; i++)
            {
                for (var j = 0; j < Level.Height; j++)
                {
                    var value = Level.Position.X - (double)i;
                    var value2 = Level.Position.Y - (double)j;

                    Level.Field[i, j].Opacity = Math.Max(Level.Field[i, j].Opacity - 0.005f, 0f);

                    if (Math.Abs(value) + Math.Abs(value2) <= 2.0)
                    {
                        Level.Field[i, j].Opacity = TouchOpacity;
                    }
                }
            }

            MessageOpacity = Math.Max(0f, MessageOpacity - 0.005f);
        }

        private void ApplyGravity()
        {
            if (!Noclip)
            {
                if (velocity <= 3.0 && !standing)
                {
                    velocity += 0.05;
                }

                if (velocity < -3.0)
                {
                    velocity = -3.0;
                }

                var maxFallLength = GetMaxFallLength();

                if (CanMove(0.0, maxFallLength))
                {
                    Level.Position.Y += maxFallLength;
                }

                Level.Position.Y = Math.Floor(Level.Position.Y / 0.05) * 0.05;
            }
        }

        private void CheckEvents()
        {
            Text = "";

            foreach (var @event in Level.Events)
            {
                if (@event.Area.HasPoint(Level.Position))
                {
                    @event.Act(this);
                }
            }
        }

        private bool CanMove(double x, double y)
        {
            var num = Level.Position.X + x;
            var num2 = Level.Position.Y + y;

            if (num < 0.0 || num + 1.0 - 0.05 >= (double)Level.Field.GetLength(0) || num2 < 0.0 || num2 + 1.0 - 0.05 >= (double)Level.Field.GetLength(1))
            {
                return false;
            }

            var num3 = (int)Math.Floor(num);
            var num4 = (int)Math.Floor(num2);
            var num5 = (int)Math.Floor(num + 1.0 - 0.05);
            var num6 = (int)Math.Floor(num2 + 1.0 - 0.05);

            return Level.Field[num3, num4].Type == CellType.Empty
                && Level.Field[num5, num4].Type == CellType.Empty
                && Level.Field[num3, num6].Type == CellType.Empty
                && Level.Field[num5, num6].Type == CellType.Empty;
        }

        private double GetMaxFallLength()
        {
            var num = 0.0;
            var num2 = 0.05 * (double)Math.Sign(velocity);

            while (Math.Abs(num + num2) <= Math.Abs(velocity) && CanMove(0.0, num + num2))
            {
                num += num2;
            }

            if (num == 0.0 && velocity >= 0.05)
            {
                standing = true;
            }

            return Math.Floor(num / 0.05) * 0.05;
        }
    }
}
