module Address

open Types
open Sutil
open type Feliz.length

let validateAddress (addr : Address) : Result<Address,string> =
    if addr.Addr1 |> System.String.IsNullOrWhiteSpace then
        Error "Address Line 1 must be given"
    else
        Ok addr

open Helpers.HtmlInput

let editAddress (addr: Address) (apply: Address -> unit) =

    let addrS = Store.make addr
    let update map = addrS |> Store.modify map

    let setAddr1 s = update (fun addr -> { addr with Addr1 = s })
    let setAddr2 s = update (fun addr -> { addr with Addr2 = s })
    let setIsBilling z = update (fun addr -> { addr with IsBilling = z })

    Html.divc "flex-column gap" [

        // Attr.style [
        //     Css.border (px 1, Feliz.borderStyle.solid, "gray" )
        //     Css.borderRadius (rem 1)
        // ]

        Html.divc "field" [
            Html.labelc "label" [ text "Address Line 1" ]
            Html.divc "control" [
                Html.inputc "input" [
                    Bind.attr("value", addrS .>> _.Addr1, setAddr1 )
                ]
            ]
        ]

        Html.divc "field" [
            Html.labelc "label" [ text "Address Line 2" ]
            Html.divc "control" [
                Html.inputc "input" [
                    Bind.attr("value", addrS .>> _.Addr2, setAddr2 )
                ]
            ]
        ]

        Html.divc "field" [
            Html.labelc "label" [ text "Is Billing" ]
            Html.divc "control" [
                Checkbox [
                    Checked (Observable  (addrS .>> _.IsBilling))
                    OnChecked setIsBilling
                ]
            ]
        ]

        Html.button [
            Bind.attr("disabled", addrS .>> validateAddress |> Store.map (function Ok _ -> false | _ -> true))
            text "Apply"
            Ev.onClick (fun _ -> apply (addrS.Value))
        ]

        Html.divc "error font80" [
            Bind.el( addrS .>> validateAddress, fun r ->
                match r with
                | Ok _ -> text ""
                | Error s -> text s)
        ]


    ]


