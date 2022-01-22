using System;
using System.Globalization;
using System.Reflection.Metadata;

namespace IsleofCirca2
{
/*Ian Renaker - Jansen
 *This program is a short text based adventure game filled with many rooms, weapons, enemies, armor, potions, scrolls, and treasure.
 *prompts the user with a simple UI to give the user options so they may interact with the environment.
 * There are many hard bosses and one exit, 
 *
 * 
 */
    public class Body
    {
        static void Main(String[] args){
        bool gameover=false;
        //making equipment
        Weapon GorAxe = new Weapon(25, 0, 1, "Axe", "Gorok's Axe");
        Armor GorHelm = new Armor(13, 0, "Gorok's Helmet");
        Weapon GreatAxe = new Weapon(38, 0, 0, "axe", "greatAxe");
        Armor MinotaurSet = new Armor(10, 7, "Minotaur Set");
        Weapon glassDagger = new Weapon(5, 6, 3, "dagger", "glass dagger");
        Armor MoragTong = new Armor(8, 5, "Morag Tong set");
        Weapon slicer = new Weapon(11, 4, 2, "Sword", "Pupil Slicer");
        Armor boots = new Armor(5, 8, "Bone Crushers");
        Weapon lightStaff = new Weapon(1, 26, 2, "Staff", "Staff of light");
        Armor fireArmor = new Armor(3, 10, "Armor of Fire");
        Armor RomanShield = new Armor(13, 4, "Shield of the Romans");
        //making monsters
        Monster gorok = new Monster(7,110,50, "Gorok the Destroyer", GorAxe, GorHelm, false );
        Hero MC = new Hero(0,100,0,"Hero");
        Monster Minotaur = new Monster(4, 100, 100, "Minotuar",GreatAxe,MinotaurSet,false);
        Monster Donnie = new Monster(5, 75, 15, "Donnie", slicer, boots, true);
        Monster Fufius = new Monster(6, 85, 50, "Fufius the Wizard", lightStaff, fireArmor, true);
        Monster slime = new Monster(6, 30, 15, "slime", 9, 11, true);
        //creating scrolls to be placed in the rooms
        Scrolls scroll1 = new Scrolls("Magic dampening scroll");
        Scrolls scroll2= new Scrolls("Magic dampening scroll");
        //making the rooms and putting stuff in them
        Room[] map = new Room[9];
        map[0] = new Room(25, 1, 2, -1, -1, true, null);
        map[1] = new Room(slime, 0, 4, 3, -1, 0, true,null);
        map[1].addRoomEqipment(glassDagger,null);
        map[2] = new Room(10, 3, 6, 0, -1, false, null);
        map[2].addRoomEqipment(null, MoragTong);
        map[3] = new Room(10, -1, 5, -1, 2, false, scroll1);
        map[4]= new Room(Minotaur,200,-1,-1,-1,1,false,null);
        map[5]= new Room(Donnie,35,8,7,3,-1,true,null);
        map[6]= new Room(Fufius, 60,-1,7,2,-1,false,null);
        map[7]= new Room(gorok,100,5,-1,-1,6,false,scroll2);
        map[7].addRoomEqipment(null,RomanShield);
        map[8]= new Room(0,-1,-1,-1,5,false,null);

        Console.WriteLine("\n-----Hello and welcome to the Isle of Circa-----\n");
        while(gameover==false)
        {//this is the while loop where the majority of the game will take place
            int response;
            bool weaponchoice=true;
            bool armorchoice=true;
            Monster currentmonster= map[MC.getCurrentRoom()].getMonster();
            if (MC.getCurrentRoom() == 8)
            {// the user has beaten the game
                gameover = true;
                Console.WriteLine("\nYou have beaten the game! Congrats on completing the dungeon and we hope you enjoyed it. ");
                Console.WriteLine("Your ending stats were: ");
                MC.printEndResults();
            }
            //check for monster aggression, else give the user the room prompt
            else if (currentmonster != null)
            {
                if (currentmonster.aggressionRoll() && currentmonster.isAlive())
                {//the monster has been angered and now the user must make some choices 
                    Console.WriteLine("You have encountered: " + currentmonster.getname() + " (1) Fight or (2) Retreat\n");
                    Console.WriteLine("The room has a Magic field status of: " + map[MC.getCurrentRoom()].getDamper() +
                                      ", The monster has a magic resistant field status of: " +
                                      currentmonster.getMagicDamper());
                    currentmonster.printEquippedStats();
                    response = Convert.ToInt32(Console.ReadLine());
                    if (response == 1)//asking the user if they want to fight the monster
                    {//asking the user if they would want to switch to their off weapon
                        while (weaponchoice)
                        {
                            // before fighting ask if the user wants to switch weapons
                            Console.WriteLine("\nEnter the 0 to switch to your auxiliary weapon, enter -1 to skip");
                            MC.printStats();
                            response = Convert.ToInt32(Console.ReadLine());
                            if (response == -1)
                            {
                                weaponchoice = false;
                            }
                            else if (response == 0)
                            {
                                //!=null and within scope, change to given weapon
                                MC.swapWeapons();
                                weaponchoice = false;
                            }
                        }

                        while (armorchoice)
                        {//asking if the user would want to switch to their off armor
                            Console.WriteLine("\nEnter 0 to equip your auxiliary armor, enter -1 to skip");
                            MC.printStats();
                            response = Convert.ToInt32(Console.ReadLine());
                            if (response == -1)
                            {
                                armorchoice = false;
                            }
                            else if (response == 0)
                            {
                                MC.swapArmor();
                                armorchoice = false;
                            }
                        }
                        MC.fight(currentmonster, map[MC.getCurrentRoom()]);//initiating the fight with the room monster
                        if (MC.isAlive() == false)
                        {//if the player gets killed print the death screen
                            gameover = true;
                            Console.WriteLine("\nThe hero has died to: "+currentmonster.getname());
                            Console.WriteLine("Search around more to find better equipment to beat them!");
                        }
                        else
                        {//having the monster drop their gear if it is not built into them
                            currentmonster.dropGear(ref map[MC.getCurrentRoom()]);
                        }
                    }
                    else
                    {//retreating if the user does not want to fight the given monster
                        MC.retreat();
                    }
                }
                else
                {//running the ui with the impression there is a monster in the rool
                    runUI(map,MC);
                }
            }
            else
            {//no monster so run the ui without checking on the monster
                runUI2(map,MC);
            }
        } 
        }

        static void runUI(Room[] Map,Hero MC) 
        {
            //This is a runUI that checks for the room monster, the other one does the same thing, minus initiating a fight with the room monster if it is alive
            //loads the room and monster
            bool turn = true;
            int response;
            Monster currentmonster = Map[MC.getCurrentRoom()].getMonster();
            Room currentRoom = Map[MC.getCurrentRoom()];
            Console.WriteLine("\nyou are in room: "+MC.getCurrentRoom()+", If the room monster is still alive you will be attacked when trying to drink a potion, check ground items, or grab the treasure.");
            while (turn)
            {
                //user UI that they will respond to, added try catches to make it more lenient 
                Console.WriteLine("\n1 check current stats, 2 check for magic dampening in the room,");
                Console.WriteLine("3 check for armor on the ground, 4 check for weapons on the ground,");
                Console.WriteLine("5 check for treasure, 6 check for potions in the room");
                Console.WriteLine("7 Drink a potion (Each potion restores 30 health, you have: "+MC.getHealthPoints()+" health points),");
                Console.WriteLine("8 go to another room, 9 to fight the room monster if it is alive.");
                Console.WriteLine("or 10 to search for a scroll ");
                try
                {
                    response = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
                if (response == 1)
                {//printing what the user has, health, potions , equipment
                    MC.printStats();
                }
                else if (response == 2)
                {//gets the magic damper status for the room
                    Console.WriteLine(currentRoom.getDamper());
                }
                else if(response==3)
                {//checking for armor on the ground, if the monster is alive it will have an aggression roll
                    if ((currentmonster.aggressionRoll()) == true &&(currentmonster.isAlive()))
                    {// if the roll returns true, start a fight option
                        Console.WriteLine("\nYou angered "+currentmonster.getname());
                        turn = false;
                    }
                    else
                    {// otherwise print what armor is on the ground to be taken
                        currentRoom.printGroundArmor();
                        Console.WriteLine("\nEnter the index(0-9) of the Armor you would like to swap with your auxiliary armor, hit -1 to exit & -2 to swap your main armor set with your auxiliary armor set \n");
                        try
                        {// a flexible response that will take the index from the user and place that armor in their inventory. if the index is null, nothing happens and if it is out of bounds nothing happens
                            response = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return;
                        }
                        if (response >= 0 && response <= 9)
                        {
                            Armor temp1 = currentRoom.takeArmor(response);
                            if (temp1 != null)
                            {//adding the armor if it exists
                                Armor temp2 = MC.pickAndDropArmor(temp1);
                                currentRoom.addArmor(temp2);
                            }
                        }
                        else if (response == -2)
                        {//swapping armor to make the pickup and drop more appropriate
                            MC.swapArmor();
                        }
                    }
                }
                else if(response==4)
                {//same as above, but with weapons
                    if ((currentmonster.aggressionRoll()) == true &&(currentmonster.isAlive()))
                    {
                        Console.WriteLine("\nYou angered "+currentmonster.getname());
                        turn = false;
                    }
                    else
                    {
                        currentRoom.printGroundWeapons();
                        Console.WriteLine("\nEnter the index (0-9) of the weapon you would like to swap with your auxiliary weapon, hit -1 to exit & -2 to swap your main weapon with your auxiliary weapon \n");
                        try
                        {
                            response = Convert.ToInt32(Console.ReadLine());
                        }
                        catch (Exception e)
                        {
                            Console.WriteLine(e);
                            return;
                        }
                        if (response >= 0 && response <= 9)
                        {
                            Weapon temp1 = currentRoom.takeWeapon(response);
                            if (temp1 != null)
                            {
                                Weapon temp2 = MC.PickAndDropWeapon(temp1);
                                currentRoom.addWeapon(temp2);
                            }
                        }
                        else if (response == -2)
                        {
                            MC.swapWeapons();
                        }
                    }
                }
                else if (response == 5)
                {//checking for treasure in the room, the monster will get aggroed if still alive 
                    if ((currentmonster.aggressionRoll()) == true &&(currentmonster.isAlive()))
                    {
                        Console.WriteLine("\nYou angered "+currentmonster.getname());
                        turn = false;
                    }
                    else
                    {
                        if (currentRoom.checkTreasure())
                        {//adding money if it has been found
                            int money = currentRoom.pickupTreasure();
                            MC.pickupTreasure(money);
                            Console.WriteLine("\nYou found "+money+" gold");
                        }
                        else
                        {// otherwise printing there is nothing left
                            Console.WriteLine("\nThere is no treasure in this room");
                        }
                    }
                }
                else if (response == 6)
                {//pick up an available potion
                    if ((currentmonster.aggressionRoll()) == true &&(currentmonster.isAlive()))
                    {//aggroing monster if it is still alive
                        Console.WriteLine("\nYou angered "+currentmonster.getname());
                        turn = false;
                    }
                    else
                    {//checking for available potions
                        int potion = currentRoom.pickUpPotions();
                        if (potion == 0)
                        {//otherwise printing nothing is to be picked up 
                            Console.WriteLine("\nThere were no potions to pick up");
                        }
                        else
                        {//picking up available potions
                            Console.WriteLine("\nYou found: " + potion + " potion(s)");
                            MC.pickupPotion(potion);
                        }
                    }
                }
                else if (response == 7)
                {//using a potion if the user has one, chance to anger the room monster
                    if ((currentmonster.aggressionRoll()) == true &&(currentmonster.isAlive()))
                    {
                        Console.WriteLine("\nYou angered "+currentmonster.getname());
                        turn = false;
                    }
                    else
                    {
                        MC.usePotion();
                    }
                }
                else if (response == 8)
                {//traversing to available rooms
                    if (currentRoom.getfront() != -1)
                    {
                        Console.WriteLine("Go north to room: " + currentRoom.getfront());
                    }

                    if (currentRoom.getback() != -1)
                    {
                        Console.WriteLine("Go south to room: " + currentRoom.getback());
                    }

                    if (currentRoom.getright() != -1)
                    {
                        Console.WriteLine("Go east to room: " + currentRoom.getright());
                    }
                    if (currentRoom.getleft() != -1)
                    {
                        Console.WriteLine("Go west to room: " + currentRoom.getleft());
                    }
                    while (response!=-1)
                    {
                        //taking a room index and moving the hero to that room.
                        Console.WriteLine("\nEnter the given number to head in the corresponding direction, or hit -1 to go back:");
                        response = Convert.ToInt32(Console.ReadLine());
                        if ((response >= 0 && response <= 8) && Map[response] != null)
                        {
                            MC.traverse(response);
                            turn = false;
                            response = -1;
                        }
                    }
                }
                else if (response == 9)
                {
                    //starting a fight with the room monster
                    currentmonster.aggravate();
                    turn = false;
                }
                else if(response==10) 
                {//checking for a scroll, if it is found, use it
                    if (currentRoom.hasScroll())
                    {
                        Console.WriteLine("\nYou found a scroll");
                        MC.pickUpScroll(currentRoom);
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no scrolls in this room");
                    }
                }
                
            }
        }

        static void runUI2(Room[] Map,Hero MC)
        {//run ui for rooms without monsters
            bool turn = true;
            int response;
            Room currentRoom = Map[MC.getCurrentRoom()];
            Console.WriteLine("\nyou are in room: "+MC.getCurrentRoom());
            while (turn)
            {
                Console.WriteLine("1 check current stats, 2 check for magic dampening in the room,");
                Console.WriteLine("3 check for armor on the ground, 4 check for weapons on the ground,");
                Console.WriteLine("5 check for treasure, 6 check for potions in the room");
                Console.WriteLine("7 Drink a potion (Each potion restores 30 health, you have: "+MC.getHealthPoints()+" health points),");
                Console.WriteLine("8 go to another room, or 9 to search for a scroll");
                try
                {
                    response = Convert.ToInt32(Console.ReadLine());
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return;
                }
                if (response == 1)
                {
                    MC.printStats();
                }
                else if (response == 2)
                {
                    Console.WriteLine(currentRoom.getDamper());
                }
                else if(response==3)
                {
                    currentRoom.printGroundArmor();
                    Console.WriteLine("\nEnter the index(0-9) of the Armor you would like to swap with your auxiliary armor, hit -1 to exit & -2 to swap your main armor set with your auxiliary armor set \n");
                    try
                    {
                        response = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return;
                    }
                    if (response >= 0 && response <= 9)
                    {
                        Armor temp1 = currentRoom.takeArmor(response);
                        if (temp1 != null)
                        {
                            Armor temp2 = MC.pickAndDropArmor(temp1);
                            currentRoom.addArmor(temp2);
                        }
                    }
                    else if (response == -2)
                    {
                        MC.swapArmor();
                    }
                }
                else if(response==4)
                {
                    currentRoom.printGroundWeapons();
                    Console.WriteLine("\nEnter the index (0-9) of the weapon you would like to swap with your auxiliary weapon, hit -1 to exit & -2 to swap your main weapon with your auxiliary weapon \n");
                    try
                    {
                        response = Convert.ToInt32(Console.ReadLine());
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                        return;
                    }
                    if (response >= 0 && response <= 9)
                    {
                        Weapon temp1 = currentRoom.takeWeapon(response);
                        if (temp1 != null)
                        {
                            Weapon temp2 = MC.PickAndDropWeapon(temp1);
                            currentRoom.addWeapon(temp2);
                        }
                    }
                    else if (response == -2)
                    {
                        MC.swapWeapons();
                    }
                }
                else if (response == 5)
                {
                    if (currentRoom.checkTreasure())
                    {
                        int money = currentRoom.pickupTreasure();
                        MC.pickupTreasure(money);
                        Console.WriteLine("\nYou found "+money+" gold\n");
                    }
                    else
                    {
                        Console.WriteLine("\nThere is no treasure in this room\n");
                    }
                }
                else if (response == 6)
                {//pick up an available potion
                    int potion = currentRoom.pickUpPotions();
                    if (potion == 0)
                    {
                        Console.WriteLine("\nThere were no potions to pick up\n");
                    }
                    else
                    {
                        Console.WriteLine("\nYou found: "+potion+" potion(s)\n");
                        MC.pickupPotion(potion);
                    }
                }
                else if (response == 7)
                {
                    MC.usePotion();
                }
                else if (response == 8)
                {
                    bool traversing = true;
                    Console.WriteLine("___________________________");
                    if (currentRoom.getfront() != -1)
                    {
                        Console.WriteLine("Go north to room: " + currentRoom.getfront());
                    }

                    if (currentRoom.getback() != -1)
                    {
                        Console.WriteLine("Go south to room: " + currentRoom.getback());
                    }

                    if (currentRoom.getright() != -1)
                    {
                        Console.WriteLine("Go east to room: " + currentRoom.getright());
                    }
                    if (currentRoom.getleft() != -1)
                    {
                        Console.WriteLine("Go west to room: " + currentRoom.getleft());
                    }
                    while (response!=-1)
                    {
                        Console.WriteLine("\n Enter the given number to head in the corresponding direction, or hit -1 to go back:");
                        response = Convert.ToInt32(Console.ReadLine());
                        if ((response >= 0 && response <= 8) )
                        {
                            MC.traverse(response);
                            turn = false;
                            response = -1;
                        }
                    }
                }
                else if (response == 9)
                {
                    if (currentRoom.hasScroll())
                    {
                        Console.WriteLine("\nYou found a scroll");
                        MC.pickUpScroll(currentRoom);
                    }
                    else
                    {
                        Console.WriteLine("\nThere are no scrolls in this room\n");
                    }
                }
            }
        }
    }
}
