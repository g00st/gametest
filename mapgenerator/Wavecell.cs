using System.Collections;



/// <summary>
///   -------------------------------------------------------------------------------------------------------------
///                                          Solid_B_T | SOLID | SOLID_E_RT | SOLID_E_LT
///                                                      #########   
///         Solid_B_L | SOLID | SOLID_E_LT | SOLID_E_LB  # SOLID #  Solid_B_R | SOLID | SOLID_E_RT | SOLID_E_RB
///                                                      #########
///                                          Solid_B_B | SOLID | SOLID_E_RB | SOLID_E_LB
///
///   -------------------------------------------------------------------------------------------------------------
///                                           Solid_B_R  | SOLID_E_RB | SOLID_C_RT
///                                                     #############   
///         Solid_B_L | SOLID | SOLID_E_LT | SOLID_E_LB # Solid_B_R #  Water
///                                                     #############
///                                          Solid_B_R  | SOLID_E_RT | SOLID_C_RB
///
///   ------------------------------------------------------------------------------------------------------------
///                                                         Water
///                                                    #############   
///          Solid_B_T  | SOLID_E_RT | SOLID_C_LT      # Solid_B_T #  Solid_B_T  | SOLID_E_LT | SOLID_C_RT
///                                                    #############
///                                        Solid_B_B | SOLID | SOLID_E_LB | SOLID_E_RB
/// 
///   ------------------------------------------------------------------------------------------------------------
///                                          Solid_B_L  | SOLID_C_LT | SOLID_E_LB
///                                                    #############   
///                                            Water   # Solid_B_L #  Solid_B_R | SOLID | SOLID_E_RT | SOLID_E_RB
///                                                    #############
///                                          Solid_B_L  | SOLID_C_LB | SOLID_E_LT
/// 
///   ------------------------------------------------------------------------------------------------------------
///                                         SOLID | Solid_B_T | Solid_E_LT |Solid_E_RT
///                                                    #############   
///             Solid_E_RB | Solid_C_LB | Solid_B_B    # Solid_B_B #  Solid_B_B | Solid_E_LB | Solid_C_RB
///                                                    #############
///                                                         Water
/// 
///   ------------------------------------------------------------------------------------------------------------
///                                                        Water  
///                                                    ##############   
///                                            Water   # Solid_C_LT #  Solid_C_RT | Solid_E_LT | Solid_B_T
///                                                    ##############
///                                        Solid_C_LB | Solid_E_LT | Solid_B_L
///
///   ------------------------------------------------------------------------------------------------------------
///                                                        Water  
///                                                    ##############   
///                Solid_C_RT | Solid_E_RT | Solid_B_T # Solid_C_RT #  Water 
///                                                    ##############
///                                         Solid_C_RB | Solid_E_RT | Solid_B_R
///
///   ------------------------------------------------------------------------------------------------------------
///                                         Solid_C_LT | Solid_E_LB | Solid_B_L
///                                                    ##############   
///                                             Water  # Solid_C_LB #   Solid_C_RB | Solid_E_LB | Solid_B_B
///                                                    ##############
///                                                          Water
///
///   ------------------------------------------------------------------------------------------------------------
///                                         Solid_C_RT | Solid_E_RB | Solid_B_R 
///                                                    ##############   
///             Solid_C_LB | Solid_E_RB | Solid_B_B    # Solid_C_LB #   Water 
///                                                    ##############
///                                                          Water
///
///   ------------------------------------------------------------------------------------------------------------
///                                                 Solid_C_LT | Solid_B_L
///                                                    ##############   
///                            Solid_C_LT | Solid_B_T  # Solid_E_LT #   Solid | Solid_E_RT | Solid_E_RB | Solid_B_R
///                                                    ##############
///                                       Solid | Solid_E_RB | Solid_E_LB | Solid_B_B
///
///   ------------------------------------------------------------------------------------------------------------
///                                                 Solid | Solid_E_RT | Solid_E_LT | Solid_B_T
///                                                    ##############   
///                            Solid_C_LB | Solid_B_B  # Solid_E_LB #   Solid | Solid_E_RT | Solid_E_RB | Solid_B_R
///                                                    ##############
///                                               Solid_C_LB | Solid_B_L
///
///   ------------------------------------------------------------------------------------------------------------
///                                                 Solid_C_RT | Solid_B_R
///                                                    ##############   
///     Solid | Solid_E_LB | Solid_E_LT | Solid_B_L    # Solid_E_RT #    Solid_C_RT | Solid_B_T
///                                                    ##############
///                                             Solid | Solid_E_LB | Solid_E_RB | Solid_B_B
///
///   ------------------------------------------------------------------------------------------------------------
///                                        Solid | Solid_E_LT | Solid_E_RT | Solid_B_T
///                                                    ##############   
///     Solid | Solid_E_LB | Solid_E_LT | Solid_B_L    # Solid_E_RB #    Solid_C_RB | Solid_B_B
///                                                    ##############
///                                                 Solid_C_RB | Solid_B_R
/// 
///
///
///
/// 
/// </summary>
[Flags]
public enum Wavecell
{
    None = 0,
    AllTerain = 0x3FFF,
    Solid = 1 << 0,
    Solid_B_R = 1 << 1,
    Solid_B_T = 1 << 2,
    Solid_B_L = 1 << 3,
    Solid_B_B = 1 << 4,
    Solid_C_LT = 1 << 5,
    Solid_C_RT = 1 << 6,
    Solid_C_LB = 1 << 7,
    Solid_C_RB = 1 << 8,
    Solid_E_LT = 1 << 9,
    Solid_E_LB = 1 << 10,
    Solid_E_RT = 1 << 11,
    Solid_E_RB = 1 << 12,
    Water = 1 << 13,
    Collapsed = 1 << 14
}