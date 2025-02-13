using System;
using System.Collections.Generic;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

using L3ave.Models;

namespace L3ave.Drawing
{
    public class Drawer
    {
        private static int TailLength = 4;

        private static Color PlayerColor = Color.LimeGreen;

        private static Dictionary<CellType, Color> CellColors = new Dictionary<CellType, Color>
        {
            {
                CellType.Danger,
                Color.Red
            },
            {
                CellType.Wall,
                Color.DarkGray
            },
            {
                CellType.Empty,
                Color.White
            }
        };

        private List<Texture2D> overlay;

        private LinkedList<Models.Point> tail;

        private Rectangle drawingArea;

        private SpriteBatch batch;

        private Texture2D texture;

        private Rectangle window;

        private SpriteFont font;

        private GameLogic.Game game;

        private int cellSize;

        private string lastMessage = "";

        private string lastText = "";

        public Drawer(ContentManager content, SpriteBatch batch, GameLogic.Game game)
        {
            this.game = game;
            this.batch = batch;

            cellSize = 20;

            overlay =
            [
                content.Load<Texture2D>("overlay-right-top"),
                content.Load<Texture2D>("overlay-left-top"),
                content.Load<Texture2D>("overlay-right-bottom"),
                content.Load<Texture2D>("overlay-left-bottom"),
            ];

            texture = content.Load<Texture2D>("white");
            font = content.Load<SpriteFont>("impact");
            tail = new LinkedList<Models.Point>();
        }

        public void Resize(Rectangle clientBounds)
        {
            window = clientBounds;

            cellSize = Math.Min(clientBounds.Width / game.Level.Width, clientBounds.Height / game.Level.Height);

            var num = cellSize * game.Level.Width;
            var num2 = cellSize * game.Level.Height;

            drawingArea = new Rectangle((clientBounds.Width - num) / 2, (clientBounds.Height - num2) / 2, num, num2);

            tail.Clear();

            for (var i = 0; i < TailLength; i++)
            {
                tail.AddFirst(game.Level.Position.Clone());
            }
        }

        public void Draw()
        {
            var noiseSize = cellSize / 4;
            var noiseSize2 = cellSize;

            DrawNoise(noiseSize, 0.1);
            DrawNoise(noiseSize2, 0.05);
            DrawField();
            DrawPlayer();
            DrawText();
            DrawOverlay();
        }

        private void DrawField()
        {
            for (var i = 0; i < game.Level.Field.GetLength(0); i++)
            {
                for (var j = 0; j < game.Level.Field.GetLength(1); j++)
                {
                    var opacity = game.Level.Field[i, j].Opacity;

                    if (opacity != 0f && game.Level.Field[i, j].Type != 0)
                    {
                        var rect = new Rectangle(
                            drawingArea.X + i * cellSize,
                            drawingArea.Y + j * cellSize,
                            cellSize - 1,
                            cellSize - 1
                        );

                        var color = (game.Level.Field[i, j].Type == CellType.Empty)
                            ? (CellColors[game.Level.Field[i, j].Type] * opacity)
                            : Color.Lerp(Color.White, CellColors[game.Level.Field[i, j].Type], opacity);

                        batch.Draw(texture, rect, color);
                    }
                }
            }
        }

        private void DrawPlayer()
        {
            var num = 1f;

            Random random = new Random();

            tail.AddFirst(game.Level.Position.Clone());
            tail.RemoveLast();

            var point = new Models.Point();

            foreach (var item in tail)
            {
                var rect = new Rectangle(
                    (int)Math.Round((double)drawingArea.X + item.X * (double)cellSize + point.X),
                    (int)Math.Round((double)drawingArea.Y + item.Y * (double)cellSize + point.Y),
                    cellSize - 1,
                    cellSize - 1
                );

                batch.Draw(texture, rect, PlayerColor * num);
                num = 0.3f;
                point = point.Offset(-4.0 + random.NextDouble() * 2.0 * 4.0, -4.0 + random.NextDouble() * 2.0 * 4.0);
            }
        }

        private void DrawText()
        {
            var num = drawingArea.Height / 8;
            var num2 = drawingArea.Height / 16;

            if ((game.Message != lastMessage && game.Message != "") || (game.Text != lastText && game.Text != ""))
            {
                MusicPlayer.Tick();

                lastMessage = game.Message;
                lastText = game.Text;
            }

            DrawPhrase(game.Message, drawingArea.Center.ToVector2(), num, Color.Gray * game.MessageOpacity);
            DrawPhrase(game.Text, new Vector2(drawingArea.X + (int)((game.Level.Position.X + 0.5) * (double)cellSize), drawingArea.Y + (int)(game.Level.Position.Y * (double)cellSize - (double)num2)), num2, Color.LimeGreen);
        }

        private void DrawNoise(int noiseSize, double freq)
        {
            var num = window.Width / Math.Max(1, noiseSize);
            var num2 = window.Height / Math.Max(1, noiseSize);

            var array = Noise.Generate(num, num2, freq);

            for (var i = 0; i < num; i++)
            {
                for (var j = 0; j < num2; j++)
                {
                    batch.Draw(texture, new Rectangle(i * noiseSize, j * noiseSize, noiseSize - 1, noiseSize - 1), Color.DarkGray * (float)array[i, j]);
                }
            }
        }

        private void DrawOverlay()
        {
            var num = Math.Min(window.Width, window.Height) / 2;
            var num2 = window.Width - num;
            var num3 = window.Height - num;

            for (var i = 0; i < overlay.Count; i++)
            {
                var x = (i % 2 == 0) ? num2 : 0;
                var y = (i > 1) ? num3 : 0;

                batch.Draw(overlay[i], new Rectangle(x, y, num, num), Color.White);
            }
        }

        private void DrawPhrase(string text, Vector2 center, float lineHeight, Color color)
        {
            var num = font.LineSpacing / 4;
            var array = text.Split(['\n']);

            for (var i = 0; i < array.Length; i++)
            {
                var vector = font.MeasureString(array[i]);
                var num2 = lineHeight / vector.Y;

                var position = new Vector2(
                    center.X - num2 * vector.X / 2f,
                    center.Y + ((float)i - 0.5f - (float)(array.Length / 2)) * (lineHeight - (float)num)
                );

                batch.DrawString(
                    font,
                    array[i],
                    position,
                    color,
                    0f,
                    Vector2.Zero,
                    num2,
                    SpriteEffects.None,
                    0f
                );
            }
        }
    }
}
