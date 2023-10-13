using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using static System.MathF;

namespace DoomFire;

public class Game1 : Game
{
    readonly GraphicsDeviceManager graphics;
    SpriteBatch spriteBatch;

    readonly FireData fireData;
    readonly Matrix scale;

    public Game1(FireData fireData, int width, int height)
    {
        Window.Title = "Doom Fire";
        IsMouseVisible = true;
        IsFixedTimeStep = true;
        TargetElapsedTime = TimeSpan.FromSeconds(1d / 30d);

        this.fireData = fireData;
        var scaleX = Max(Ceiling((float) width / fireData.Cols), 1);
        var scaleY = Max(Ceiling((float) height / fireData.Rows), 1);
        scale = Matrix.CreateScale(scaleX, scaleY, 1);

        graphics = new(this);
        graphics.IsFullScreen = false;
        graphics.PreferredBackBufferWidth = width;
        graphics.PreferredBackBufferHeight = height;
        graphics.ApplyChanges();
    }

    protected override void Initialize()
    {
        spriteBatch = new(GraphicsDevice);
        base.Initialize();
    }

    protected override void Update(GameTime gameTime)
    {
        if (Keyboard.GetState().IsKeyDown(Keys.Space) ||
            Mouse.GetState().LeftButton == ButtonState.Pressed)
            fireData.Wind = fireData.Wind.Toggle();

        if (Keyboard.GetState().IsKeyDown(Keys.Escape))
            Exit();

        fireData.Update();
        base.Update(gameTime);
    }

    protected override void Draw(GameTime gameTime)
    {
        GraphicsDevice.Clear(Color.White);
        spriteBatch.Begin(transformMatrix: scale);

        for (var row = 0; row < fireData.Rows; row++)
        for (var col = 0; col < fireData.Cols; col++)
        {
            Rectangle rect = new(col, row, 1, 1);
            var colorIndex = fireData[row, col];
            var color = ColorPalete.Colors[colorIndex];
            spriteBatch.Draw(spriteBatch.BlankTexture(), rect, color);
        }

        spriteBatch.End();
        base.Draw(gameTime);
    }
}