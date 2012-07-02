﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace CTC
{
    class ItemButton : UIButton
    {
        protected GameRenderer Renderer;
        public ClientItem Item;

        public ItemButton(UIView Parent, GameRenderer Renderer, ClientItem Item)
            : base(Parent)
        {
            this.Item = Item;
            this.Renderer = Renderer;
            this.Padding = new Margin
            {
                Top = -1,
                Right = 1,
                Bottom = 1,
                Left = -1
            };

            Bounds.Width = 34;
            Bounds.Height = 34;

            ElementType = UIElementType.InventorySlot;
        }

        public override bool Highlighted
        {
            get
            {
                return base.Highlighted;
            }
            set
            {
                base.Highlighted = value;

                // Reset 'our' element type
                ElementType = UIElementType.InventorySlot;
            }
        }

        public override bool MouseLeftClick(Microsoft.Xna.Framework.Input.MouseState mouse)
        {
            return base.MouseLeftClick(mouse);
        }

        protected override void DrawContent(SpriteBatch Batch)
        {
            Renderer.DrawInventorySlot(Batch, ScreenBounds);

            if (Item != null)
                Renderer.DrawInventoryItem(Batch, Item, ScreenClientBounds);
        }
    }

    /// <summary>
    /// Renders an inventory slot.
    /// This is different from the above in that it reads the Viewport to get
    /// it's data, and it also draws a background image if there is no item
    /// in that slot.
    /// </summary>
    class InventoryItemButton : ItemButton
    {
        protected ClientViewport Viewport;
        protected InventorySlot Slot = InventorySlot.None;

        public InventoryItemButton(UIView Parent, ClientViewport Viewport, InventorySlot Slot)
            : base(Parent, null, null)
        {
            this.Viewport = Viewport;
            this.Slot = Slot;
        }

        public void OnNewState(ClientState NewState)
        {
            Viewport = NewState.Viewport;
            this.Renderer = new GameRenderer(Context, Viewport.GameData);
        }

        protected override void DrawContent(SpriteBatch Batch)
        {
            Item = Viewport.Inventory[(int)Slot];

            base.DrawContent(Batch);

            if (Item == null)
            {
                // TODO: Draw the background image
            }
        }
    }
}