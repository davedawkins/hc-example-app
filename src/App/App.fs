module App

open Sutil
open Types
open type Feliz.length
open Server

let init (u : User option, p : Page) = { CurrentPage = p; User = u; Address = None }

let viewPageHome (s : System.IObservable<Address option>) =
    Bind.el( s, fun a ->
        match a with 
        | None -> Html.div "Go to the Address page to create a new address"
        | Some addr -> 
            Html.divc "flex-column" [
                Html.h4 "Current Address"
                Html.divc "flex-row" [ Html.span "Address Line 1: "; Html.span (addr.Addr1) ]
                Html.divc "flex-row" [ Html.span "Address Line 2: "; Html.span (addr.Addr2) ]
                Html.divc "flex-row" [ Html.span "Is Billing: "; Html.span (addr.IsBilling |> string) ]
            ] )

let viewPage (appState : IStore<AppState>) (setAddress) =
    Html.divc "app-page-container" [
        Bind.el( 
            (appState .>> _.CurrentPage), // See notes below
            fun p ->
                match p with
                | Home -> viewPageHome (appState .>> _.Address)
                | Login -> Html.div "Login"
                | Address -> 
                    Address.editAddress 
                        (appState.Value.Address |> Option.defaultValue (Address.Create()))
                        setAddress 
        )
    ]

let viewMain (server : Server) = 
    // Initialise application state as a store
    let appState = Store.make (init (Some User.Guest, Home))

    // Updater functions 
    let update map = appState |> Store.modify map
    let setCurrentPage page = update (fun s -> { s with CurrentPage = page } )
    let setAddress addr = update (fun s -> { s with Address = Some addr } )

    Html.divc "app-main" [

        Header.viewHeader 
            appState 
            setCurrentPage

        viewPage 
            appState 
            (fun addr -> setAddress addr; setCurrentPage Home)
    ]


let view() = 
    // Load connection to server, may take a second or two...
    Bind.promise( Server.Create(), viewMain )

view() |> Program.mount
