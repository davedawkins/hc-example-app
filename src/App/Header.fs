module Header

open Types
open Sutil
open type Feliz.length

let viewHeader (state : System.IObservable<AppState>) (changePage : Page -> unit) =
    let viewLogo() = 
        Html.divc "app-logo" [
            Html.img [ Attr.src "https://cdn2.hubspot.net/hub/5314158/hubfs/favicon.ico?width=108&height=108" ]
        ]

    let makePageButton (p : Page) =
        Html.divc "flat-button" [ 
            text (string p)
            Bind.toggleClass( state .>> (fun s -> s.CurrentPage = p), "selected" )
            Ev.onClick (fun _ -> changePage p)
        ]

    let viewPageMenu() = 
        Html.divc "app-page-menu flex-row" [ 
            yield! Page.All |> List.map makePageButton
        ]

    let viewUser() =
        Html.divc "app-user" [ 
            Bind.el( state .>> _.User, fun u ->
                match u with
                | None -> Html.divc "flat-button" [ text "Login" ]
                | Some user ->
                    Html.divc "flex-row" [
                        text (sprintf "Welcome %s" user.Name)
                        Html.span [ 
                            Attr.style [  Css.marginLeft (px 4) ]
                            text "|" 
                        ] 
                        Html.divc "flat-button" [ text "Logout" ]
                    ] )
        ]   

    Html.divc "app-header font120 space-between" [ 
        viewLogo()
        viewPageMenu()
        viewUser()
    ]
