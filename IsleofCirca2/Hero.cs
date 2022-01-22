using System;

namespace IsleofCirca2
{
    public class Hero:Creature
    {
        private Armor heldArmor;//aux
        private Weapon heldWeapon;//aux
        private int heldPotions = 0;
        private Scrolls heldScroll= null;
        private Random rand = new Random();
        private int lastRoom;
        private int treasure;
        //Armor[0]= new Armor();
        //Weapon[0]= new Weapon();
        public Hero(int setroom,int givenhealth, int givenaggro,String monname):base(setroom,givenhealth,givenaggro,monname)
        {
            heldWeapon = null;
            heldArmor = null;
            equippedArmor = new Armor(8,0,"Leather armor");//making starting armor
            equippedWeapon = new Weapon(8,0,0,"sword","Iron sword");//make a starting weapon
            lastRoom = currentroom;
            currentroom = setroom;
        }


        public void printStats()
        {//printing hero stats and all held equipment for the user to see
            Console.WriteLine("_________________________________");
            Console.WriteLine("Main: "+equippedArmor.ToString()+"\nMain: "+equippedWeapon.ToString());
            if (heldArmor != null)
            {
                Console.WriteLine("Auxiliary: " + heldArmor.ToString());
            }
            if (heldWeapon != null)
            {
                Console.WriteLine("Auxiliary: " + heldWeapon.ToString());
            }
            Console.WriteLine("\nHealth "+healthpoints+"| number of potions: "+heldPotions+"| Magic resitant: "+magicDamper+"\n");
        }

        public void retreat()
        {//returning to the last room
            currentroom = lastRoom;
        }

        public void traverse(int index)
        {//storing last room for a retreat
            lastRoom = currentroom;
            currentroom = index;
        }
        //swapping equipped armors/weapons with offhand armors/weapons as long as they are not null
        public void swapWeapons()
        {
            if (heldWeapon != null)
            {
                Weapon temp = equippedWeapon;
                equippedWeapon = heldWeapon;
                heldWeapon = temp;
            }
        }
        public void swapArmor()
        {
            if(heldArmor!=null){
                Armor temp = equippedArmor;
                equippedArmor = heldArmor;
                heldArmor = temp;
            }
        }
        //pick up and drop armor/weapons will pickup the given equipment, and drop their offhand equipment
        public Armor pickAndDropArmor(Armor a)
        {
            Armor temp = heldArmor;
            heldArmor = new Armor(a);
            return temp;
        }

        public Weapon PickAndDropWeapon(Weapon w)
        {
            Weapon temp = heldWeapon;
            heldWeapon = new Weapon(w);
            return temp;
        }

        public void pickupTreasure(int money)
        {//adding treasure to the backpack
            treasure = treasure + money;
        }

        public void pickUpScroll(Room r)
        {//picking up scroll and hurting the user if it is cursed, otherwise setting the hero resistant to true.
            //if the scroll in the room is cursed then strike the hero for 1/3 of their health
            if (r.PickupRoomScroll().isCursed())
            {
                strike(getHealthPoints() / 3);
                Console.WriteLine("The scroll was cursed and blew up on the hero.\n");
            }
            else
            {
                magicDamper = true;
                Console.WriteLine("The knowledge of the scroll has made the hero resistant to magic\n");
            }
        }

        public void usePotion()
        {//healing the hero with a potion if available
            if (heldPotions > 0)
            {
                healthpoints = healthpoints + 30;
                if (healthpoints > 100)
                {//setting the healthpoints to 100 if it is more than 100 after healing
                    healthpoints = 100;
                }
                //print current health here
                Console.WriteLine("\nyour health is now "+healthpoints+" points\n");
            }
            else
            {
                Console.WriteLine("\nYou have no potions\n");
                //printing that the user cannot heal
            }
        }

        public void pickupPotion(int i)
        {//adding potions to the heros backpack
            heldPotions = heldPotions + i;
        }

        public void printEndResults()
        {//printing the end results
            Console.WriteLine("\n____________________________________________________________________________________________\n");
            Console.WriteLine("Your ending gear was( Weapon: "+equippedWeapon.ToString()+"; Armor: "+equippedArmor.ToString());
            Console.WriteLine("Your ending gear was( Weapon: "+heldWeapon.ToString()+"; Armor: "+heldArmor.ToString());
            Console.WriteLine("Your ending backpack had: "+heldPotions+" potions, "+treasure+" gold, and your magic resistance was: "+magicDamper);
            Console.WriteLine("You had: "+healthpoints+" health points left");
            
        }
    }
}