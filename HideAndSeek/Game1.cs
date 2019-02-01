using HideAndSeek.Sprites;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Penumbra;
using System.Collections.Generic;
using System.Linq;

namespace HideAndSeek
{
  /// <summary>
  /// This is the main type for your game.
  /// </summary>
  public class Game1 : Game
  {
    GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    private PenumbraComponent _penumbra;

    private Light _playerLight;

    private List<Sprite> _sprites;

    public Game1()
    {
      graphics = new GraphicsDeviceManager(this);
      Content.RootDirectory = "Content";

      _penumbra = new PenumbraComponent(this);
      //_penumbra.SpriteBatchTransformEnabled = false;

      this.Components.Add(_penumbra);
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
      // Create a new SpriteBatch, which can be used to draw textures.
      spriteBatch = new SpriteBatch(GraphicsDevice);

      var blockTexture = Content.Load<Texture2D>("Square");

      _sprites = new List<Sprite>()
      {
        new Player(Content.Load<Texture2D>("Ball"))
        {
          Position = new Vector2(125, 125),
          Colour = Color.Green,
        },
        new Sprite(blockTexture)
        {
          Position = new Vector2(25, 75),
        },
        new Door(Content.Load<Texture2D>("Door/Frame"))
        {
          ActualDoor = new Sprite(Content.Load<Texture2D>("Door/Door"))
          {
            IsVisible = false,
          },
          Position = new Vector2(75, 75),
        },
        new Sprite(blockTexture)
        {
          Position = new Vector2(125, 75),
        },
        new Sprite(blockTexture)
        {
          Position = new Vector2(175, 75),
        },
        new Door(Content.Load<Texture2D>("Door/Frame"))
        {
          ActualDoor = new Sprite(Content.Load<Texture2D>("Door/Door"))
          {
            IsVisible = false,
          },
          Rotatation = MathHelper.ToRadians(90),
          Position = new Vector2(175, 125),
        },
        new Sprite(blockTexture)
        {
          Position = new Vector2(175, 175),
        },
      };

      foreach (var sprite in _sprites)
      {
        if (sprite is Player)
          continue;

        if (sprite is Door)
          continue;

        Hull hull = GetHull(sprite);

        _penumbra.Hulls.Add(hull);
      }

      _playerLight = new PointLight()
      {
        Radius = 5,
        Scale = new Vector2(350),
      };

      _penumbra.Lights.Add(_playerLight);
    }

    private static Hull GetHull(Sprite sprite)
    {
      return new Hull(
        new Vector2(-25f, -25f),
        new Vector2(+25f, -25f),
        new Vector2(+25f, +25f),
        new Vector2(-25f, +25f))
      {
        Enabled = true,
        Position = sprite.Position,
        Scale = new Vector2(1.0f),
      };
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
      base.Update(gameTime);

      foreach (var sprite in _sprites)
        sprite.Update(gameTime);

      foreach (var spriteA in _sprites)
      {
        foreach (var spriteB in _sprites)
        {
          // Don't do anything if they're the same sprite!
          if (spriteA == spriteB)
            continue;

          //if (!spriteA.CollisionArea.Intersects(spriteB.CollisionArea))
          //  continue;

          if (spriteA.FutureRectangle.Intersects(spriteB.FutureRectangle))
            spriteA.OnCollide(spriteB);

          if (spriteA.Rectangle.Intersects(spriteB.Rectangle))
            spriteA.OnEnter(spriteB);
          else
            spriteA.OnLeave(spriteB);
        }
      }

      foreach (var sprite in _sprites)
      {
        // TODO: Have a list of hulls inside the Sprite.cs
        if (sprite is Door)
        {
          var door = sprite as Door;

          var hull = GetHull(door.ActualDoor);

          if (door.ActualDoor.IsVisible)
          {
            if (_penumbra.Hulls.All(c => !c.IsSame(hull)))
              _penumbra.Hulls.Add(hull);
          }
          else
          {
            var newHull = _penumbra.Hulls.SingleOrDefault(c => c.IsSame(hull));
            if (newHull != null)
            {
              _penumbra.Hulls.Remove(newHull);

              // Due to a way penumbra works, I have to do a "forced" movement on any light sorce to make it update
              _penumbra.Lights[0].Position += new Vector2(0.001f, 0);
            }
          }

        }
        sprite.PostUpdate(gameTime);
      }

      _playerLight.Position = _sprites[0].Position;
    }

    /// <summary>
    /// This is called when the game should draw itself.
    /// </summary>
    /// <param name="gameTime">Provides a snapshot of timing values.</param>
    protected override void Draw(GameTime gameTime)
    {
      _penumbra.BeginDraw();

      GraphicsDevice.Clear(Color.CornflowerBlue);

      spriteBatch.Begin();

      foreach (var sprite in _sprites)
        sprite.Draw(gameTime, spriteBatch);

      spriteBatch.End();

      base.Draw(gameTime);
    }
  }
}
