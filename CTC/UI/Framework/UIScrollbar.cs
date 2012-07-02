﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CTC
{
    public class UIScrollbar : UIView
    {
        private int _ScrollbarLength;
        /// <summary>
        /// Length of the scrollbar
        /// </summary>
        public int ScrollbarLength
        {
            get
            {
                return _ScrollbarLength;
            }
            set
            {
                if (_ScrollbarPosition < value)
                    _ScrollbarPosition = value;
                _ScrollbarLength = value;
            }
        }

        private int _ScrollbarPosition;
        /// <summary>
        /// Position of the scrollbar
        /// </summary>
        public int ScrollbarPosition
        {
            get
            {
                return _ScrollbarPosition;
            }
            set
            {
                if (value > ScrollbarLength)
                    throw new ArgumentException("Scrollbar Position cannot be greater than Length.");
                else if (value < 0)
                    throw new ArgumentException("Scrollbar Position cannot be less than 0.");
                else
                    _ScrollbarPosition = value;
            }
        }

        protected UIButton TopButton;
        protected UIButton BottomButton;
        protected UIButton GemButton;

        /// <summary>
        /// Constructor of the scrollbar
        /// </summary>
        /// <param name="Parent"></param>
        public UIScrollbar(UIView Parent)
            : base(Parent)
        {
            ElementType = UIElementType.ScrollbarBackground;
            TopButton = new UIButton(this);
            TopButton.ElementType = UIElementType.ScrollbarTop;
            AddSubview(TopButton);

            BottomButton = new UIButton(this);
            BottomButton.ElementType = UIElementType.ScrollbarBottom;
            AddSubview(BottomButton);

            GemButton = new UIButton(this);
            GemButton.ElementType = UIElementType.ScrollbarGem;
            AddSubview(GemButton);

            ScrollbarLength = 1000;
            ScrollbarPosition = 0;
        }

        public override void Update(GameTime time)
        {
            base.Update(time);
        }

        protected override void DrawContent(SpriteBatch CurrentBatch)
        {
            // Draw top button
            Rectangle top = new Rectangle();
            top.Width = (int)Context.Skin.Measure(UIElementType.ScrollbarTop, UISkinOrientation.Center).X;
            top.Height = (int)Context.Skin.Measure(UIElementType.ScrollbarTop, UISkinOrientation.Center).Y;
            TopButton.Bounds = top;

            // Draw bottom button
            Rectangle bottom = new Rectangle();
            bottom.Width = (int)Context.Skin.Measure(UIElementType.ScrollbarBottom, UISkinOrientation.Center).X;
            bottom.Height = (int)Context.Skin.Measure(UIElementType.ScrollbarBottom, UISkinOrientation.Center).Y;
            bottom.Y += Bounds.Height;
            bottom.Y -= bottom.Height;
            BottomButton.Bounds = bottom;

            // Draw the gem
            Rectangle gem = new Rectangle();
            gem.Width = (int)Context.Skin.Measure(UIElementType.ScrollbarGem, UISkinOrientation.Center).X;
            gem.Height = (int)Context.Skin.Measure(UIElementType.ScrollbarGem, UISkinOrientation.Center).Y;
            gem.Y += top.Height;

            int h = Bounds.Height - bottom.Height - top.Height - gem.Height;
            float pos = (float)ScrollbarPosition / (ScrollbarLength > 0 ? ScrollbarLength : 1);
            gem.Y += (int)(h * pos);

            GemButton.Bounds = gem;
        }
    }
}