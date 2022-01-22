using System;

namespace IsleofCirca2
{
    public class Weapon
    {
        private string name;
        private int numAttacks;
        private int magicNumAttacks;
        private int damage;
        private string type;
        private int magicDamage;
        public Weapon(int dam,  int magdam,int magicSwings, string type, string name)
        {
            //setting corresponding values
            magicDamage = magdam;
            magicNumAttacks = magicSwings;
            this.name = name;
            setType(type);
            damage = dam;
        }
        public Weapon(Weapon w)
        {//copy constructor
            magicDamage = w.getMagicDamage();
            magicNumAttacks = w.getMagicAttacks();
            name = w.getName();
            setType(w.getType());
            damage = w.getWeaponDamage(true);
        }

        public void setType(string t)
        {
            //determining the number of swings based on the type
            if (t.ToLower().Equals("sword"))
            {
                type = "sword";
                numAttacks = 3;
            }
            else if (t.ToLower().Equals("dagger"))
            {
                type = "dagger";
                numAttacks = 5;
            }
            else if (t.ToLower().Equals("wand"))
            {
                type = "wand";
                numAttacks = 4;
            }
            else if (t.ToLower().Equals("axe"))
            {
                type = "axe";
                numAttacks = 1;
            }
            else if (t.ToLower().Equals("staff"))
            {
                type = "staff";
                numAttacks = 1;
            }
            else if (t.ToLower().Equals("claws"))
            {
                type = "claws";
                numAttacks = 4;
            }
            else
            {
                Console.Write("Invalid weapon type");
            }
        }

        public void setMagic(int m)
        {//setting the magic
            magicDamage = m;
        }

        public int getNumAttacks()
        {//getting the base number of attakcs
            return numAttacks;
        }

        public string getType()
        {//getting the type
            return type;
        }

        public int getWeaponDamage(bool dampen)
        {
            if (dampen)
            {
                return damage;
            }
            else
            {
                return damage+ magicDamage;
            }
        }

        public int getSwings(bool dampen)
        {
            if (dampen)
            {
                return numAttacks;
            }
            else
            {
                return numAttacks+magicNumAttacks;
            }
        }

        public int getMagicAttacks()
        {
            return magicNumAttacks;
        }

        public int getMagicDamage()
        {
            return magicDamage;
        }

        public string getName()
        {
            return name;
        }

        public int getDamage(bool magicDamp)
        {
            Random r = new Random();
            int max;
            if (magicDamp)//If magic is dampened return the value without magic boost
            {max = damage;}
            else//Else apply magic
            {max = damage + magicDamage;}
            return r.Next(1, max);
        }

        public override string ToString()
        {
            return name+"| type: " +type +"| base damage: "+damage+", magical damage: "+magicDamage+"| basic swings: "+numAttacks+", magic swings: "+magicNumAttacks;
        }
    }
}