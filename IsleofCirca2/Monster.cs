using System;

namespace IsleofCirca2
{
    public class Monster:Creature
    {
        private Random rand = new Random();
        private int claws = 0;
        private int skinguard = 0;
        private bool primalenemy;//if true, then upon death nothing will be dropped
        public Monster(int setroom, int givenhealth,int givenaggro, String monname,Weapon givenWeapon,Armor givenArmor,bool magicResistant ):base(setroom,givenhealth,givenaggro,monname)
        {//enemy will drop weapons/ armor on death
            primalenemy = false;
            magicDamper = magicResistant;
            equippedArmor = new Armor(givenArmor.getArmor(true), givenArmor.getMagicRating(), givenArmor.getName());
            equippedWeapon = new Weapon(givenWeapon.getWeaponDamage(true), givenWeapon.getMagicDamage(),
                givenWeapon.getMagicAttacks(), givenWeapon.getType(), givenWeapon.getName());

        }
        public Monster(int setroom, int givenhealth,int givenaggro, String monname,int clawDamage, int skinProtection, bool magicResistant ):base(setroom,givenhealth,givenaggro,monname)
        {//enemy will not drop anything due to being primal
            equippedWeapon = new Weapon(clawDamage, 0, 0, "claws", "claws");
            equippedArmor = new Armor(skinProtection, 0, "tough skin");
            primalenemy = true;
            claws = clawDamage;
            skinguard = skinProtection;

        }
        //getters for many different variables/objects

        public Weapon getWeapon()
        {
            return equippedWeapon;
        }

        public int getClawdamage()
        {
            return claws;
        }

        public int getSkinProtection()
        {
            return skinguard;
        }
        public bool getPrimalStatus()
        {
            return primalenemy;
        }

        public Armor getArmor()
        {
            return equippedArmor;
        }

        public bool aggressionRoll()
        {
            //roll for aggression when 
            if (isAlive())
            {
                //checking to make sure they are alive
                int aggroroll = rand.Next(1,101);
                //Console.WriteLine(aggroroll+" "+aggressionpoints+" "+ aggressionstatus+" "+healthpoints+" "+name);
                if (aggressionpoints == 100 || aggressionstatus == true)
                {
                    aggressionstatus = true;
                } //if 100, start swinging
                else if (aggroroll <= aggressionpoints)
                {
                    
                    //if the roll falls within the range, set aggro to true
                    aggressionstatus = true;
                }
                else
                {
                    //else return false
                    aggressionstatus = false;
                }

                return aggressionstatus;
            }
            else
            {
                return false;
            }
        }

        public void dropGear(ref Room r)
        {
            if (primalenemy==false)
            {// if the enemy is not using claws and tough skin, drop the gear it was using
                r.addRoomEqipment(equippedWeapon,equippedArmor);
                equippedArmor = null;
                equippedWeapon = null;
                //emptying their inventory now that it has been passed and they are dead
            }
            //else set the claws and skinguard to 0 and set nothing
            claws = 0;
            skinguard = 0;
        }

        public void aggravate()
        {//will make the monster hostile so the hero can fight them. 
            aggressionpoints = 100;
            aggressionstatus = true;
        }
    }
}