using System;

namespace IsleofCirca2
{
    public class Room
    {
        //a room class which will hold a number of 
        protected Monster monster;
        protected int treasure;
        private int front;
        private int back;
        private int left;
        private int right;
        protected bool MagicDampening;
        private Weapon[] groundWeapons = new Weapon[10];
        private Armor[] groundArmors = new Armor[10];
        private Scrolls roomScroll=null;
        private int roompotion;//add a random amount(0-2) per room
        private Random rand = new Random();

        public Room(Monster mon,int givenloot,int givenfront,int givenright,int givenLeft,int givenback,bool damp,Scrolls s)
        {//constructor for a room with a moonster
            if (mon.getPrimalStatus())
            {//if its primal, make a new primal monster with its stats
                monster = new Monster(mon.getCurrentRoom(),mon.getHealthPoints(),mon.getAggropoints(),mon.getname(),mon.getClawdamage(),mon.getSkinProtection(),mon.getMagicDamper());
                
            }
            else
            {//else make a non primal monster with a weapon
                monster = new Monster(mon.getCurrentRoom(),mon.getHealthPoints(),mon.getAggropoints(),mon.getname(),mon.getWeapon(),mon.getArmor(),mon.getMagicDamper());
            }

            if (s != null)
            {
                roomScroll = new Scrolls(s);
            }
            treasure = givenloot;
            front = givenfront;
            right = givenright;
            left = givenLeft;
            back = givenback;
            MagicDampening = damp;
            for (int i = 0; i < 10; i++)
            {//filling with null values
                groundWeapons[i] = null;
                groundArmors[i] = null;
            }
            roompotion = rand.Next(3);
        }

        public Room(int givenloot,int givenfront,int givenright,int givenLeft,int givenback,bool damp,Scrolls s)
        {// a case for if there is no monster given
            if (s != null)
            {
                roomScroll = new Scrolls(s);
            }
            monster = null;
            treasure = givenloot;
            front = givenfront;
            right = givenright;
            left = givenLeft;
            back = givenback;
            MagicDampening = damp;
            for (int i = 0; i < 10; i++)
            {
                groundWeapons[i] = null;
                groundArmors[i] = null;
            }
            roompotion = rand.Next(3);//rolling to see if the room will have potions 0-2
        }
        //the below two prints will print the values of the held weapons and armors
        public void printGroundArmor()
        {
            Console.WriteLine("____________________________________");
            for(int i=0;i<groundArmors.GetLength(0);i++)
            {
                if (groundArmors[i] != null)
                {
                    Console.WriteLine("Armor index: " + i + " " + groundArmors[i].ToString()); 
                    //print the name of the item lying on the ground
                }
            }
        }
        public void printGroundWeapons()
        {
            Console.WriteLine("____________________________________");
            for(int i=0;i<groundWeapons.GetLength(0);i++)
            {
                if(groundWeapons[i]!=null){
                Console.WriteLine("Weapon index: "+i+" "+groundWeapons[i].ToString());
                }
                
                //print the name of the item lying on the ground
            }
        }

        public Monster getMonster()
        {//monster getter
            return monster;
        }
        public Scrolls PickupRoomScroll()
        {//picking up a room scroll
            
            Scrolls temp = roomScroll;
            roomScroll = null;
            return temp;
        }
        // a lot of getters 
        public bool getDamper()
        {
            return MagicDampening;
        }
        public int getleft()
        {
            return left;
        }
        public int getright()
        {
            return right;
        }
        public int getfront()
        {
            return front;
        }
        public int getback()
        {
            return back;
        }
        //picking up potions and money, then setting their spots to 0
        public int pickUpPotions()
        {
            int temp = roompotion;
            roompotion = 0;
            return temp;
        }

        public int pickupTreasure()
        {
            int temp = treasure;
            treasure = 0;
            return temp;
        }

        public bool checkTreasure()
        {//checking to see if there is treasure
            return treasure!=0;//if it is empty it will return false; otherwise return true.
        }

        public bool hasScroll()
        {//checking for a scroll in the room, otherwise return false
            if (roomScroll == null)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        public void addRoomEqipment(Weapon givenWeapon, Armor givenArmor)
        {//adding equipment to the room, if null nothing is added 
            for (int p = 0; p < 10; p++)
            {
                if (groundArmors[p] == null && givenArmor!=null)
                {
                    groundArmors[p] = new Armor(givenArmor);
                    p = 10;
                }
            }
            for (int p = 0; p < 10; p++)
            {
                if (groundWeapons[p] == null && givenWeapon!=null)
                {
                    groundWeapons[p] = new Weapon(givenWeapon);
                    p = 10;
                }
            }
        }

        public void addWeapon(Weapon w)
        {//adding a weapon to the first open spot as long as it is not null
            if (w != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    //placing the weapon in the first open spot
                    if (groundArmors[i] == null)
                    {
                        groundWeapons[i] = new Weapon(w);
                        i = 10;
                    }
                }
            }
        }

        public void addArmor(Armor a)
        {// adding armor to the first open spot
            if (a != null)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (groundArmors[i] == null)
                    {
                        groundArmors[i] = new Armor(a);
                        i = 10;
                    }
                }
            }
        }
        //taking armor or weapons off the ground based on a given index, otherwise returning null
        public Armor takeArmor(int index)
        {
            if (groundArmors[index] != null)
            {
                Armor temp = groundArmors[index];
                groundArmors[index] = null;
                return temp;
            }
            else
            {
                return null;
            }
        }

        public Weapon takeWeapon(int index)
        {
            if (groundWeapons[index] != null)
            {
                Weapon temp = groundWeapons[index];
                groundWeapons[index] = null;
                return temp;
            }
            else
            {
                return null;
            }
        }
    }
}