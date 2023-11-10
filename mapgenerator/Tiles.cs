using System.Collections;
using System.Collections.Specialized;

namespace mapgenerator;

public static class Tiles
{


    public struct rules
    {
        public Wavecell top;
        public Wavecell right;
        public Wavecell left;
        public Wavecell bottom;

        public rules(Wavecell top, Wavecell right, Wavecell left, Wavecell bottom)
        {
            this.left = left;
            this.right = right;
            this.top = top;
            this.bottom = bottom;
        }

    }

    private static rules Solid = new rules(
        Wavecell.Solid_B_T | Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_LT,
        Wavecell.Solid_B_R | Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_RB,
        Wavecell.Solid_B_L | Wavecell.Solid | Wavecell.Solid_E_LT | Wavecell.Solid_E_LB,
        Wavecell.Solid_B_B | Wavecell.Solid | Wavecell.Solid_E_RB | Wavecell.Solid_E_LB
    );

    private static rules Solid_B_R = new rules(
        Wavecell.Solid_B_R | Wavecell.Solid_E_RB | Wavecell.Solid_C_RT,
        Wavecell.Water,
        Wavecell.Solid_B_L | Wavecell.Solid | Wavecell.Solid_E_LT | Wavecell.Solid_E_LB,
        Wavecell.Solid_B_R | Wavecell.Solid_E_RT | Wavecell.Solid_C_RB
    );

    private static rules Solid_B_T = new rules(
        Wavecell.Water,
        Wavecell.Solid_B_T | Wavecell.Solid_E_LT | Wavecell.Solid_C_RT,
        Wavecell.Solid_B_T | Wavecell.Solid_E_RT | Wavecell.Solid_C_LT,
        Wavecell.Solid_B_B | Wavecell.Solid | Wavecell.Solid_E_LB | Wavecell.Solid_E_RB
    );

    private static rules Solid_B_L = new rules(
        Wavecell.Solid_B_L | Wavecell.Solid_C_LT | Wavecell.Solid_E_LB,
        Wavecell.Solid_B_R | Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_RB,
        Wavecell.Water,
        Wavecell.Solid_B_L | Wavecell.Solid_C_LB | Wavecell.Solid_E_LT
    );

    private static rules Solid_B_B = new rules(
        Wavecell.Solid | Wavecell.Solid_B_T | Wavecell.Solid_E_LT | Wavecell.Solid_E_RT,
        Wavecell.Solid_B_B | Wavecell.Solid_E_LB | Wavecell.Solid_C_RB,
        Wavecell.Solid_E_RB | Wavecell.Solid_C_LB | Wavecell.Solid_B_B,
        Wavecell.Water
    );

    private static rules Solid_C_LT = new rules(
        Wavecell.Water,
        Wavecell.Solid_C_RT | Wavecell.Solid_E_LT | Wavecell.Solid_B_T,
        Wavecell.Water,
        Wavecell.Solid_C_LB | Wavecell.Solid_E_LT | Wavecell.Solid_B_L
    );

    private static rules Solid_C_RT = new rules(
        Wavecell.Water,
        Wavecell.Water,
        Wavecell.Solid_C_LT | Wavecell.Solid_E_RT | Wavecell.Solid_B_T,
        Wavecell.Solid_C_RB | Wavecell.Solid_E_RT | Wavecell.Solid_B_R
    );


    private static rules Solid_C_LB = new rules(
        Wavecell.Solid_C_RT | Wavecell.Solid_E_RB | Wavecell.Solid_B_R,
        Wavecell.Solid_C_LB | Wavecell.Solid_E_RB | Wavecell.Solid_B_B,
        Wavecell.Water,
        Wavecell.Water

    );

    private static rules Solid_C_RB = new rules(
        Wavecell.Solid_C_RT | Wavecell.Solid_E_RB | Wavecell.Solid_B_R,
        Wavecell.Water, 
        Wavecell.Solid_C_LB | Wavecell.Solid_E_RB | Wavecell.Solid_B_B,
        Wavecell.Water
    );

    private static rules Solid_E_LT = new rules(
        Wavecell.Solid_C_LT | Wavecell.Solid_B_L,
        Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_RB | Wavecell.Solid_B_R,
        Wavecell.Solid_C_LT | Wavecell.Solid_B_T,
        Wavecell.Solid | Wavecell.Solid_E_RB | Wavecell.Solid_E_LB | Wavecell.Solid_B_B
    );

    private static rules Solid_E_LB = new rules(
        Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_LT | Wavecell.Solid_B_T,
        Wavecell.Solid | Wavecell.Solid_E_RT | Wavecell.Solid_E_RB | Wavecell.Solid_B_R,
        Wavecell.Solid_C_LB | Wavecell.Solid_B_B,
        Wavecell.Solid_C_LB | Wavecell.Solid_B_L
    );

    private static rules Solid_E_RT = new rules(
        Wavecell.Solid_C_RT | Wavecell.Solid_B_R,
        Wavecell.Solid_C_RT | Wavecell.Solid_B_T,
        Wavecell.Solid | Wavecell.Solid_E_LB | Wavecell.Solid_E_LT | Wavecell.Solid_B_L,
        Wavecell.Solid | Wavecell.Solid_E_LB | Wavecell.Solid_E_RB | Wavecell.Solid_B_B
    );

    private static rules Solid_E_RB = new rules(
        Wavecell.Solid | Wavecell.Solid_E_LT | Wavecell.Solid_E_RT | Wavecell.Solid_B_T,
        Wavecell.Solid_C_RB | Wavecell.Solid_B_B,
        Wavecell.Solid | Wavecell.Solid_E_LB | Wavecell.Solid_E_LT | Wavecell.Solid_B_L,
        Wavecell.Solid_C_RB | Wavecell.Solid_B_R
    );

    private static rules Water = new rules(
        Wavecell.Solid_B_B | Wavecell.Solid_C_RB | Wavecell.Solid_C_LB |Wavecell.Water, 
        Wavecell.Solid_B_L | Wavecell.Solid_C_LT | Wavecell.Solid_C_LB |Wavecell.Water,
        Wavecell.Solid_B_R | Wavecell.Solid_C_RB | Wavecell.Solid_C_RT |Wavecell.Water,
        Wavecell.Solid_B_T | Wavecell.Solid_C_LT | Wavecell.Solid_C_RT |Wavecell.Water
    );


    


    public static Wavecell getneigbours(char dir, Wavecell type)
    {

        Wavecell ret = Wavecell.None;

        if ((type & Wavecell.AllTerain) == Wavecell.None)
        {
            return Wavecell.None;
        }

        foreach (Wavecell types in Enum.GetValues(typeof(Wavecell)))
        {
            if ((type & types) != Wavecell.None && types != Wavecell.AllTerain)
            {
                switch ((types & Wavecell.AllTerain))
                {
                    case Wavecell.Solid:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid.left;
                                break;
                            case 'R':
                                ret = ret | Solid.right;
                                break;
                            case 'T':
                                ret = ret | Solid.top;
                                break;
                            case 'B':
                                ret = ret | Solid.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_B_R:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_B_R.left;
                                break;
                            case 'R':
                                ret = ret | Solid_B_R.right;
                                break;
                            case 'T':
                                ret = ret | Solid_B_R.top;
                                break;
                            case 'B':
                                ret = ret | Solid_B_R.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_B_T:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_B_T.left;
                                break;
                            case 'R':
                                ret = ret | Solid_B_T.right;
                                break;
                            case 'T':
                                ret = ret | Solid_B_T.top;
                                break;
                            case 'B':
                                ret = ret | Solid_B_T.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_B_L:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_B_L.left;
                                break;
                            case 'R':
                                ret = ret | Solid_B_L.right;
                                break;
                            case 'T':
                                ret = ret | Solid_B_L.top;
                                break;
                            case 'B':
                                ret = ret | Solid_B_L.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_B_B:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_B_B.left;
                                break;
                            case 'R':
                                ret = ret | Solid_B_B.right;
                                break;
                            case 'T':
                                ret = ret | Solid_B_B.top;
                                break;
                            case 'B':
                                ret = ret | Solid_B_B.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_C_LT:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_C_LT.left;
                                break;
                            case 'R':
                                ret = ret | Solid_C_LT.right;
                                break;
                            case 'T':
                                ret = ret | Solid_C_LT.top;
                                break;
                            case 'B':
                                ret = ret | Solid_C_LT.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_C_RT:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_C_RT.left;
                                break;
                            case 'R':
                                ret = ret | Solid_C_RT.right;
                                break;
                            case 'T':
                                ret = ret | Solid_C_RT.top;
                                break;
                            case 'B':
                                ret = ret | Solid_C_RT.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_C_LB:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_C_LB.left;
                                break;
                            case 'R':
                                ret = ret | Solid_C_LB.right;
                                break;
                            case 'T':
                                ret = ret | Solid_C_LB.top;
                                break;
                            case 'B':
                                ret = ret | Solid_C_LB.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_C_RB:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_C_RB.left;
                                break;
                            case 'R':
                                ret = ret | Solid_C_RB.right;
                                break;
                            case 'T':
                                ret = ret | Solid_C_RB.top;
                                break;
                            case 'B':
                                ret = ret | Solid_C_RB.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_E_LT:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_E_LT.left;
                                break;
                            case 'R':
                                ret = ret | Solid_E_LT.right;
                                break;
                            case 'T':
                                ret = ret | Solid_E_LT.top;
                                break;
                            case 'B':
                                ret = ret | Solid_E_LT.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_E_LB:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_E_LB.left;
                                break;
                            case 'R':
                                ret = ret | Solid_E_LB.right;
                                break;
                            case 'T':
                                ret = ret | Solid_E_LB.top;
                                break;
                            case 'B':
                                ret = ret | Solid_E_LB.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_E_RT:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_E_RT.left;
                                break;
                            case 'R':
                                ret = ret | Solid_E_RT.right;
                                break;
                            case 'T':
                                ret = ret | Solid_E_RT.top;
                                break;
                            case 'B':
                                ret = ret | Solid_E_RT.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Solid_E_RB:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Solid_E_RB.left;
                                break;
                            case 'R':
                                ret = ret | Solid_E_RB.right;
                                break;
                            case 'T':
                                ret = ret | Solid_E_RB.top;
                                break;
                            case 'B':
                                ret = ret | Solid_E_RB.bottom;
                                break;
                        }

                        break;
                    case Wavecell.Water:
                        switch (dir)
                        {
                            case 'L':
                                ret = ret | Water.left;
                                break;
                            case 'R':
                                ret = ret | Water.right;
                                break;
                            case 'T':
                                ret = ret | Water.top;
                                break;
                            case 'B':
                                ret = ret | Water.bottom;
                                break;

                        }

                        break;

                }
            }


        }

        return ret;
    }
}