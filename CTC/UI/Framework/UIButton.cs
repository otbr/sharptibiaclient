﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;

namespace CTC
{
    public class UIButton : UIView
    {
        public String Label;

        public UIButton(UIView Parent)
            : base(Parent)
        {
            ElementType = UIElementType.Button;
            Bounds = new Rectangle(0, 0, 32, 32);
        }

        protected bool _Highlighted = false;
        public virtual bool Highlighted
        {
            get
            {
                return _Highlighted;
            }
            set
            {
                if (value)
                    ElementType = UIElementType.ButtonHighlight;
                else
                    ElementType = UIElementType.Button;
                _Highlighted = value;
            }
        }

        public override bool MouseLeftClick(MouseState mouse)
        {
            if (mouse.LeftButton == ButtonState.Pressed)
            {
                if (CaptureMouse())
                    Highlighted = true;
            }
            else if (mouse.LeftButton == ButtonState.Released && Highlighted)
            {
                ReleaseMouse();
                // ACTION!
            }
            return true;
        }

        public override bool MouseLost()
        {
            Highlighted = false;
            return true;
        }

        protected override void DrawContent(SpriteBatch CurrentBatch)
        {
            if (Label != null)
            {
                Vector2 Size = Context.StandardFont.MeasureString(Label);
                Vector2 Offset = new Vector2(
                    (int)((ClientBounds.Width - Size.X) / 2),
                    (int)((ClientBounds.Height - Size.Y) / 2)
                );

                CurrentBatch.DrawString(
                    Context.StandardFont, Label, ScreenCoordinate(Offset),
                    Color.LightGray,
                    0.0f, new Vector2(0.0f, 0.0f),
                    1.0f, SpriteEffects.None, 0.5f
                );
            }
        }
    }
}