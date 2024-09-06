module Types

/// Read Wlaschin on Domain Modelling to see why this is a terrible way of modelling
/// an address!!  It breaks nearly all of his rules. However, this isn't a study in 
/// DDD, we're looking at how to edit and view such data structures with Sutil.
type Address = 
    {
        Addr1 : string
        Addr2 : string
        Postcode : string
        Town : string
        IsBilling : bool
    }
    with 
        static member Create() = { Addr1 = ""; Addr2 = ""; Postcode = ""; Town = ""; IsBilling = false }

type User =
    {
        Name : string
        Username : string
        Email : string
    }
    static member Guest = 
        {
            Name = "guest"
            Username = "guest"
            Email = "nobody@any.com"
        }

type Page =
    | Home
    | Login
    | Address
    with static member All = [ Home; Address ]

type AppState =
    {
        User : User option
        CurrentPage : Page
        Address : Address option
    }
