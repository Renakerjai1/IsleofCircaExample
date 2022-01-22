using System;

namespace IsleofCirca2
{
    public class Creature
    {
        protected int currentroom;
        protected int healthpoints;
        protected bool aggressionstatus = false;//base case so that a fight will not initiate unless changed
        protected int aggressionpoints;
        private Random rand = new Random();
        protected String name;
        protected bool magicDamper;
        protected Armor equippedArmor;
        protected Weapon equippedWeapon;
        public Creature(int setroom, int givenhealth,int givenaggro, String monname)
        {//constructor to take, roon, health, aggro, and name
            currentroom = setroom;
            healthpoints = givenhealth;
            aggressionpoints = givenaggro;
            name = monname;
        }
        
        public bool isAlive()
        {//checking to see if the creature is alive
            if (healthpoints > 0)
            {
                return true;
            }
            else
            {
                aggressionpoints = 0;
                aggressionstatus = false;
                return false;
            }
        }
        //getters
        public int getHealthPoints()
        {
            return healthpoints;
        }

        public int getCurrentRoom()
        {
            return currentroom;
        }

        public int getAggropoints()
        {
            return aggressionpoints;
        }

        public String getname()
        {
            return name;
        }

        public bool getMagicDamper()
        {
            return magicDamper;
        }
        // the fight method which will find if the given creature will be able to use magic and base its attacks off of that
        public void fight(Creature c,Room r)
        {//fighting
            int hitchance=0;
            bool localdamper;
            if (r.getDamper() || c.getMagicDamper())
            {
                localdamper = true;
            }
            else
            {
                localdamper = false;
            }
            while (isAlive() && c.isAlive())
            {// if they are alive, start fighting
                for (int i = 0; i < equippedWeapon.getSwings(localdamper); i++)
                {// swing for the allotted amount
                    hitchance = rand.Next(1, 21); //1-20 roll to hit the other creature
                    if (hitchance > c.getArmorpoints(localdamper))
                    {// if the hitchance is higher than the creature's armor, strike them for a rolled damage
                        c.strike(equippedWeapon.getDamage(localdamper));
                    }
                }
                if (c.isAlive())
                {// as long as the other creature is alive, keep fighting 
                    c.fight(this,r);
                }
            }
        }
        public int getArmorpoints(bool d)
        {//getter for armor points
            return equippedArmor.getArmor(d);
        }

        public void strike(int damage)
        {//striking the creature 
            healthpoints = healthpoints - damage;
            Console.WriteLine(name+" has "+healthpoints+" health points remaining");
        }
        
        public void printEquippedStats()
        {//printing their given armor and weapons. 
            Console.WriteLine("The Creature has: "+equippedArmor.ToString());
            Console.WriteLine("using : "+equippedWeapon.ToString());
        }
        
/*
 * notes:
 * 
 *
 * 
 * 
 */
    }
}