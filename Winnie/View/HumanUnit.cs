using System;

namespace MGUI
{
    public class HumanUnit : Unit
    {
        protected override void SetBody()
        {
            string[] skins = { "MaleMixedRace", "FemaleMixedRace" };
            body = skins[gender];
        }

        protected override void SetPant()
        {
            string[] pants = { "RedPant", "YellowPant" };
            pant = pants[rnd.Next(0, 2)];
        }

        protected override void SetShirt()
        {
            string[] shirts = { "RedShirt1", "RedShirt2", "RedShirt3" };
            shirt = shirts[rnd.Next(0, 3)];
        }

        protected override void SetHair()
        {
            string[] hairs = { "BrownHair1", "BrownHair2" };
            hair = hairs[gender];
        }

        protected override void SetWeapon()
        {
            weapon = "Stick1";
        }
    }
}

