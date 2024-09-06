module Types

type Address = 
    {
        Addr1 : string
        Addr2 : string
        Postcode : string
        Town : string
    }
    with 
        static member Create() = { Addr1 = ""; Addr2 = ""; Postcode = ""; Town = "" }

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
