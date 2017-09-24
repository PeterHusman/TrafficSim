using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;

namespace TrafficSim
{
    /// <summary>
    /// This is the main type for your game.
    /// </summary>
    public class Game1 : Game
    {
        GraphicsDeviceManager graphics;
        SpriteBatch spriteBatch;


        MouseState mouse;
        MouseState oldMouse;

        KeyboardState keyboard;
        KeyboardState oldKeyboard;

        List<Car> cars = new List<Car>();

        public static Texture2D Pixel;

        public Game1()
        {
            graphics = new GraphicsDeviceManager(this);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here

            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            IsMouseVisible = true;

            graphics.PreferredBackBufferHeight = 1080;
            graphics.PreferredBackBufferWidth = 1920;
            graphics.ApplyChanges();

            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            Pixel = new Texture2D(GraphicsDevice, 1, 1);
            Pixel.SetData(new Color[] { Color.White });

            cars.Add(new Car(Vector2.One * 1000, 1f, 0.3f, Direction.North, 100f));

            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// game-specific content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            mouse = Mouse.GetState();
            keyboard = Keyboard.GetState();

            if (keyboard.IsKeyDown(Keys.W))
            {
                cars[0].Direction = Direction.North;
            }
            else if (keyboard.IsKeyDown(Keys.D))
            {
                cars[0].Direction = Direction.East;
            }
            else if (keyboard.IsKeyDown(Keys.S))
            {
                cars[0].Direction = Direction.South;
            }
            else if (keyboard.IsKeyDown(Keys.A))
            {
                cars[0].Direction = Direction.West;
            }
            else if (keyboard.IsKeyDown(Keys.Up) && oldKeyboard.IsKeyUp(Keys.Up))
            {
                cars[0].TargetSpeed += 10;
            }
            else if (keyboard.IsKeyDown(Keys.Down) && oldKeyboard.IsKeyUp(Keys.Down))
            {
                cars[0].TargetSpeed -= 10;
            }
            if (mouse.LeftButton == ButtonState.Pressed && oldMouse.LeftButton == ButtonState.Released)
            {
                cars.Add(new Car(Vector2.One * 1000, 0.1f, 4f, Direction.East, 100f, 30f));
            }

            foreach (Car car in cars)
            {
                LinkedList<Car> linked = new LinkedList<Car>(new Car[] { });

                /*foreach(Car car2 in cars)
                {
                    float decelerationConst = 400f;
                    float distBetween = 400f;


                    if(car.Position == car2.Position)
                    {
                        break;
                    }

                    if(    (car2.Position.X == car.Position.X && car2.Position.Y - car.Position.Y <= distBetween && car2.Position.Y - car.Position.Y > 0 && car.Direction == car2.Direction && car.Direction == Direction.South))
                    {
                        car.TargetSpeed -= decelerationConst / (car2.Position.Y - car.Position.Y);
                    }
                    if ((car2.Position.X == car.Position.X && car.Position.Y - car2.Position.Y <= distBetween && car.Position.Y - car2.Position.Y > 0 && car.Direction == car2.Direction && car.Direction == Direction.North))
                    {
                        car.TargetSpeed -= decelerationConst / (car.Position.Y - car2.Position.Y);
                    }
                    if ((car2.Position.Y == car.Position.Y && car2.Position.X - car.Position.X <= distBetween && car2.Position.X - car.Position.X > 0 && car.Direction == car2.Direction && car.Direction == Direction.West))
                    {
                        car.TargetSpeed -= decelerationConst / (car2.Position.X - car.Position.X);
                    }
                    if ((car2.Position.Y == car.Position.Y && car.Position.X - car2.Position.X <= distBetween && car.Position.X - car2.Position.X > 0 && car.Direction == car2.Direction && car.Direction == Direction.East))
                    {
                        car.TargetSpeed -= decelerationConst / (car.Position.X - car2.Position.X);
                    }
                    //    || (car2.Position.X == car.Position.X && car.Position.Y - car2.Position.Y >= -200 && car.Direction == car2.Direction && car.Direction == Direction.North)
                    //    || (car2.Position.Y == car.Position.Y && car.Position.X - car2.Position.X >= -200 && car.Direction == car2.Direction && car.Direction == Direction.West)
                    //    || (car2.Position.Y == car.Position.Y && car2.Position.X - car.Position.X >= -200 && car.Direction == car2.Direction && car.Direction == Direction.East))
                    //{
                    //    car.TargetSpeed--;
                    //}
                    ////else
                    ////{
                    ////    car.TargetSpeed *= 1.0001f;
                    ////}
                }*/
                car.Update();
            }


            oldKeyboard = keyboard;
            oldMouse = mouse;
            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            float scalar = 0.1f;
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Car car in cars)
            {
                spriteBatch.Draw(Pixel, new Rectangle((int)(car.Position.X * scalar), (int)(car.Position.Y * scalar), (int)((((int)car.Direction % 2 + 1)) * 50 * scalar), (int)(((((int)car.Direction + 1) % 2 + 1)) * 50 * scalar)), Color.Red);
            }




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
