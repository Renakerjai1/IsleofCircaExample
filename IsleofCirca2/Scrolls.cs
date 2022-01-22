using System;

namespace IsleofCirca2
{
    public class Scrolls
    {
        private String scrollname;
        private bool cursed;
        private Random rand = new Random();
        public Scrolls( String givenname)
        {
            cursed = false;
            int roll;
            scrollname = givenname;
            
            /*roll to see if a scroll will backfire on the user causing damage,striking them for 1/3 of their health;
             upon activating a scroll, if it blows up print" the scroll was cursed and damaged the user"
             */
            roll = rand.Next(1, 6);
            if (roll == 5)
                cursed = true;
        }

        public Scrolls(Scrolls s)
        {//copy constructor
            cursed = s.isCursed();
            scrollname = s.getScrollName();
        }
        //getter and checker
        public bool isCursed()
        {
            return cursed;
        }

        public string getScrollName()
        {
            return scrollname;
        }

    }
}