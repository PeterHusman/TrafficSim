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

        List<Street> streets = new List<Street>();

        float scalar = 0.1f;

        Street s;

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

            s = new Street(Direction.East, 1000);
            s.Cars = new LinkedList<Car>(cars);

            streets.Add(s);
            streets.Add(new Street(Direction.West, 1500));

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
                streets[0].Cars.AddFirst(new Car(Vector2.One * 1000, 0.01f, 0.2f, Direction.East, 50f, 30f, 800));
            }
            if (mouse.RightButton == ButtonState.Pressed && oldMouse.RightButton == ButtonState.Released)
            {
                streets[1].Cars.AddFirst(new Car(new Vector2(15000,1500), 0.01f, 0.2f, Direction.West, 50f, 30f, 800));
            }

            scalar = mouse.ScrollWheelValue * 0.0001f + 0.1f;

            foreach (Street street in streets)
            {
                foreach (Car car in street.Cars)
                {
                    //Car follows lane
                    car.Direction = street.Direction;


                    //Brake if cars are ahead
                    if (street.Cars.Find(car).Next != null)
                    {
                        float distance = car.Position.X - street.Cars.Find(car).Next.Value.Position.X + (car.Position.Y - street.Cars.Find(car).Next.Value.Position.Y);
                        distance = Math.Abs(distance);
                        if (distance < car.BrakingDistance)
                        {
                            car.TargetSpeed = Math.Max(0f, car.TargetSpeed * distance/car.BrakingDistance);
                        }
                        else
                        {
                            car.TargetSpeed = car.MaxSpeed;
                        }
                    }
                    car.Update();
                }
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
            
            GraphicsDevice.Clear(Color.CornflowerBlue);

            spriteBatch.Begin();

            foreach (Street street in streets)
            {
                foreach (Car car in street.Cars)
                {
                    spriteBatch.Draw(Pixel, new Rectangle((int)(car.Position.X * scalar), (int)(car.Position.Y * scalar), (int)((((int)car.Direction % 2 + 1)) * 50 * scalar), (int)(((((int)car.Direction + 1) % 2 + 1)) * 50 * scalar)), Color.Red);
                }
            }




            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
