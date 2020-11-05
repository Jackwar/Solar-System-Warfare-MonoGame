using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Solar_System_Warfare.Movement;
using Solar_System_Warfare.Spites;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Security.Principal;
using System.Text;

namespace Solar_System_Warfare
{
    class ShipPool
    {
        private const int LaserSpeed = 600;
        private readonly Random rand = new Random();
        public Lives Lives { get; private set; } = new Lives();
        public ScoreUpdater ScoreUpdater { get; private set; } = new ScoreUpdater();
        public bool EarthAlive { get; set; } = false;
        public Earth Earth { get; set; }
        public List<Sprite> Ships { get; set; }
        public Stack<Sprite> SpawnStack { get; set; }
        public SpawnTimer SpawnTimer { get; private set; }

        public ShipPool(Earth earth)
        {
            Earth = earth;
            SpawnTimer = new SpawnTimer();
            Ships = new List<Sprite>
            {
                earth
            };
            SpawnStack = new Stack<Sprite>();
        }

        public void Reset()
        {
            Earth.Reset();
            SpawnTimer = new SpawnTimer();
            Ships = new List<Sprite>
            {
                Earth
            };
            SpawnStack = new Stack<Sprite>();
            ScoreUpdater = new ScoreUpdater();
            Lives = new Lives();
            EarthAlive = true;
        }
        public void Update(GameTime gameTime, ContentManager content, KeyboardState kState, Vector2 leftStick)
        {
            //Check if any ship entities are out of bounds
            //Or if they collided with another ship entity
            //The game ends here if the Earth loses all durability
            for (int i = 0; i < Ships.Count; i++)
            {
                if(OutOfBoundsCheck(Ships[i]))
                {
                    continue;
                }
                for (int i2 = 0; i2 < Ships.Count; i2++)
                {
                    if(i >= Ships.Count)
                    {
                        break;
                    }
                    if(i2 == i)
                    {
                        continue;
                    }
                    CollisionCheck(Ships[i], Ships[i2]);
                }
            }

            if (EarthAlive)
            {
                //Spawn enemies
                if (SpawnTimer.SpawnEnemy(gameTime))
                {
                    var patterns = GetEnemyPattern();
                    var enemy = new Enemy(patterns.position, 200, 1, patterns.pattern);
                    SpawnStack.Push(enemy);
                }

                //Move the player and fire lasers if space is pressed
                Earth.PlayerDirection(kState, leftStick);
                Earth.MoveEarth(gameTime);
                if (Earth.Firing(gameTime, kState))
                {
                    SpawnStack.Push(new EarthProjectile(
                        new Vector2(Earth.Position.X - 5, Earth.Position.Y - 5),
                        LaserSpeed,
                        1)
                        {
                            Direction = new Vector2(0, -1)
                        });
                    SpawnStack.Push(new EarthProjectile(
                        new Vector2(Earth.Position.X + Earth.Texture.Width - 5, Earth.Position.Y - 5),
                        LaserSpeed,
                        1)
                        {
                            Direction = new Vector2(0, -1)
                        });
                }

                //Move all ship entities minus the player
                for (int i = 1; i < Ships.Count; i++)
                {
                    if (Ships[i] is Enemy enemy)
                    {
                        if (enemy.Firing(gameTime))
                        {
                            SpawnStack.Push(new EnemyProjectile(
                                //content.Load<Texture2D>("Projectile"),
                                new Vector2(enemy.Position.X + (enemy.Texture.Width / 2), enemy.Position.Y + enemy.Texture.Height + 5),
                                LaserSpeed,
                                1)
                                {
                                    Direction = new Vector2(0, 1)
                                });
                        }
                    }
                    Ships[i].Move(gameTime);
                }

                //Add all ships in the spawn stack to the ship pool
                while (SpawnStack.Count != 0)
                {
                    var ship = SpawnStack.Pop();
                    Ships.Add(ship);
                    ship.PoolIndex = Ships.Count - 1;
                }
            }
        }

        public void Draw(SpriteBatch spriteBatch)
        {
            foreach(var ship in Ships)
            {
                ship.Draw(spriteBatch);
            }
        }

        private bool OutOfBoundsCheck(Sprite sprite)
        {
            if(sprite.Position.X > 700)
            {
                FillAndRemove(sprite);
                return true;
            }
            if(sprite.Position.X < -100)
            {
                FillAndRemove(sprite);
                return true;
            }
            if(sprite.Position.Y < -100)
            {
                FillAndRemove(sprite);
                return true;
            }
            if(sprite.Position.Y > 900)
            {
                FillAndRemove(sprite);
                return true;
            }
            return false;
        }
        
        private void CollisionCheck(Sprite sprite1, Sprite sprite2)
        {
            var rectangle1 = new Rectangle(
                (int)sprite1.Position.X, (int)sprite1.Position.Y, sprite1.Texture.Width, sprite1.Texture.Height);
            var rectangle2 = new Rectangle(
                (int)sprite2.Position.X, (int)sprite2.Position.Y, sprite2.Texture.Width, sprite2.Texture.Height);
            if (rectangle1.Intersects(rectangle2))
            {
                if(sprite1 is Earth)
                {
                    if(sprite2 is EnemyProjectile || sprite2 is Enemy)
                    {
                        Damage1(sprite1, sprite2);
                    }
                }
                else if(sprite1 is Enemy)
                {
                    if(sprite2 is EarthProjectile || sprite2 is Earth)
                    {
                        Damage1(sprite1, sprite2);
                    }
                }
                else if(sprite1 is EarthProjectile)
                {
                    if(sprite2 is Enemy)
                    {
                        Damage1(sprite1, sprite2);
                    }
                }
                else if(sprite1 is EnemyProjectile)
                {
                    if(sprite2 is Earth)
                    {
                        Damage1(sprite1, sprite2);
                    }
                }
            }
        }

        private void FillAndRemove(Sprite sprite)
        {
            if (EarthAlive)
            {
                if (sprite is Earth)
                {
                    EarthAlive = false;
                    Ships.Clear();
                    return;
                }
                if (sprite.PoolIndex != Ships.Count - 1)
                {
                    var tempSprite = Ships[^1];
                    Ships.RemoveAt(Ships.Count - 1);
                    Ships[sprite.PoolIndex] = tempSprite;
                    tempSprite.PoolIndex = sprite.PoolIndex;
                }
                else
                {
                    Ships.RemoveAt(Ships.Count - 1);
                }
            }
        }

        private void Damage1(Sprite sprite1, Sprite sprite2)
        {
            sprite1.Damage(1);
            sprite2.Damage(1);
            if (sprite1.Durability <= 0)
                FillAndRemove(sprite1);
            if (sprite2.Durability <= 0)
                FillAndRemove(sprite2);
            if(sprite1 is Earth || sprite2 is Earth)
            {
                Lives.RemoveLife(Earth.Durability);
            }
            if(sprite1 is Enemy || sprite2 is Enemy)
            {
                ScoreUpdater.AddScore();
            }
        }

        private (Vector2 position, IPattern pattern) GetEnemyPattern()
        {
            int randNum = rand.Next(4);
            int movementEnd = rand.Next(300, 500);

            if(randNum == 0)
            {
                var position = new Vector2(-50, rand.Next(100));
                var pattern = new SwishPattern(movementEnd);
                return (position, pattern);
            }
            else if (randNum == 1)
            {
                var position = new Vector2(650, rand.Next(100));
                var pattern = new SwishPattern(movementEnd);
                return (position, pattern);
            }
            else if (randNum == 2)
            {
                var position = new Vector2(rand.Next(25, 575), -50);
                var pattern = new UpDownPattern(movementEnd, true);
                return (position, pattern);
            }
            else
            {
                var position = new Vector2(rand.Next(25, 575), -50);
                var pattern = new UpDownPattern(movementEnd, false);
                return (position, pattern);
            }
        }
    }
}
